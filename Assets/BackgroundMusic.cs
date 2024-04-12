using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    private AudioSource audioSource;

    void Start()
    {
        // Get the AudioSource component attached to this GameObject
        audioSource = GetComponent<AudioSource>();

        if (audioSource) {
            Debug.Log("audio source exists");
        }

        // Play the audio
        audioSource.Play();

    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
