using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraChange : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) {
        var p = other.GetComponent<Player>();
        if(p == null)return;
        CameraManager.Instance.obj = this.transform;
    }
}
