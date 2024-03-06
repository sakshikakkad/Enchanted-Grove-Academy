using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIController : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    private Animator animator;
    public GameObject player;
    private NavMeshHit hit;

    public enum AIState
    {
        Idle,
        Chase,
        Attack,
        Retreat
    };
    public AIState aiState;
    
    // Start is called before the first frame update
    void Start()
    {
        aiState = AIState.Chase;
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();   
    }

    // Update is called once per frame
    void Update()
    {
        // if ()
        // {
            
        // } 
        
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
            // case AIState.ChaseMovingWayPoint:
            //     animator.SetFloat("vely", navMeshAgent.velocity.magnitude / navMeshAgent.speed);
            //     Vector3 futureTarget = calculateDestination();
            //     if (!NavMesh.Raycast(navMeshAgent.transform.position, futureTarget, out hit, NavMesh.AllAreas))
            //     {
            //         navMeshAgent.SetDestination(futureTarget);
            //     }
            //     break;
            default:
                break;
        }
    }

    public Vector3 calculateDestination()
    {
        float dist = (navMeshAgent.transform.position - player.transform.position).magnitude;
        float lookAheadT = Mathf.Clamp(dist / (navMeshAgent.speed), 0.1f, 10f);
        Vector3 futureTarget = player.transform.position + lookAheadT * player.GetComponent<VelocityReporter>().velocity;
        return futureTarget;
    }
}
