using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainSceneLoader : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("alex_main_3_7_24");
        Time.timeScale = 1f;
    }
}