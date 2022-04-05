using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public System.Action onReset = delegate{};
    [SerializeField] float moveSpeed;
    [SerializeField] float movingDistance = 15;
    public Vector2 direction;
    public int pierce = 0;
    public GameObject shuriken;
    public GameObject arrow;
    Vector2 startingDist;
    void Start(){
        startingDist = this.transform.position;
    }
    public void StartDist() {
        startingDist = this.transform.position;
    }
    void Update(){
        if(direction == Vector2.zero)return;
        if(Vector2.Distance(startingDist, this.transform.position) >= movingDistance){
            Reset();
        }
        this.transform.position = Vector2.MoveTowards(this.transform.position, direction, moveSpeed * Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D other) {
        var e = other.GetComponent<Entity>();
        if(e==null)return;
        pierce--;
        OnHit(e);
        if(pierce < 0)Reset();
    }
    public virtual void OnHit(Entity entity){
        entity.Damage(TagQuery.FindObject("Jelly").GetComponent<Player>().playerDamage);
    }
    public virtual void Reset(){
        pierce = 0;
        direction = Vector2.zero;
        shuriken.SetActive(false);
        arrow.SetActive(false);
        PoolsManager.Instance.GetPool("PlayerBullets")?.AddToPool(this.gameObject);
        onReset();
    }
}
