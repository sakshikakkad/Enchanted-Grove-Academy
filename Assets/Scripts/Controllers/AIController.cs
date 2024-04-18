//author: Sakshi, Alina
using System.Collections;
using System.Collections.Generic;
using System.Net;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;


// [RequireComponent(typeof(Collider))]
public class AIController : MonoBehaviour
{

    // components
    private NavMeshAgent navMeshAgent;
    public Animator animator;
    public GameObject player;

    // spider helper vars
    float attackCooldown;
    float nextAttackTime;
    private NavMeshHit hit;

    // public AudioSource spiderGetsHurtAudioSource;
    // public AudioClip spiderGetsHurtClip;

    // Change distance to player if needed (for attacking/chasing)
    public float attackRange = 20;
    public float chaseRange = 250;

    public enum AIState
    {
        Idle,
        Chase,
        Attack,
        Die
    };
    public AIState aiState;
   
    // Start is called before the first frame update
    void Start()
    {

        // Set initial state to idle
        aiState = AIState.Idle;

        // reference components
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        // set vars
        attackCooldown = 5f;
        nextAttackTime = 0;
        

        // set player here so you don't have to in Inspector
        player = GameObject.FindGameObjectWithTag("Player");
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        // if player is in attack range, set spider to attack
        if (playerInRange(attackRange) && aiState != AIState.Die)
        {
            if (Time.time >= nextAttackTime)
            {
                aiState = AIState.Attack;
                Debug.Log("set ai state to attack");
            }
            else
            {
                aiState = AIState.Idle;
            }      
        } 
        
        // if player in chase range, set spider to chase
        else if (playerInRange(chaseRange) && aiState != AIState.Die)
        {
            aiState = AIState.Chase;
        }

        else if (aiState != AIState.Die)
        {
            aiState = AIState.Idle;
        }

        // spider animations
        switch (aiState)
        {
            case AIState.Idle:
                if (animator != null)
                {
                    animator.Play("Base Layer.Idle");
                };
                break;

            case AIState.Chase:
                if (animator != null)
                {
                    animator.Play("Base Layer.Walk");
                };
                Vector3 futureTarget = calculateDestination();
                if (!NavMesh.Raycast(navMeshAgent.transform.position, futureTarget, out hit, NavMesh.AllAreas))
                {
                    navMeshAgent.SetDestination(futureTarget);
                }
                break;

            case AIState.Attack:
                if (animator != null)
                {
                    Debug.Log("playing attack animation)");
                    animator.Play("Base Layer.Attack1");

                    float animationTime = Mathf.Floor(animator.GetCurrentAnimatorStateInfo(0).normalizedTime);

                    //check when animation finishes
                    if (animationTime == 1)
                    {
                        Debug.Log("attack animation finished, removing player life");
                        nextAttackTime = Time.time + attackCooldown;
                        player.GetComponent<LifeController>().removeLife();
                    }
                };
                break;

            case AIState.Die:
                if (animator != null)
                {
                    animator.Play("Death");

                    float animationTime = Mathf.Floor(animator.GetCurrentAnimatorStateInfo(0).normalizedTime);

                    if (animationTime == 1)
                    {
                        Destroy(this.GameObject());
                    }
                    
                };
                break;

            default:
                break;
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.transform.gameObject.tag == "projectile")
        {
            Debug.Log("hit spider");
            aiState = AIState.Die;
        }
    }

    public Vector3 calculateDestination()
    {
        float dist = Vector3.Distance(navMeshAgent.transform.position, player.transform.position);
        float lookAheadT = Mathf.Clamp(dist / (navMeshAgent.speed), 0.1f, 1f);
        Vector3 futureTarget = player.transform.position + lookAheadT * player.GetComponent<VelocityReporter>().velocity;
        return futureTarget;
    }

    // returns true if player is within a certain distance (for attack and chase)
    public bool playerInRange(float distance)
    {
        return Vector3.Distance(navMeshAgent.transform.position, player.transform.position) <= distance;
    }

}



