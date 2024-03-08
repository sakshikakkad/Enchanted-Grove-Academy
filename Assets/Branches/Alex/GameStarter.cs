using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameStarter : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        // TODO we will have to chain this to just Main for when we merge into main branch (out of Alex branch)
        SceneManager.LoadScene("alex_main_3_7_24");
        Time.timeScale = 1f;
    }
}
