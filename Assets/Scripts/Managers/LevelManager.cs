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
        PlayerPrefs.SetInt("Level" + 1 + "Unlocked", 1);
        AssignLevelBoleans();

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

    public void LoadNewGame()
    {
        for (int i = 1; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            bool unLocked = PlayerPrefs.GetInt("Level" + i + "Unlocked") == 1;

            if(unLocked)
            {
                PlayerPrefs.SetInt("Level" + i + "Unlocked", 0);
            } else
            {
                SceneManager.LoadScene("Level1");
                return;
            }
        }
    }

    public void LoadContinueGame()
    {
        for (int i = 1; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            bool unLocked = PlayerPrefs.GetInt("Level" + i + "Unlocked") == 1;

            if (!unLocked)
            {
                SceneManager.LoadScene("Level" + (i - 1));
            }
        }
    }

    private void AssignLevelBoleans()
    {
        for (int i = 1; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            bool unLocked = PlayerPrefs.GetInt("Level" + i + "Unlocked") == 1;

            if (unLocked)
            {
                levelOpen[i] = true;
            }
            else
            {
                return;
            }
        }
    }
}
