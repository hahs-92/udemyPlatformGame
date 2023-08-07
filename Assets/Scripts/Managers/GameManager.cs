using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int difficulty;
    public float timer;
    public bool startTimer;
    public int levelNumber;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        //PlayerPrefs.SetInt("Coins", 50);

        if(difficulty == 0)
        {
            difficulty = PlayerPrefs.GetInt("GameDifficulty", 1);
        }

        Debug.Log("Best Time " + PlayerPrefs.GetFloat("Level" + levelNumber + "BestTime"));
    }

    private void Update()
    {
        //if(Input.GetKeyUp(KeyCode.L))
        //{
        //    var coins = PlayerPrefs.GetInt("Coins");
        //    Debug.Log(coins);
        //}

        if(startTimer)
        {
            timer += Time.deltaTime;
        }
    }

    public void SaveDifficulty()
    {
        PlayerPrefs.SetInt("GameDifficulty", difficulty);
    }

    public void SaveBestTime()
    {
        startTimer = true;
        //float lastTime = PlayerPrefs.GetFloat("Level" + levelNumber + "BestTime");
        PlayerPrefs.SetFloat("Level" + levelNumber + "BestTime", timer);
        timer = 0;
    }

    public void SaveCollectedFruits()
    {
        int totalFruits = PlayerPrefs.GetInt("TotalFruitsCollected");
        int newTotalFruits = totalFruits + PlayerManager.instance.fruits;

        PlayerPrefs.SetInt("TotalFruitsCollected", newTotalFruits);
        PlayerPrefs.SetInt("Level" + levelNumber + "FruitsCollected", PlayerManager.instance.fruits);
        PlayerManager.instance.fruits = 0;

        Debug.Log("Total fruits: "+ newTotalFruits);
    }

    public void SaveLevelInfo()
    {
        int nextlevelNumber = levelNumber + 1;
        PlayerPrefs.SetInt("Level" + nextlevelNumber + "Unlocked", 1);
    }
}
