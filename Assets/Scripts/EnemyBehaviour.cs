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

    [Header("AI Ranges")]
    [SerializeField] private float chaseRange;
    [SerializeField] private float attackRange;
    //[SerializeField] private float searchTime;
    private float distanceFromPoint;
    private Vector3 currentTargetPoint;
    private float distanceFromTarget;
    private Vector3 lastSeenTargetPos;

    [Header("Search State Timer Length")]
    [SerializeField] private float timer;
    private float timeStamp;
    private bool timerStarted;

    [Header("Player Object (Target to Chase)")]
    [SerializeField] GameObject player;

    [Header("Ease State Switch (State Switch Timer)")]
    [SerializeField] private float swTimer;
    private float swTimeStamp;
    private bool swTimerStarted;


    private State state;
    private NavMeshAgent navMeshAgent;
    private MeshRenderer meshRenderer;

    // Start is called before the first frame update
    void Start()
    {
        timerStarted = false;
        swTimerStarted = false;
        loopingForward = true;
        meshRenderer = GetComponent<MeshRenderer>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        currentPoint = 0;
        currentTargetPoint = patrolPoints[currentPoint];
    }

    // Update is called once per frame
    void Update()
    {
        distanceFromTarget = Vector3.Distance(player.transform.position, transform.position);

        switch (state)
        {
            case State.patrol:
                Debug.Log("In Patrol State");
                Patrol();
                if (TargetInChaseRange())
                {
                    if (StateSwitchTimerExpired())
                    {
                        state = State.chase;
                    }
                }
                break;

            case State.chase:
                Debug.Log("In Chase State");
                Chase();
                if (!TargetInChaseRange())
                {
                    lastSeenTargetPos = player.transform.position; // setting last seen position for the search state
                    if (StateSwitchTimerExpired())
                    {
                        state = State.search;
                    }
                }
                else if (TargetInAttackRange())
                {
                    ClearSWTimer();
                    state = State.attack;
                }
                break;

            case State.retreat:
                Debug.Log("In Retreat State");
                Retreat();
                if (TargetInChaseRange())
                {
                    if (StateSwitchTimerExpired())
                    {
                        state = State.chase;
                    }
                }
                else if (ReturnedToPatrolPoint())
                {
                    if (StateSwitchTimerExpired())
                    {
                        state = State.patrol;
                    }
                }
                break;

            case State.attack:
                Debug.Log("In Attack State");
                Attack();
                if (!TargetInAttackRange())
                {
                    if (StateSwitchTimerExpired())
                    {
                        state = State.chase;
                    }
                }
                break;

            case State.search:
                Debug.Log("In Search State");
                Search();
                if (TargetInChaseRange())
                {
                    if (StateSwitchTimerExpired())
                    {
                        state = State.chase;
                    }
                }
                else if (SearchTimerExpired())
                {
                    state = State.retreat;
                }
                break;
        }
    }

    // -------------------------------------------------------------------------------------- private methods

    private void ClearSWTimer()
    {
        swTimerStarted = false;
    }

    private void Patrol()
    {
        // patrol logic
        // ** change color here **
        meshRenderer.material.color = Color.green;
        navMeshAgent.SetDestination(currentTargetPoint);
        distanceFromPoint = Vector3.Distance(currentTargetPoint, transform.position);
            if(currentPoint == patrolPoints.Length)
            {
                loopingForward = false;
            }
            else if(currentPoint == 0)
            {
                loopingForward = true;
            }

        if(distanceFromPoint <= 0.5f)
        {
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

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            player.GetComponent<Player>().Death();
        }
    }

    // ------------------------------------------------------------------------------------- methods returning bools

    private bool StateSwitchTimerExpired()
    {
        if (!swTimerStarted)
        {
            swTimeStamp = Time.time;
            swTimerStarted = true;
            return false;
        }
        else
        {
            if ((Time.time - swTimeStamp) >= swTimer)
            {
                swTimerStarted = false;
                return true;
            }
            else { return false; }
        }
    }

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
        if(distanceFromPoint <= 0.5f/*Mathf.Epsilon*/)
        {
            return true;
        }
        else { return false; }
    }

    private bool SearchTimerExpired()
    {
        //Debug.Log("Timer Started");
        if (!timerStarted)
        {
            timeStamp = Time.time;
            timerStarted = true;
            return false;
        }
        else
        {
            if((Time.time - timeStamp) >= timer)
            {
                timerStarted = false;
                return true;
            }
            else { return false; }
        }
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
