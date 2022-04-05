using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    [Header("Stats")]
    public int maxHp;
    public float moveSpeed;
    public SpriteRenderer sprite;

    int currentHp;
    int expAmount, pointsAmount;
    Timer timer, damageTimer;
    Transform player;
    GameObject particle;
    Color color;
    private void Start() {
        player = TagQuery.FindObject("Jelly").transform;
        currentHp = maxHp;
        timer = new Timer(5);
        timer.OnComplete += ()=>{
            timer.elapsed = 0;
            PoolsManager.Instance.GetPool("EntityPool").AddToPool(this.gameObject);
        };
    }
    public void Initialize(EntityData entityData, int number, int increase = 1){
        var d = entityData.damage * number * increase;
        damageTimer = new Timer(TagQuery.FindObject("Jelly").GetComponent<Player>().damageTick, true);
        damageTimer.OnComplete += () =>{
            if(Vector2.Distance(this.transform.position, player.position) <= .5f)player.GetComponent<Player>().jellyEnergy-=d;
        };
        maxHp = entityData.hp * number * increase;
        currentHp = maxHp;
        moveSpeed = entityData.moveSpeed;
        expAmount = entityData.experienceAmount * number * increase;
        pointsAmount = entityData.points * number * increase;
        sprite.sprite = entityData.lowExp;
        color = entityData.color;
        if(number == 2){
            sprite.transform.localScale = new Vector3(2,2,1);
            sprite.sprite = entityData.medExp;
        }else if(number == 3){
            sprite.transform.localScale = new Vector3(3,3,1);
            sprite.sprite = entityData.highExp;
        }
    }
    private void Update() {
        if(StateController.Instance.currentState == States.GAME_IDLE)return;
        damageTimer.Update();
        var x = this.transform.position.x - player.transform.position.x;
        transform.localScale = x >= 0 ? new Vector3(1,1,1) : new Vector3(-1,1,1);
        if(Vector2.Distance(this.transform.position, player.position) <= 2)timer.Update();
        this.transform.position = Vector2.MoveTowards(this.transform.position, player.position, moveSpeed * Time.deltaTime);
    }
    public virtual void Damage(int amount){
        AudioController.Instance.PlaySound("hit");
        currentHp -= amount;
        particle = PoolsManager.Instance.GetPool("BloodPool").GetFromPool(this.transform.position);
        particle.GetComponent<ParticleSystem>().Play();
        particle.GetComponent<ParticleSystem>().startColor = color;
        GetComponent<SpriteSquash>()?.Squash(.8f, .65f);
        if(currentHp <= 0)Die();
    }
    public virtual void Die(){
        timer.elapsed = 0;
        sprite.transform.localScale = new Vector3(1,1,1);
        AudioController.Instance.PlaySound("explosion");
        CameraManager.Instance.killShake();
        Engine.points += pointsAmount;
        Engine.mobsKilled++;
        for (int i = 0; i < expAmount; i++)
        {
            PoolsManager.Instance.GetPool("ExperiencePool").GetFromPool(new Vector2(this.transform.position.x + Random.Range(-.5f, .5f), this.transform.position.y + Random.Range(-.5f, .5f)));
        }
        PoolsManager.Instance.GetPool("MiniJamPool").GetFromPool(this.transform.position);
        PoolsManager.Instance.GetPool("EntityPool").AddToPool(this.gameObject);
    }
}
[System.Serializable]
public class EntityData{
    public int hp;
    public float moveSpeed;
    public Sprite lowExp, medExp, highExp;
    public int experienceAmount;
    public int points;
    public int damage = 1;
    public string pool;
    public Color color;

    public EntityData(int hp, float moveSpeed, Sprite lowExp, Sprite medExp, Sprite highExp, int experienceAmount, int points, string pool, Color color, int damage = 1)
    {
        this.hp = hp;
        this.moveSpeed = moveSpeed;
        this.lowExp = lowExp;
        this.medExp = medExp;
        this.highExp = highExp;
        this.damage = damage;
        this.experienceAmount = experienceAmount;
        this.points = points;
        this.pool = pool;
        this.color = color;
    }
}
