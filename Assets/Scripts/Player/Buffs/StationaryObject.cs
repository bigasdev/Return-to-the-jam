using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationaryObject : MonoBehaviour
{
    public float speed;
    public Transform target;
    private Vector3 zAxis = new Vector3(0,0,1);
    private void FixedUpdate() {
        transform.RotateAround(target.position, zAxis, speed);
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(StateController.Instance.currentState != States.GAME_UPDATE)return;
        var e = other.GetComponent<Entity>();
        if(e==null)return;
        OnHit(e);
    }
    public virtual void OnHit(Entity entity){
        entity.Damage(1);
    }
}
