using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GardenLoader : MonoBehaviour
{
    public LoadingScene ls;
    private bool loaded = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (!loaded && collision.gameObject.CompareTag("Player"))
        {
            ls.LoadScene(2);
            loaded = true;
        }
    }

    IEnumerator Cooldown() {
        isCooldown = true;
        yield return new WaitForSeconds(cooldownTime);
        isCooldown = false;
    }
}
