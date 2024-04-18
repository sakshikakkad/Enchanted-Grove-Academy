using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainLoader : MonoBehaviour
{
    // Is called after the garden minigame
    public void LoadMain() {
        SceneManager.LoadScene("Main");
        Time.timeScale = 1f;
    }
}
