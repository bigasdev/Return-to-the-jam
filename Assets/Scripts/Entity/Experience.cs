using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Experience : MonoBehaviour
{
    [SerializeField] float moveSpeed = 4;
    Transform obj;
    private void Start() {
        obj = TagQuery.FindObject("Jelly").transform;
    }
    private void Update() {
        if(Vector2.Distance(this.transform.position, obj.position) <= 3){
            this.transform.position = Vector2.MoveTowards(this.transform.position, obj.position, moveSpeed * Time.deltaTime);
        }
    }
    private void OnTriggerEnter2D(Collider2D other) {
        var p = other.GetComponent<Player>();
        if(p == null)return;
        Engine.experienceGained++;
        p.jellyExperience++;
        PoolsManager.Instance.GetPool("ExperiencePool").AddToPool(this.gameObject);
    }
}
