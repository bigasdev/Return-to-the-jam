using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class DieScreen : MonoBehaviour
{
    [SerializeField] Text statsText;
    private void Start() {
        StateController.Instance.ChangeState(States.GAME_IDLE);
        statsText.text = $"Points: {Engine.points} \n Buffs: {Engine.buffsPicked} \n Experience Gained: {Engine.experienceGained} \n Mobs Killed : {Engine.mobsKilled}";
    }

    public void Respawn(){
        SceneManager.LoadScene("Game", LoadSceneMode.Single);
    }
    public void MainMenu(){
        SceneManager.LoadScene("MainMenu");
    }
}
