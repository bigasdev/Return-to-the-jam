using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Stats")]
    public int jellyEnergy = 1000;
    public int jellyExperienceMax = 100;
    public int jellyExperience;
    public int playerDamage = 1;
    public int jamBonus = 0;
    public int pierce = 0;
    [Header("Move Stuff and what not")]
    public float moveSpeed;
    [Header("Jelly decrease")]
    [SerializeField] int jellyDecreaseFromMove;
    [SerializeField] int jellyDecreaseFromAttack;
    [SerializeField] float jellyDecreaseFromDamage;
    [Header("Combat")]
    public bool hasShuriken = false;
    public bool hasArrow = false;
    public bool hasMirror = false;
    public float shurikenCd = 10f, arrowCd = 3f;
    public float shootCd;
    public List<Buff> buffs = new List<Buff>();
    public float damageTick = .05f;
    public StationaryObject skull;
    float h, v;
    Timer timer;
    public Timer shurikenTimer;
    public Timer arrowTimer;
    float shootTimer;
    bool shot = false;
    Vector2 direction = new Vector2(0, 1*50);
    Animator animator;
    public void AddBuff(Buff buff){
        foreach(var b in buffs){
            if(b.Name() == buff.Name()){
                b.Add();
                return;
            }
        }
        buff.Initialize();
        buffs.Add(buff);
    }
    void Start(){
        buffs = new List<Buff>();
        animator = GetComponentInChildren<Animator>();
        timer = new Timer(1.5f, true);
        shurikenTimer = new Timer(shurikenCd, true);
        shurikenTimer.OnComplete += ()=>{
            for (int i = 0; i < 4; i++)
            {
                var b = PoolsManager.Instance.GetPool("PlayerBullets")?.GetFromPool(new Vector2(this.transform.position.x + i, this.transform.position.y));
                b.GetComponent<Bullet>().pierce = pierce;
                b.GetComponent<Bullet>().StartDist();
                b.GetComponent<Bullet>().shuriken.SetActive(true);
                b.GetComponent<Bullet>().direction = new Vector2(direction.x == 0 ? b.transform.position.x : direction.x, direction.y == 0 ? b.transform.position.y : direction.y);
            }
        };
        arrowTimer = new Timer(arrowCd, true);
        arrowTimer.OnComplete += ()=>{
            var b = PoolsManager.Instance.GetPool("PlayerBullets")?.GetFromPool(new Vector2(this.transform.position.x, this.transform.position.y));
            b.GetComponent<Bullet>().pierce = pierce;
            b.GetComponent<Bullet>().StartDist();
            b.GetComponent<Bullet>().arrow.SetActive(true);
            b.GetComponent<Bullet>().direction = new Vector2(direction.x == 0 ? b.transform.position.x : direction.x, direction.y == 0 ? b.transform.position.y : direction.y);
        };
        timer.OnComplete += ()=>{
            if(h != 0 || v != 0){
            PoolsManager.Instance.GetPool("JamTrail").GetFromPool(this.transform.position).GetComponent<PlayerTrail>().Change();
            jellyEnergy -= jellyDecreaseFromMove;
            }
        };
    }
    void Update(){
        if(StateController.Instance.currentState == States.GAME_IDLE)return;
        if(hasShuriken)shurikenTimer.Update();
        if(hasArrow)arrowTimer.Update();
        if(jellyExperience >= jellyExperienceMax){
            AudioController.Instance.PlaySound("powerUp");
            h = 0;
            v = 0;
            var b = Resources.Load<GameObject>("Prefabs/UI/BuffScreen");
            var buff = Instantiate(b);
            jellyExperience = 0;
            jellyExperienceMax += (int)(jellyExperienceMax * .3f);
            return;
        }
        if(jellyEnergy <= 0){
            Die();
        }
        shootTimer += Time.deltaTime;
        if(shootTimer >= shootCd){
            shot = false;
        }
        if(Input.GetKeyDown(KeyCode.E) && shootTimer >= shootCd){
            Shoot();
        }
        if(Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)){
            animator.SetInteger("direction", 0);
            direction = new Vector2(-1*50, 0);
        }
        if(Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)){
            animator.SetInteger("direction", 2);
            direction = new Vector2(1*50, 0);
        }
        if(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)){
            animator.SetInteger("direction", 1);
            direction = new Vector2(0, 1*50);
        }
        if(Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)){
            animator.SetInteger("direction", 3);
            direction = new Vector2(0, -1*50);
        }
        animator.SetBool("Walking", h != 0 || v != 0);
        animator.SetBool("Walking", shot == false);
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");
        timer.Update();
    }
    void Shoot(){
        AudioController.Instance.PlaySound("shoot");
        animator.SetTrigger("Attack");
        shot = true;
        shootTimer = 0;
        jellyEnergy -= jellyDecreaseFromAttack;
        var b = PoolsManager.Instance.GetPool("PlayerBullets")?.GetFromPool(this.transform.position);
        b.GetComponent<Bullet>().pierce = pierce;
        b.GetComponent<Bullet>().StartDist();
        b.GetComponent<Bullet>().direction = new Vector2(direction.x == 0 ? b.transform.position.x : direction.x, direction.y == 0 ? b.transform.position.y : direction.y);
        if(hasMirror){
            var bu = PoolsManager.Instance.GetPool("PlayerBullets")?.GetFromPool(this.transform.position);
            bu.GetComponent<Bullet>().pierce = pierce;
            bu.GetComponent<Bullet>().StartDist();
            bu.GetComponent<Bullet>().direction = new Vector2(direction.x == 0 ? b.transform.position.x : direction.x * -1, direction.y == 0 ? b.transform.position.y : direction.y * -1);
        }
    }
    void Die(){
        var d = Resources.Load<GameObject>("Prefabs/UI/DieScreen");
        TagQuery.DisposeCache();
        var die = Instantiate(d);
    }
    void FixedUpdate(){
        this.transform.position = Vector2.MoveTowards(this.transform.position, new Vector2(this.transform.position.x + h, this.transform.position.y + v), moveSpeed * Time.deltaTime);
    }
    int GetDecreaseForMove(){
        return (int)(h*jellyDecreaseFromMove+v*jellyDecreaseFromMove);
    }
}
