using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeController : MonoBehaviour
{
    public int lifeCount = 3;

    // UI elements for each life bar
    public GameObject life1;
    public GameObject life2;
    public GameObject life3;

    // audio
    public AudioSource ouchAudioSource;
    public AudioClip ouchClip;

    public void removeLife()
    {
        lifeCount--;
        ouchAudioSource.PlayOneShot(ouchClip);
        UpdateLifeUI();
    }

    void UpdateLifeUI()
    {
        if (lifeCount == 2)
        {
            life3.SetActive(false);
        } 
        
        else if (lifeCount == 1) 
        {
            life3.SetActive(false);
            life2.SetActive(false);
        }

        else if (lifeCount == 0)
        {
            life3.SetActive(false);
            life2.SetActive(false);
            life1.SetActive(false);
        }
    }
}
