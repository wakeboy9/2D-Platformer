using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseMenu : MonoBehaviour
{
    public GameObject myLevelHandler;
    public void Start()
    {
        myLevelHandler = GameObject.FindGameObjectWithTag("LevelHandler");
    }

    public void RestartLevel()
    {
        //PlayerController.myActiveScene
        SceneManager.LoadScene(myLevelHandler.PrevScene);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

}
