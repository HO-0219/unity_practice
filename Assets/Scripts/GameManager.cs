using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    //----------------------------------
    [Header("# Player Info")]
    public int playerId;
    public int level;
    public int kill;
    public int exp;
    public int[] nextExp = {3,5,10,30,60,100,150,210,280, 360, 450, 600};
    public float health;
    public float maxHealth = 100;

    //----------------------------------
    [Header("# Game Control")]
    public float gameTime;
    public float maxGameTime = 60f;
    public bool isLive;
    //----------------------------------
    [Header("# Game Objcet")]
    public PoolManager pool;
    public Player player;
    public LevelUp uiLevelUp;
    public Result uiResult;

    public GameObject enemyCleaner;

    void Awake(){
        instance = this;

      
    }
    public void GameStart(int id) {
        playerId = id;
        health= maxHealth;   
        player.gameObject.SetActive(true);
        uiLevelUp.Select(id%2);  
        Resume();
    }
    public void GameOver(){
        StartCoroutine(GameOverRoutine());

    }
    IEnumerator GameOverRoutine(){
        isLive = false;
        yield return new WaitForSeconds(1f);
        uiResult.gameObject.SetActive(true);
        uiResult.Lose();

        Stop();
    }

    public void GameVictroy(){
        StartCoroutine(GameVictroyRoutine());
    }
    IEnumerator GameVictroyRoutine(){
        isLive = false;
        enemyCleaner.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        uiResult.gameObject.SetActive(true);
        uiResult.Win();
        
        Stop();
    }
    public void GameRetry(){
        SceneManager.LoadScene("InGameSene");
    }
    void Update() {
        if(!isLive)
            return;

        gameTime += Time.deltaTime;
        if(gameTime > maxGameTime){
            gameTime = maxGameTime;
            GameVictroy();
        }    
    }
    public void GetExp(){
        if(!isLive) 
            return;

        exp++;

        if(exp == nextExp[Mathf.Min(level, nextExp.Length-1)]){
            //level up logic
            level++;

            exp = 0;
            uiLevelUp.Show();
        }
    }

    public void Stop(){
        isLive = false;
        Time.timeScale = 0;
    }
    public void Resume(){
        isLive = true;
        Time.timeScale = 1; // 2나 3으로 하면 2배 3배속
    }


}
