using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    public static string prevScene;
    public static string currentScene;
    public static string nextScene;
    int level = 1;

    public static LevelController instance;

    void Awake()
    {
        DontDestroyOnLoad(this);
        prevScene = SceneManager.GetActiveScene().name;
        currentScene = SceneManager.GetActiveScene().name;
        getNextScene();
    }

    void Update()
    {
        if(SceneManager.GetActiveScene().name == "Lose" && currentScene != "Lose") {
            prevScene = currentScene;
            currentScene = "Lose";
        } else if(SceneManager.GetActiveScene().name != currentScene && SceneManager.GetActiveScene().name != "Lose") {
            prevScene = currentScene;
            Debug.Log(prevScene);
            currentScene = SceneManager.GetActiveScene().name;
            getNextScene();
        }
    }

    void getNextScene(){
        if (currentScene != "Lose")
        {
            int.TryParse(currentScene.Substring(currentScene.Length - 1), out level);
            nextScene = "Level" + (level + 1);
        }
    }
}
