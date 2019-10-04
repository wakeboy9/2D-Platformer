using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    public int PrevScene = SceneManager.GetActiveScene().buildIndex - 1;
    public int NextScene = SceneManager.GetActiveScene().buildIndex + 1;

    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }

}
