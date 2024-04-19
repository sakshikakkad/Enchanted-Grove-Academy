// Author: Alex Soong
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuestLoader : MonoBehaviour
{
    private static bool active = false;
    public LoadingScene ls;
    private bool loaded = false;

    private void OnCollisionEnter(Collision collision)
    {

        if (active == true && collision.gameObject.CompareTag("Player"))
        {
            if (!loaded)
            {
                ls.LoadScene(3);
                loaded = true;
            }
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