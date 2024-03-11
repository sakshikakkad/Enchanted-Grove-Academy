//author: Alina Polyudova
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


[RequireComponent(typeof(Collider))]
public class AIController : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    public Animator animator;
    public GameObject player;
    private NavMeshHit hit;
    private Vector3 somePoint;
    private bool justKilled;
    public bool hitPlayer;
    private float idleWaitTime = 5f;
    private float timer = 0f;

    // Change max distance to player if needed (for attacking)
    float maxDistance = 50;

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
        somePoint = new Vector3(0,0,0);
        justKilled = false;
        hitPlayer = false;

        // set player here so you don't have to in Inspector - Sakshi
        player = GameObject.FindGameObjectWithTag("Player");
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        bool inRange = playerInAttackRange();

        // chase player after idle wait time
        if (aiState == AIState.Idle)
        {
            timer += Time.fixedDeltaTime;

            if (timer >= idleWaitTime)
            {
                Debug.Log("spider is done idling");
                timer = 0f;
                aiState = AIState.Attack;
            }

        }

        // set spider to attack if within distance
        if (aiState == AIState.Chase && inRange && !justKilled)
        {
           aiState = AIState.Attack;
        } 

        // set spider to chase if farther away
        if (aiState == AIState.Attack && !inRange) {
            aiState = AIState.Chase;
        }

        // set player attack spider anims (player and spider)
        if (aiState != AIState.Die && inRange && player.GetComponent<InputController>().Click) {
            // do player attack animation
            player.GetComponent<Animator>().SetTrigger("AttackTrigger");
            // kill spider (temporarily)
            aiState = AIState.Die;
            justKilled = true;
            StartCoroutine(ResetAfterDeath());
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
                //Debug.Log("current player pos: " + player.transform.position);
                //Debug.Log(this + "future target: " + futureTarget);
                if (!NavMesh.Raycast(navMeshAgent.transform.position, futureTarget, out hit, NavMesh.AllAreas))
                {
                    transform.LookAt(player.transform);
                    transform.Rotate(Vector3.up, 180f);
                    navMeshAgent.SetDestination(futureTarget);
                }
                break;
            case AIState.Attack:
                if (animator != null)
                {
                    transform.LookAt(player.transform);
                    transform.Rotate(Vector3.up, 180f);
                    animator.Play("Base Layer.Attack1");

                    float animationTime = Mathf.Floor(animator.GetCurrentAnimatorStateInfo(0).normalizedTime);

                    // check when animation finishes
                    if (animationTime == 1 && !hitPlayer)
                    {
                        Debug.Log("spider anim finished, gonna hit player");
                        hitPlayer = true;
                        aiState = AIState.Idle;
                    }
                };
                break;
            // case AIState.TakeDamage:
            //     if (animator != null)
            //     {
            //         animator.Play("Base Layer.TakeDamage_002");
            //     };
            //     somePoint = RandomPointOnNavMesh();
            //     aiState = AIState.Retreat;
            //     break;
            // case AIState.Retreat:
            //     transform.LookAt(player.transform);
            //     transform.Rotate(Vector3.up, 180f);
            //     navMeshAgent.SetDestination(somePoint);
            //     if (animator != null)
            //     {
            //         animator.Play("Base Layer.Walk");
            //     };
            //     break;
            case AIState.Die:
                if (animator != null)
                {
                    this.GetComponent<Collider>().enabled = false;
                    transform.LookAt(player.transform);
                    transform.Rotate(Vector3.up, 180f);
                    animator.Play("Death");
                };
                break;
            default:
                break;
        }
    }


    IEnumerator ResetAfterDeath()
    {
        this.GetComponent<Collider>().enabled = true;
        yield return new WaitForSeconds(6.0f); // Adjust the delay duration as needed
        justKilled = false;
        aiState = AIState.Chase;
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
        float dist = Vector3.Distance(navMeshAgent.transform.position,player.transform.position);
        float lookAheadT = Mathf.Clamp(dist / (navMeshAgent.speed), 0.1f, 10f);
        Vector3 futureTarget = player.transform.position + lookAheadT * player.GetComponent<VelocityReporter>().velocity;
        return futureTarget;
    }

    // returns true if player is within the maxDistance
    public bool playerInAttackRange()
    {
        return Vector3.Distance(navMeshAgent.transform.position, player.transform.position) <= maxDistance;
    }
}



