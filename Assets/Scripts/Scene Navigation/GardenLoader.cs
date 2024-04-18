using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GardenLoader : MonoBehaviour
{
    public LoadingScene ls;
    private bool isCooldown = false;
    private float cooldownTime = 2.0f;  // Cooldown time in seconds

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("colliding");
        if (!isCooldown && collision.gameObject.CompareTag("Player"))
        {
            ls.LoadScene(2);
            StartCoroutine(Cooldown());
        }
    }

    IEnumerator Cooldown() {
        isCooldown = true;
        yield return new WaitForSeconds(cooldownTime);
        isCooldown = false;
    }
}
