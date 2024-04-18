// Author: Alex Soong
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuestLoader : MonoBehaviour
{
    private static bool active = false;
    public LoadingScene ls;
    private bool isCooldown = false;
    private float cooldownTime = 2.0f;  // Cooldown time in seconds

    public void Quest() {
        ls.LoadScene(3);
        Time.timeScale = 1f;
    }

    private void OnCollisionEnter(Collision collision)
    {

        if (active == true && !isCooldown && collision.gameObject.CompareTag("Player"))
        {
            Quest();
            StartCoroutine(Cooldown());
        }
    }
    public static void Activate() {
        active = true;
    }
    IEnumerator Cooldown() {
        isCooldown = true;
        yield return new WaitForSeconds(cooldownTime);
        isCooldown = false;
    }
}