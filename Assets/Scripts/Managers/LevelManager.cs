using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private GameObject levelButton;
    [SerializeField] private Transform parent;
    [SerializeField] private bool[] levelOpen;


    private void Start()
    {
        for (int i = 1; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            if (!levelOpen[i]) return;

            string sceneName = "Level" + i;
            GameObject newButton = Instantiate(levelButton, parent);
            newButton.GetComponent<Button>().onClick.AddListener(() => Loadlevel(sceneName));
            //newButton.AddComponent<Button>().onClick.AddListener(() => Loadlevel(sceneName));
            newButton.GetComponentInChildren<TextMeshProUGUI>().text= sceneName;
        }
    }

    public void Loadlevel(string sceneName)
    {
        GameManager.instance.SaveDifficulty();
        SceneManager.LoadScene(sceneName);
    } 
}
