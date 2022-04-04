using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Engine : MonoBehaviour
{
    public static int points = 000;
    public static int buffsPicked = 0;
    public static int experienceGained = 0;
    public static int mobsKilled = 0;
    int enemiesWave = 0;
    Timer spawnTimer;
    private void Start() {
        spawnTimer = new Timer(10, true);
        spawnTimer.OnComplete += SpawnEnemy;
    }
    private void Update() {
        if(StateController.Instance.currentState == States.GAME_IDLE)return;
        GameHud.Instance.SetText((int)spawnTimer.reverseElaped);
        spawnTimer.Update();
    }
    void SpawnEnemy(){
        GameHud.Instance.nextWaveAnim.SetTrigger("Take");
        enemiesWave++;
        var quint = enemiesWave % 5;
        var dez = enemiesWave % 10;
        if(quint == 0){
            var rnd = Random.Range(0, EntityManager.Instance.enemies.Length);
            var e = PoolsManager.Instance.GetPool("EntityPool").GetFromPool(new Vector2(this.transform.position.x + Random.Range(-15, 15), this.transform.position.y + Random.Range(-15,15)));
            e.GetComponent<Entity>().Initialize(EntityManager.Instance.enemies[rnd], 2);
        }
        if(dez == 0){
            var rnd = Random.Range(0, EntityManager.Instance.enemies.Length);
            var e = PoolsManager.Instance.GetPool("EntityPool").GetFromPool(new Vector2(this.transform.position.x + Random.Range(-15, 15), this.transform.position.y + Random.Range(-15,15)));
            e.GetComponent<Entity>().Initialize(EntityManager.Instance.enemies[rnd], 3);
            return;
        }
        for (int i = 0; i < enemiesWave; i++)
        {
            var rnd = Random.Range(0, EntityManager.Instance.enemies.Length);
            var e = PoolsManager.Instance.GetPool("EntityPool").GetFromPool(new Vector2(this.transform.position.x + Random.Range(-15, 15), this.transform.position.y + Random.Range(-15,15)));
            e.GetComponent<Entity>().Initialize(EntityManager.Instance.enemies[rnd], 1);
        }
    }
}
