// Author: Alex Soong
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuestLoader : MonoBehaviour
{
    private static bool active = false;

    public void Quest() {
        SceneManager.LoadScene("Quest");
        Time.timeScale = 1f;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (active == true && collision.gameObject.CompareTag("Player"))
        {
            Quest();
        }
    }
    public static void Activate() {
        active = true;
    }
}