using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffScreen : MonoBehaviour
{
    private void Start() {
        StateController.Instance.ChangeState(States.GAME_IDLE);
    }
    public void SelectBuff(){
        Destroy(this.gameObject);
        StateController.Instance.ChangeState(States.GAME_UPDATE);
    }
}
