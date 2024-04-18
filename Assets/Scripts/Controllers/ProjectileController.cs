using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Author: Sakshi Kakkad

[RequireComponent(typeof(PlayerController))]
public class ProjectileController : MonoBehaviour
{
    public float launchVelocity = 100f;
    public GameObject projectile; // SET IN INSPECTOR
    PlayerController player;
    Vector3 height = new Vector3(0, 10, 0);

    // audio
    public AudioSource attackAudioSource;
    public AudioClip attackClip;

    void Start()
    {
        player = GetComponent<PlayerController>();
    }
    // Update is called once per frame
    void Update()
    {
        if (player._inputClick)
        {
            // play audio for player attacking here
            attackAudioSource.PlayOneShot(attackClip);

            // create projectile
            GameObject fireball = Instantiate(projectile, transform.position + height, transform.rotation, transform);
            
            // launch projectile
            Vector3 velocity = new Vector3(0, 0, 1) * launchVelocity;
            fireball.GetComponent<Rigidbody>().AddRelativeForce(velocity);

            // destroy projectile after delay
            Destroy(fireball, 5f);
        }
    }
}
