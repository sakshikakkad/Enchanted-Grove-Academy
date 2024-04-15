// Author: Alex Soong
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameStarter : MonoBehaviour
{

    public void StartGame()
    {
        SceneManager.LoadScene("Main");
        Time.timeScale = 1f;
    }
}
