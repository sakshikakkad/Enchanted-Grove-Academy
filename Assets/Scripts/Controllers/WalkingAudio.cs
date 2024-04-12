using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingAudio : MonoBehaviour
{    
    public AudioSource soundEffect;

    // This function is called by the Animation Event
    public void PlayFootstepSound()
    {
        // Play the footstep sound
        soundEffect.Play();
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
