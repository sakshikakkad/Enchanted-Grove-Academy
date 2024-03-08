using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f; // Adjust this to change the movement speed
    public float flySpeed = 10f; // Adjust this to change the flying speed
    public float maxVerticalVelocity = 10f; // Maximum vertical velocity allowed
    public float yLimit = 10f; // The maximum allowed Y position
    // public GameObject pixieDustBar;
    // public float pixieDust;
    public float attackRange;
    public bool isAttacking;
    public GameObject enemy;

    Rigidbody rb;

    void Start()
    {
        // Get the Rigidbody component attached to the capsule
        rb = GetComponent<Rigidbody>();
        attackRange = 7f;
        isAttacking = false;
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
        // PixieDustSystem pixieDustSystem = pixieDustBar.GetComponent<PixieDustSystem>();
        // pixieDust = pixieDustSystem.updatedPixieDust;
        // Debug.Log("current pixie dust: " + pixieDust);

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, Mathf.Infinity) && hit.collider.gameObject.layer == LayerMask.NameToLayer("Default"))
            {
                // Check if the enemy is within the attack range
                NavMeshAgent enemyNavMeshAgent = hit.transform.GetComponent<NavMeshAgent>();
                if (enemyNavMeshAgent != null) {
                    float distanceToEnemy = Vector3.Distance(transform.position, hit.transform.position);
                    if (distanceToEnemy <= attackRange)
                    {
                        isAttacking = true;
                        Debug.Log("Attacking!");
                        StartCoroutine(Cooldown());
                    }
                }
            }
        }
    }
    //waits a few seconds before switching isAttacking back to false
    IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(1f);
        isAttacking = false;
    }
}
