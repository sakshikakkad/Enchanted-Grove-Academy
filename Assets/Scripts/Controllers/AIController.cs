//author: Alina Polyudova
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


// [RequireComponent(typeof(Collider))]
public class AIController : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    public Animator animator;
    public GameObject player;
    private NavMeshHit hit;
    private Vector3 somePoint;
    private bool justKilled;
    public bool hitPlayer;
    private float idleWaitTime;
    private float timer;
    public int spiderLives;
    public bool canAttack;
    public float attackCooldown;
    public float nextAttackTime;
    // public AudioSource spiderGetsHurtAudioSource;
    // public AudioClip spiderGetsHurtClip;

    // Change max distance to player if needed (for attacking)
    float maxDistance = 17;

    public enum AIState
    {
        Idle,
        Chase,
        Attack,
        TakeDamage,
        Retreat,
        Die
    };
    public AIState aiState;
   
    // Start is called before the first frame update
    void Start()
    {
        aiState = AIState.Chase;
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        //somePoint = new Vector3(0,0,0);
        justKilled = false;
        hitPlayer = false;
        // this.GetComponent<Collider>().enabled = true;
        idleWaitTime = 2f;
        timer = 0f;
        spiderLives = 3;
        canAttack = true;
        attackCooldown = 3f;
        nextAttackTime = 0f;

        // set player here so you don't have to in Inspector
        player = GameObject.FindGameObjectWithTag("Player");
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        bool inRange = playerInAttackRange();

        //update canAttack value
        if (Time.time >= nextAttackTime)
        {
            canAttack = true;
        }

        // attack player after idle wait time
        if (aiState == AIState.Idle)
        {
            timer += Time.fixedDeltaTime;

            if (timer >= idleWaitTime)
            {
                timer = 0f;
                if (aiState != AIState.Die && inRange && player.GetComponent<InputController>().Click) {
                    aiState = AIState.TakeDamage;
                } else {
                    aiState = AIState.Attack;
                }
            }

        }

        // set spider to attack if within distance
        if (aiState == AIState.Chase && inRange && !justKilled)
        {
           aiState = AIState.Attack;
        } 

        // set spider to chase if farther away
        if ((aiState == AIState.Attack || aiState == AIState.TakeDamage) && !inRange) {
            aiState = AIState.Chase;
        }

        // set player attack spider anims (player and spider)
        if (aiState != AIState.Die && inRange && player.GetComponent<InputController>().Click && canAttack) {
            // do player attack animation
            player.GetComponent<Animator>().SetTrigger("AttackTrigger");
            aiState = AIState.TakeDamage;  
            spiderLives--;
            canAttack = false;
            nextAttackTime = Time.time + attackCooldown;
        }

        if (spiderLives <= 0) {
            aiState = AIState.Die;
            justKilled = true;
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
                // transform.LookAt(player.transform.position); 
                if (animator != null)
                {
                    animator.Play("Base Layer.Attack1");

                    float animationTime = (animator.GetCurrentAnimatorStateInfo(0).normalizedTime); //Mathf.Floor

                    // check when animation finishes
                    if (animationTime >= 0.8 && animationTime < 2 && !hitPlayer)
                    {
                        hitPlayer = true;
                        aiState = AIState.Idle;
                    }
                        // if (!hitPlayer) {
                        //     StartCoroutine(PerformActionAfterAnimation());
                        // }
                };
                break;
            case AIState.TakeDamage:
                if (animator != null)
                {
                    animator.Play("Base Layer.TakeDamage_002");

                    // Debug.Log("take damage");

                    // if (animator != null) {
                    //     spiderGetsHurtAudioSource.PlayOneShot(spiderGetsHurtClip);

                    // }


                    float animationTime = Mathf.Floor(animator.GetCurrentAnimatorStateInfo(0).normalizedTime);

                    //check when animation finishes
                    if (animationTime == 1)
                    {
                        aiState = AIState.Idle;
                    }
                };
                
                break;
            // case AIState.Retreat:
            //     navMeshAgent.SetDestination(somePoint);
            //     if (animator != null)
            //     {
            //         animator.Play("Base Layer.Walk");
            //     };
            //     break;
            case AIState.Die:
                if (animator != null)
                {
                    Vector3 backup = new Vector3(navMeshAgent.transform.position.x, navMeshAgent.transform.position.y, navMeshAgent.transform.position.z - 20);
                    navMeshAgent.SetDestination(backup);
                    animator.Play("Death");
                    StartCoroutine(Dying());
                };
                break;
            default:
                break;
        }
    }



    IEnumerator Dying()
    {
        yield return new WaitForSeconds(2.0f);
        gameObject.SetActive(false);
    }


    // public Vector3 RandomPointOnNavMesh()
    // {
    //     Vector3 randomPoint = navMeshAgent.transform.position + Random.insideUnitSphere * 10f;
    //     for (int i = 0; i < 30; i++) {
    //         if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
    //         {
    //             return hit.position;
    //         }
    //     }
    //     return navMeshAgent.transform.position;
    // }


    public Vector3 calculateDestination()
    {
        float dist = Vector3.Distance(navMeshAgent.transform.position, player.transform.position);
        float lookAheadT = Mathf.Clamp(dist / (navMeshAgent.speed), 0.1f, 1f);
        Vector3 futureTarget = player.transform.position + lookAheadT * player.GetComponent<VelocityReporter>().velocity;
        return futureTarget;
    }

    // returns true if player is within the maxDistance
    public bool playerInAttackRange()
    {
        return Vector3.Distance(navMeshAgent.transform.position, player.transform.position) <= maxDistance;
    }

    // IEnumerator PerformActionAfterAnimation()
    // {
    //     yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
    //     if (!hitPlayer)
    //     {
    //         hitPlayer = true;
    //         aiState = AIState.Idle;
    //     }
    // }

}



