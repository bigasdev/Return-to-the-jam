using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Stats")]
    public int jellyEnergy = 1000;
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
    Vector2 direction = new Vector2(0, 1*50);
    Animator animator;
    void Start(){
        animator = GetComponentInChildren<Animator>();
        timer = new Timer(.5f, true);
        timer.OnComplete += ()=>{
            if(h != 0 || v != 0){
            jellyEnergy -= jellyDecreaseFromMove;
            }
        };
    }
    void Update(){
        shootTimer += Time.deltaTime;
        if(Input.GetKeyDown(KeyCode.E) && shootTimer >= shootCd){
            Shoot();
        }
        if(Input.GetKeyDown(KeyCode.A)){
            animator.SetInteger("direction", 0);
            direction = new Vector2(-1*50, 0);
        }
        if(Input.GetKeyDown(KeyCode.D)){
            animator.SetInteger("direction", 2);
            direction = new Vector2(1*50, 0);
        }
        if(Input.GetKeyDown(KeyCode.W)){
            animator.SetInteger("direction", 1);
            direction = new Vector2(0, 1*50);
        }
        if(Input.GetKeyDown(KeyCode.S)){
            animator.SetInteger("direction", 3);
            direction = new Vector2(0, -1*50);
        }
        animator.SetBool("Walking", h != 0 || v != 0);
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");
        timer.Update();
    }
    void Shoot(){
        animator.SetTrigger("Attack");
        shootTimer = 0;
        jellyEnergy -= jellyDecreaseFromAttack;
        var b = PoolsManager.Instance.GetPool("PlayerBullets")?.GetFromPool(this.transform.position);
        b.GetComponent<Bullet>().direction = direction;
    }
    void FixedUpdate(){
        this.transform.position = Vector2.MoveTowards(this.transform.position, new Vector2(this.transform.position.x + h, this.transform.position.y + v), moveSpeed * Time.deltaTime);
    }
    int GetDecreaseForMove(){
        return (int)(h*jellyDecreaseFromMove+v*jellyDecreaseFromMove);
    }
}
