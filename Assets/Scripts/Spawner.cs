using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    
    public Transform[] spawnPoints;
    public SpawnData[] spawnData;
    float timer;
    int level;

    void Awake() {
        spawnPoints = GetComponentsInChildren<Transform>();   
    }

    void Update() {
        if(!GameManager.instance.isLive)
            return;

        timer += Time.deltaTime;
//        level = Mathf.FloorToInt(GameManager.instance.gameTime / 60f);
        level = Mathf.Min(Mathf.FloorToInt(GameManager.instance.gameTime / 60f), spawnData.Length -1);
        if(timer > spawnData[level].spawnTime){
            timer = 0;
            Spawn();
         
        }
    }
    void Spawn(){
    GameObject enemy = GameManager.instance.pool.GetObject(0);
    enemy.transform.position = spawnPoints[Random.Range(1, spawnPoints.Length)].position;
    enemy.GetComponent<Enemy>().Init(spawnData[level]);

    }
    
}

[System.Serializable]
public class SpawnData{
    public int spriterType;
    public float spawnTime;
    public int health;
    public float speed;

}