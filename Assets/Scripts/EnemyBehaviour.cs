using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour
{


    // patrol locations
    [Header("Patrol Points (positions)")]
    [SerializeField] Vector3[] patrolPoints;
    private int currentPoint;
    private bool loopingForward;
    /*[SerializeField] Vector3 point1;
    [SerializeField] Vector3 point2;
    [SerializeField] Vector3 point3;
    [SerializeField] Vector3 point4;
    [SerializeField] Vector3 point5;*/

    [Header("AI Properties")]
    [SerializeField] private float chaseRange;
    [SerializeField] private float attackRange;
    [SerializeField] private float searchTime;
    private float distanceFromPoint;
    private Vector3 currentTargetPoint;
    private float distanceFromTarget;
    private Vector3 lastSeenTargetPos;

    [Header("Player Object (Target to Chase)")]
    [SerializeField] GameObject player;

    private NavMeshAgent navMeshAgent;
    private State state;
    private MeshRenderer meshRenderer;

    // Start is called before the first frame update
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        currentPoint = 0;
        currentTargetPoint = patrolPoints[currentPoint];
    }

    // Update is called once per frame
    void Update()
    {
        distanceFromTarget = Vector3.Distance(transform.position, player.transform.position);

        switch (state)
        {
            case State.patrol:
                Patrol();
                if (TargetInChaseRange())
                {
                    state = State.chase;
                }
                break;

            case State.chase:
                Chase();
                if (!TargetInChaseRange())
                {
                    lastSeenTargetPos = player.transform.position; // setting last seen position for the search state
                    state = State.search;
                }
                else if (TargetInAttackRange())
                {
                    state = State.attack;
                }
                break;

            case State.retreat:
                Retreat();
                if (TargetInChaseRange())
                {
                    state = State.chase;
                }
                else if (ReturnedToPatrolPoint())
                {
                    state = State.patrol;
                }
                break;

            case State.attack:
                Attack();

                break;

            case State.search:
                Search();

                break;
        }
    }

    // -------------------------------------------------------------------------------------- private methods

    private void Patrol()
    {
        // patrol logic
        // ** change color here **
        meshRenderer.material.color = Color.green;
        navMeshAgent.SetDestination(currentTargetPoint);
        distanceFromPoint = Vector3.Distance(transform.position, currentTargetPoint);

        if(distanceFromPoint <= Mathf.Epsilon)
        {
            if(currentPoint == patrolPoints.Length)
            {
                loopingForward = false;
            }
            else if(currentPoint == 0)
            {
                loopingForward = true;
            }

            if (loopingForward)
            {
                currentPoint++;
                currentTargetPoint = patrolPoints[currentPoint];
            }
            else
            {
                currentPoint--;
                currentTargetPoint = patrolPoints[currentPoint];
            }
        }
    }

    private void Chase()
    {
        meshRenderer.material.color = Color.magenta;
        navMeshAgent.SetDestination(player.transform.position);
    }

    private void Search()
    {
        meshRenderer.material.color = Color.yellow;
        navMeshAgent.SetDestination(lastSeenTargetPos);
    }

    private void Attack()
    {
        meshRenderer.material.color = Color.red;
        // attack logic here
    }

    private void Retreat()
    {
        meshRenderer.material.color = Color.blue;
        navMeshAgent.SetDestination(currentTargetPoint);
    }

    // ------------------------------------------------------------------------------------- methods returning bools

    private bool TargetInChaseRange()
    {
        //distanceFromTarget = Vector3.Distance(transform.position, player.transform.position);
        if (distanceFromTarget <= chaseRange) // may need to modify to alleviate state flicker -> there may be a better place to alleviate all state flickering
        {
            return true;
        }
        else { return false; }
    }

    private bool TargetInAttackRange()
    {
        if(distanceFromTarget <= attackRange)
        {
            return true;
        }
        else { return false; }
    }

    private bool ReturnedToPatrolPoint()
    {
        distanceFromPoint = Vector3.Distance(transform.position, currentTargetPoint);
        if(distanceFromPoint <= Mathf.Epsilon)
        {
            return true;
        }
        else { return false; }
    }


    // ----------------------------------------------------------------------------------------- enums

    public enum State
    {
        patrol,
        chase,
        search,
        attack,
        retreat, // returning to nearest patrol point -> changes to patrol state
    }
}
