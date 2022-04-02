using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    [Header("Stats")]
    public int maxHp;

    int currentHp;
    private void Start() {
        currentHp = maxHp;
    }
    public virtual void Damage(int amount){
        currentHp -= amount;
        GetComponent<SpriteSquash>()?.Squash(.65f, .85f);
        if(currentHp <= 0)Die();
    }
    public virtual void Die(){
        CameraManager.Instance.killShake();
        Destroy(this.gameObject);
    }
}
