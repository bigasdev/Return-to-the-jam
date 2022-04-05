using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class Buff
{
    public int counter;
    public abstract string Name();
    public abstract string Description();
    public abstract void Initialize();
    public abstract void Add();
    public abstract void Update();
}
[System.Serializable]
public class DamageBuff : Buff
{
    public override void Add()
    {
        TagQuery.FindObject("Jelly").GetComponent<Player>().playerDamage++;
        counter++;
    }

    public override string Description()
    {
        return "Increases your damage dealt by 1.";
    }

    public override void Initialize()
    {
        TagQuery.FindObject("Jelly").GetComponent<Player>().playerDamage++;
        counter = 1;
    }

    public override string Name()
    {
        return "JAM DAMAGE!";
    }

    public override void Update()
    {
        
    }
}
[System.Serializable]
public class AtkSpeedBuff : Buff
{
    public override void Add()
    {
        TagQuery.FindObject("Jelly").GetComponent<Player>().shootCd -= .045f;
        counter++;
    }

    public override string Description()
    {
        return "Increase your atk speed by 5%";
    }

    public override void Initialize()
    {
        TagQuery.FindObject("Jelly").GetComponent<Player>().shootCd -= .045f;
        counter = 1;
    }

    public override string Name()
    {
        return "MINIGUN!";
    }

    public override void Update()
    {
        
    }
}
[System.Serializable]
public class ShieldBuff : Buff
{
    public override void Add()
    {
        TagQuery.FindObject("Jelly").GetComponent<Player>().damageTick += .05f;
        counter++;
    }

    public override string Description()
    {
        return "Reduce the amount of damage you take.";
    }

    public override void Initialize()
    {
        TagQuery.FindObject("Jelly").GetComponent<Player>().damageTick += .015f;
        counter = 1;
    }

    public override string Name()
    {
        return "SHIELD!";
    }

    public override void Update()
    {
        
    }
}
[System.Serializable]
public class HealthBuff : Buff
{
    public override void Add()
    {
        TagQuery.FindObject("Jelly").GetComponent<Player>().jellyEnergy += 100;
        counter++;
    }

    public override string Description()
    {
        return "Increase your energy by 100";
    }

    public override void Initialize()
    {
        TagQuery.FindObject("Jelly").GetComponent<Player>().jellyEnergy += 100;
        counter = 1;
    }

    public override string Name()
    {
        return "GIANT JAM!";
    }

    public override void Update()
    {
        
    }
}
[System.Serializable]
public class MoveSpeedBuff : Buff
{
    public override void Add()
    {
        TagQuery.FindObject("Jelly").GetComponent<Player>().moveSpeed += .5f;
        counter++;
    }

    public override string Description()
    {
        return "Increase your move speed.";
    }

    public override void Initialize()
    {
        TagQuery.FindObject("Jelly").GetComponent<Player>().moveSpeed += .5f;
        counter = 1;
    }

    public override string Name()
    {
        return "FLASH!";
    }

    public override void Update()
    {
        
    }
}
[System.Serializable]
public class GloveBuff : Buff
{
    public override void Add()
    {
        TagQuery.FindObject("Jelly").GetComponent<Player>().jamBonus += 10;
        counter++;
    }

    public override string Description()
    {
        return "Increase the amount of jam you gather";
    }

    public override void Initialize()
    {
        TagQuery.FindObject("Jelly").GetComponent<Player>().jamBonus += 10;
        counter = 1;
    }

    public override string Name()
    {
        return "GLOVE!";
    }

    public override void Update()
    {
        
    }
}
[System.Serializable]
public class SkullBuff : Buff
{
    public override void Add()
    {
        TagQuery.FindObject("Jelly").GetComponent<Player>().skull.speed += 1;
        counter++;
    }

    public override string Description()
    {
        return "Add a skull that rotates around you and do damage";
    }

    public override void Initialize()
    {
        TagQuery.FindObject("Jelly").GetComponent<Player>().skull.gameObject.SetActive(true);
        counter = 1;
    }

    public override string Name()
    {
        return "SKULL!";
    }

    public override void Update()
    {
        
    }
}
[System.Serializable]
public class ShurikenBuff : Buff
{
    public override void Add()
    {
        if(counter >= 3){
            Engine.Instance.buffsAvaliable.Remove(this);
        }
        TagQuery.FindObject("Jelly").GetComponent<Player>().shurikenCd -= .50f;
        TagQuery.FindObject("Jelly").GetComponent<Player>().shurikenTimer.setTime = TagQuery.FindObject("Jelly").GetComponent<Player>().shurikenCd;
        counter++;
    }

    public override string Description()
    {
        return "You shoot 4 shurikens in the direction you are facing!";
    }

    public override void Initialize()
    {
        TagQuery.FindObject("Jelly").GetComponent<Player>().hasShuriken = true;
        counter = 1;
    }

    public override string Name()
    {
        return "SHURIKEN!";
    }

    public override void Update()
    {
        
    }
}
public class SpearBuff : Buff
{
    public override void Add()
    {
        if(counter >= 1){
            Engine.Instance.buffsAvaliable.Remove(this);
        }
        TagQuery.FindObject("Jelly").GetComponent<Player>().pierce++;
        counter++;
    }

    public override string Description()
    {
        return "Your shots pierce!";
    }

    public override void Initialize()
    {
        TagQuery.FindObject("Jelly").GetComponent<Player>().pierce++;
        counter = 1;
    }

    public override string Name()
    {
        return "PIERCE!";
    }

    public override void Update()
    {
        
    }
}
public class MirrorBuff : Buff
{
    public override void Add()
    {
    }

    public override string Description()
    {
        return "You shot backwards too!";
    }

    public override void Initialize()
    {
        TagQuery.FindObject("Jelly").GetComponent<Player>().hasMirror = true;
        counter = 1;
        Engine.Instance.buffsAvaliable.Remove(this);
    }

    public override string Name()
    {
        return "MIRROR!";
    }

    public override void Update()
    {
        
    }
}
public class ArrowBuff : Buff
{
    public override void Add()
    {
        if(counter >= 2){
            Engine.Instance.buffsAvaliable.Remove(this);
        }
        TagQuery.FindObject("Jelly").GetComponent<Player>().arrowCd -= .5f;
        counter++;
    }

    public override string Description()
    {
        return "You shot an arrow in the direction you are facing!";
    }

    public override void Initialize()
    {
        TagQuery.FindObject("Jelly").GetComponent<Player>().hasArrow = true;
        counter = 1;
    }

    public override string Name()
    {
        return "ARROW!";
    }

    public override void Update()
    {
        
    }
}