using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTrail : MonoBehaviour
{
    [SerializeField] Sprite[] trails;
    Timer timer;
    private void Start() {
        Change();
        timer = new Timer(5, true);
        timer.OnComplete += ()=>{
            timer.Reset();
            PoolsManager.Instance.GetPool("JamTrail").AddToPool(this.gameObject);
        };
    }
    private void Update() {
        timer.Update();
    }
    public void Change(){
        var rnd = Random.Range(0, trails.Length);
        GetComponent<SpriteRenderer>().sprite = trails[rnd];
    }
}
