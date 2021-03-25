using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour
{


    // patrol locations
    [Header("Patrol Point Positions")]
    [SerializeField] Vector3[] points;
    /*[SerializeField] Vector3 point1;
    [SerializeField] Vector3 point2;
    [SerializeField] Vector3 point3;
    [SerializeField] Vector3 point4;
    [SerializeField] Vector3 point5;*/

    private NavMeshAgent navMeshAgent;
    private State state;


    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = this.GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case State.patrol:
                Patrol();
                break;

            case State.chase:
                
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
    }

    private void Chase()
    {

    }

    private void Search()
    {

    }

    private void Attack()
    {

    }

    private void Retreat()
    {

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
