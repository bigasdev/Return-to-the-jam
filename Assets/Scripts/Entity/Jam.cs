using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jam : MonoBehaviour
{
    [SerializeField] int jamAmount;
    private void OnTriggerEnter2D(Collider2D other) {
        var p = other.GetComponent<Player>();
        if(p == null)return;
        TagQuery.FindObject("Jelly").GetComponent<Player>().jellyEnergy += jamAmount;
        PoolsManager.Instance.GetPool("MiniJamPool").AddToPool(this.gameObject);
    }
}
