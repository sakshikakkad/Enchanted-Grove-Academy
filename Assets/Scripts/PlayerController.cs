using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f; // Adjust this to change the movement speed
    public float flySpeed = 10f; // Adjust this to change the flying speed
    public float maxVerticalVelocity = 10f; // Maximum vertical velocity allowed
    public float yLimit = 10f; // The maximum allowed Y position

    Rigidbody rb;

    void Start()
    {
        // Get the Rigidbody component attached to the capsule
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Check if the spacebar is being held down
        if (Input.GetKey(KeyCode.Space))
        {
            // Get input from the WASD keys
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");

            // Calculate the movement direction
            Vector3 moveDirection = new Vector3(horizontalInput, flySpeed, verticalInput).normalized;

            // Move the capsule in the calculated direction
            rb.velocity = moveDirection * flySpeed;

            rb.velocity = new Vector3(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -maxVerticalVelocity, maxVerticalVelocity), rb.velocity.z);
        }
        else
        {
            // Get input from the WASD keys
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");

            // Calculate the movement direction
            Vector3 moveDirection = new Vector3(horizontalInput, 0f, verticalInput).normalized;

            // Move the capsule in the calculated direction
            transform.Translate(moveDirection * speed * Time.deltaTime);
        }
    }
}
