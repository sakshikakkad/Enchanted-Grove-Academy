// Author: Alex Soong
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameStarter : MonoBehaviour
{
    public GameObject introUI;

    public void StartGame()
    {
        SceneManager.LoadScene("Main");
        Time.timeScale = 1f;
        introUI = GameObject.Find("Intro1");
        introUI.SetActive(true);
    }
}
