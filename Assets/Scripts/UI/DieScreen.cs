using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class DieScreen : MonoBehaviour
{
    [SerializeField] Text statsText;
    Coroutine loading;
    private void Start() {
        StateController.Instance.ChangeState(States.GAME_IDLE);
        statsText.text = $"Points: {Engine.points} \n Buffs: {Engine.buffsPicked} \n Experience Gained: {Engine.experienceGained} \n Mobs Killed : {Engine.mobsKilled}";
    }

    public void Respawn(){
        Engine.Instance.Restart();
        if(loading == null)loading = StartCoroutine(LoadCoroutine("Game"));
    }
    public void MainMenu(){
        if(loading == null)loading = StartCoroutine(LoadCoroutine("MainMenu"));
    }
    IEnumerator LoadCoroutine(string name){
        AsyncOperation operation = SceneManager.LoadSceneAsync(name);
        while(!operation.isDone){
            yield return null;
        }
        loading = null;
    }
}
