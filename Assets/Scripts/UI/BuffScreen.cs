using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BuffScreen : MonoBehaviour
{
    [SerializeField] Image[] icons;
    [SerializeField] Text[] titles, descriptions;
    List<Buff> buffs = new List<Buff>();
    private void Start() {
        StateController.Instance.ChangeState(States.GAME_IDLE);
        var rnd = Engine.Instance.GetRnd(3, Engine.Instance.buffsAvaliable.Count);
        foreach(var i in rnd){
            buffs.Add(Engine.Instance.buffsAvaliable[i]);
        }
        var inte = 0;
        foreach(var b in buffs){
            icons[inte].sprite = ResourceController.GetSprite(b.Name());
            titles[inte].text = b.Name() + "+" + b.counter;
            descriptions[inte].text = b.Description();
            inte++;
        }
    }
    public void SelectBuff(int b){
        Engine.buffsPicked++;
        AudioController.Instance.PlaySound("buttonClick");
        TagQuery.FindObject("Jelly").GetComponent<Player>().AddBuff(buffs[b]);
        Destroy(this.gameObject);
        StateController.Instance.ChangeState(States.GAME_UPDATE);
    }
}
