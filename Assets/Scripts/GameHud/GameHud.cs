using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameHud : MonoBehaviour
{
    [SerializeField] Text jellyText;
    Player player;
    void Start(){
        player = TagQuery.FindObject("Jelly")?.GetComponent<Player>();
    }
    void Update(){
        jellyText.text = "Jam Energy: "+player.jellyEnergy;
    }

}
