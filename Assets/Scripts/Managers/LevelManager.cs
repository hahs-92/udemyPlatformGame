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


    private void Start()
    {
        for (int i = 1; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            string sceneName = "Level" + i;
            GameObject newButton = Instantiate(levelButton, parent);
            newButton.AddComponent<Button>().onClick.AddListener(() => Loadlevel(sceneName));
            newButton.GetComponentInChildren<TextMeshProUGUI>().text= sceneName;
        }
    }

    public void Loadlevel(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    } 
}
