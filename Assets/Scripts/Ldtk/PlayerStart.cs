using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStart : MonoBehaviour
{
    private void Start() {
        TagQuery.FindObject("Jelly").transform.position = this.transform.position;
    }
}
