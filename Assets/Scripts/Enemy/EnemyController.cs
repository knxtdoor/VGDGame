
using System;
using UnityEngine;
using UnityEngine.AI;

public enum EnemyState
{
    Patrolling,
    Chasing
}

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyController : MonoBehaviour
{

    public float speed = 1f;
    public float rotateSpeed = 220f;
    public Transform player;

    public Vector3 pointA = new Vector3(0, 1, 15); //location to move from
    public Vector3 pointB = new Vector3(10, 1, 15); //location to move towrds
    private Vector3 moveTo;
    private Vector3 lookTo;

    private GameObject huntTarget = null;

    private Mesh mesh;

    public float visionRange = 5f;
    public float FOV = 120f;

    private NavMeshAgent navMeshAgent;
    private EnemyState currentState;
    private VelocityReporter velocityReporter;
    private Animator anim;
    public float maxLookaheadTime = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.speed = speed;
        currentState = EnemyState.Patrolling;
        moveTo = pointB;
        lookTo = pointB;
        SetNextPatrolPoint();
        velocityReporter = player.GetComponent<VelocityReporter>();
        anim = GetComponent<Animator>();
        anim.SetBool("active", true);

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        switch (currentState)
        {
            case EnemyState.Patrolling:
                Patrol();
                break;
            case EnemyState.Chasing:
                Chase();
                break;
        }
    }

    private void SetNextPatrolPoint()
    {
        if (moveTo == pointB)
        {
            moveTo = pointA; // Move towards the start position
            lookTo = pointA;
        }
        else
        {
            moveTo = pointB; // Move towards the end position
            lookTo = pointB;

        }

        navMeshAgent.SetDestination(moveTo);
    }

    private void Patrol()
    {
        if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance < 0.5f)
        {
            SetNextPatrolPoint();
        }

        CheckForTarget();
    }

    private void Chase()
    {
        if (huntTarget != null)
        {
            float distanceToTarget = Vector3.Distance(transform.position, huntTarget.transform.position);
            float lookAheadTime = Mathf.Clamp(distanceToTarget / navMeshAgent.speed, 0, maxLookaheadTime);
            Vector3 predictedDestination = huntTarget.transform.position + (velocityReporter.velocity * lookAheadTime);

            navMeshAgent.SetDestination(predictedDestination);

            if (distanceToTarget > visionRange)
            {
                huntTarget = null;
                currentState = EnemyState.Patrolling;
                SetNextPatrolPoint();
            }
        }
        else
        {
            huntTarget = null;
            currentState = EnemyState.Patrolling;
            SetNextPatrolPoint();
        }
    }

    void CheckForTarget()
    {
        float degreeIncrement = Mathf.PI / 8;
        float facingDirection = Mathf.Atan2(this.transform.forward.z, this.transform.forward.x);
        float FOVrad = Mathf.Deg2Rad * FOV;

        for (float i = facingDirection - (FOVrad / 2); i < facingDirection + (FOVrad / 2); i += degreeIncrement)
        {
            Vector3 currDirection = new Vector3(Mathf.Cos(i), 0, Mathf.Sin(i));
            RaycastHit rayHit;
            if (Physics.Raycast(transform.position, currDirection, out rayHit, visionRange))
            {
                string collisionTag = rayHit.collider.gameObject.tag;
                if (collisionTag == "Player" || collisionTag == "Hologram")
                {
                    Debug.Log("Player/Hologram in field of vision");
                    huntTarget = rayHit.collider.gameObject;
                    currentState = EnemyState.Chasing;
                    break;
                }
            }
        }
    }
}
