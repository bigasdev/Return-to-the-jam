using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Engine : MonoBehaviour
{
    private static Engine instance;
    public static Engine Instance{
        get{
            if(instance == null)instance = FindObjectOfType<Engine>();
            return instance;
        }
    }
    public static int points = 000;
    public static int buffsPicked = 0;
    public static int experienceGained = 0;
    public static int mobsKilled = 0;
    public static int enemiesWave = 0;
    int increase = 1;
    Timer spawnTimer;
    public List<Buff> buffsAvaliable = new List<Buff>();
    public void Restart(){
        buffsAvaliable = new List<Buff>();
        points = 0;
        buffsPicked = 0;
        experienceGained = 0;
        mobsKilled = 0;
        enemiesWave = 0;
    }
    private void Start() {
        buffsAvaliable.Add(new AtkSpeedBuff());
        buffsAvaliable.Add(new DamageBuff());
        buffsAvaliable.Add(new HealthBuff());
        buffsAvaliable.Add(new ShieldBuff());
        buffsAvaliable.Add(new MoveSpeedBuff());
        buffsAvaliable.Add(new GloveBuff());
        buffsAvaliable.Add(new SkullBuff());
        buffsAvaliable.Add(new ShurikenBuff());
        buffsAvaliable.Add(new SpearBuff());
        buffsAvaliable.Add(new MirrorBuff());
        buffsAvaliable.Add(new ArrowBuff());
        spawnTimer = new Timer(7, true);
        spawnTimer.OnComplete += SpawnEnemy;
        ResourceController.StartSets();
    }
    private void Update() {
        if(StateController.Instance.currentState == States.GAME_IDLE)return;
        GameHud.Instance.SetText((int)enemiesWave);
        spawnTimer.Update();
    }
    void SpawnEnemy(){
        GameHud.Instance.nextWaveAnim.SetTrigger("Take");
        enemiesWave++;
        var quint = enemiesWave % 5;
        var dez = enemiesWave % 10;
        if(quint == 0){
            var rnd = Random.Range(0, EntityManager.Instance.enemies.Length);
            var e = PoolsManager.Instance.GetPool("EntityPool").GetFromPool(new Vector2(this.transform.position.x + Random.Range(-25, 25), this.transform.position.y + Random.Range(-15,15)));
            e.GetComponent<Entity>().Initialize(EntityManager.Instance.enemies[rnd], 2);
        }
        if(dez == 0){
            increase++;
            var rnd = Random.Range(0, EntityManager.Instance.enemies.Length);
            var e = PoolsManager.Instance.GetPool("EntityPool").GetFromPool(new Vector2(this.transform.position.x + Random.Range(-25, 25), this.transform.position.y + Random.Range(-15,15)));
            e.GetComponent<Entity>().Initialize(EntityManager.Instance.enemies[rnd], 3);
            return;
        }
        for (int i = 0; i < (enemiesWave * 1); i++)
        {
            var rnd = Random.Range(0, EntityManager.Instance.enemies.Length);
            var e = PoolsManager.Instance.GetPool("EntityPool").GetFromPool(new Vector2(this.transform.position.x + Random.Range(-25, 25), this.transform.position.y + Random.Range(-15,15)));
            e.GetComponent<Entity>().Initialize(EntityManager.Instance.enemies[rnd], 1, increase);
        }
    }
    public List<int> GetRnd(int amount, int max){
        List<int> localList = new List<int>();
        for (int i = 0; i < amount; i++)
        {
            var n = Random.Range(0,max);
            while(localList.Contains(n)){
                n = Random.Range(0,max);
            }
            localList.Add(n);
        }
        return localList;
    }
}
