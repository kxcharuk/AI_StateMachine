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
                    state = State.retreating;
                }
                break;

            case State.retreating:

                break;

            case State.attack:

                break;

            case State.search:

                break;
        }
    }

    // -------------------------------------------------------------------------------------- private methods

    private void Patrol()
    {
        // patrol logic
        // ** change color here **
        meshRenderer.material.color = Color.white;
        navMeshAgent.SetDestination(currentTargetPoint);
        distanceFromPoint = Vector3.Distance(transform.position, currentTargetPoint);

        if(distanceFromPoint <= Mathf.Epsilon)
        {
            currentPoint++;
            currentTargetPoint = patrolPoints[currentPoint];
        }
    }

    private void Chase()
    {
        meshRenderer.material.color = Color.magenta;
    }

    private void Search()
    {
        meshRenderer.material.color = Color.yellow;
    }

    private void Attack()
    {
        meshRenderer.material.color = Color.red;
    }

    private void Retreat()
    {
        meshRenderer.material.color = Color.blue;
    }

    // ------------------------------------------------------------------------------------- methods returning bools

    private bool TargetInChaseRange()
    {
        distanceFromTarget = Vector3.Distance(transform.position, player.transform.position);
        if(distanceFromTarget <= chaseRange) // may need to modify to alleviate state flicker -> there may be a better place to alleviate all state flickering
        {
            return true;
        }
        else { return false; }
    }

    public enum State
    {
        patrol,
        chase,
        search,
        attack,
        retreating, // returning to nearest patrol point -> changes to patrol state
    }
}
