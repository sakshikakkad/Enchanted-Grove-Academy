using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainLoader : MonoBehaviour
{

    public void MainScene() {
        SceneManager.LoadScene("Main");
        Time.timeScale = 1f;
    }
}
