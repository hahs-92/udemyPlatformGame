using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int difficulty;

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
        PlayerPrefs.SetInt("Coins", 50);
    }

    private void Update()
    {
        if(Input.GetKeyUp(KeyCode.L))
        {
            var coins = PlayerPrefs.GetInt("Coins");
            Debug.Log(coins);
        }
    }
}
