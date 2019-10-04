using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{

    public static int PrevScene;
    public static int NextScene;

    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
        PrevScene = SceneManager.GetActiveScene().buildIndex - 1;
        NextScene = SceneManager.GetActiveScene().buildIndex + 1;
    }
}
