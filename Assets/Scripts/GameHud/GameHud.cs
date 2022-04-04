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
    Player player;
    float time;
    void Start(){
        player = TagQuery.FindObject("Jelly")?.GetComponent<Player>();
    }
    void Update(){
        time = Time.realtimeSinceStartup;
        TimeSpan timeSpan = TimeSpan.FromSeconds(time);

        string timeText = timeSpan.ToString(@"mm\:ss");
        playTime.text = timeText;

        hpBar.fillAmount = (float)player.jellyEnergy/(float)1000;
        expBar.fillAmount = (float)player.jellyExperience/ (float)player.jellyExperienceMax;

        jellyText.text = "Jam Energy: "+player.jellyEnergy;
        jellyExperienceText.text = "Exp : "+player.jellyExperience;
        scoreText.text = Engine.points.ToString();
    }
    public void SetText(int text){
        nextWaveText.text = "Next wave in..."+text+" seconds!";
    }
}
