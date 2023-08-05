using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public string sceneToLoad;


    public void Loadlevel(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    } 
}
