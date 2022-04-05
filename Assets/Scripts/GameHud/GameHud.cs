using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class GameHud : MonoBehaviour
{
    private static GameHud instance;
    public static GameHud Instance{
        get{
            if(instance == null)instance = FindObjectOfType<GameHud>();
            return instance;
        }
    }
    [SerializeField] Text jellyText, nextWaveText, playTime, scoreText, jellyExperienceText;
    [SerializeField] Image hpBar, expBar;
    public Animator nextWaveAnim;
    public GameObject pauseMenu;
    Player player;
    bool paused;
    float time;
    Coroutine pause;
    void Start(){
        player = TagQuery.FindObject("Jelly")?.GetComponent<Player>();
    }
    void Update(){
        if(Input.GetKeyDown(KeyCode.Escape)){
            pauseMenu.SetActive(true);
            StateController.Instance.ChangeState(States.GAME_IDLE);
            if(pause == null)pause = StartCoroutine(Pause());
        }
        if(paused){
            if(Input.anyKeyDown){
                StateController.Instance.ChangeState(States.GAME_UPDATE);
                pauseMenu.SetActive(false);
                paused = false;
            }
        }
        time = Time.realtimeSinceStartup;
        TimeSpan timeSpan = TimeSpan.FromSeconds(time);

        string timeText = timeSpan.ToString(@"mm\:ss");
        playTime.text = timeText;
        var f = (float)player.jellyEnergy/(float)1000;

        hpBar.fillAmount = f;
        expBar.fillAmount = (float)player.jellyExperience/ (float)player.jellyExperienceMax;

        jellyText.text = "Jam Energy: "+player.jellyEnergy;
        jellyExperienceText.text = "Exp : "+player.jellyExperience;
        scoreText.text = Engine.points.ToString();
    }
    public void SetText(int text){
        nextWaveText.text = "WAVE "+text+"!";
    }
    IEnumerator Pause(){
        yield return new WaitForSeconds(.2f);
        paused = true;
        pause = null;
    }
}
