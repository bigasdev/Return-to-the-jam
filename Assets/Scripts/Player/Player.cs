using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Stats")]
    public int jellyEnergy = 1000;
    public int jellyExperienceMax = 100;
    public int jellyExperience;
    [Header("Move Stuff and what not")]
    [SerializeField] float moveSpeed;
    [Header("Jelly decrease")]
    [SerializeField] int jellyDecreaseFromMove;
    [SerializeField] int jellyDecreaseFromAttack;
    [SerializeField] float jellyDecreaseFromDamage;
    [Header("Combat")]
    [SerializeField] float shootCd;
    float h, v;
    Timer timer;
    float shootTimer;
    bool shot = false;
    Vector2 direction = new Vector2(0, 1*50);
    Animator animator;
    void Start(){
        animator = GetComponentInChildren<Animator>();
        timer = new Timer(1.5f, true);
        timer.OnComplete += ()=>{
            if(h != 0 || v != 0){
            PoolsManager.Instance.GetPool("JamTrail").GetFromPool(this.transform.position).GetComponent<PlayerTrail>().Change();
            jellyEnergy -= jellyDecreaseFromMove;
            }
        };
    }
    void Update(){
        if(StateController.Instance.currentState == States.GAME_IDLE)return;
        if(jellyExperience >= jellyExperienceMax){
            h = 0;
            v = 0;
            var b = Resources.Load<GameObject>("Prefabs/UI/BuffScreen");
            var buff = Instantiate(b);
            jellyExperience = 0;
            jellyExperienceMax += 10;
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
        animator.SetTrigger("Attack");
        shot = true;
        shootTimer = 0;
        jellyEnergy -= jellyDecreaseFromAttack;
        var b = PoolsManager.Instance.GetPool("PlayerBullets")?.GetFromPool(this.transform.position);
        b.GetComponent<Bullet>().StartDist();
        b.GetComponent<Bullet>().direction = new Vector2(direction.x == 0 ? b.transform.position.x : direction.x, direction.y == 0 ? b.transform.position.y : direction.y);
    }
    void Die(){
        var d = Resources.Load<GameObject>("Prefabs/UI/DieScreen");
        var die = Instantiate(d);
    }
    void FixedUpdate(){
        this.transform.position = Vector2.MoveTowards(this.transform.position, new Vector2(this.transform.position.x + h, this.transform.position.y + v), moveSpeed * Time.deltaTime);
    }
    int GetDecreaseForMove(){
        return (int)(h*jellyDecreaseFromMove+v*jellyDecreaseFromMove);
    }
}
