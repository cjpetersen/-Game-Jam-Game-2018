using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FollowScript : MonoBehaviour
{

    NavMeshAgent agent;
    public Transform target;
    public Transform target2;
    public Transform target3;
    private bool playerFound = false;
    public float LostPlayerTimer = 5f;
    public float PatrolTimer = 5f;
    public float PatrolTimer2 = 7f;
    public float PatrolTimer3 = 7f;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        
    }

    void Update()
    {
        if (playerFound == true)
        {
            LostPlayerTimer -= Time.deltaTime;
            PatrolTimer = 7;
            PatrolTimer2 = 7;
            PatrolTimer3 = 7;

        }
        
        if (LostPlayerTimer <= 0)
        {
            playerFound = false;
             PatrolTimer = 5;
             PatrolTimer2 = 7;
            PatrolTimer3 = 7;
        }

        if (Vector3.Distance(target.position, transform.position) < 5)
        {
            playerFound = true;

            agent.SetDestination(target.position);

                LostPlayerTimer = 5f;
              
        }
        else
        if (PatrolTimer < 6)
        { PatrolTimer -= Time.deltaTime; }
        if (PatrolTimer2 < 6)
        { PatrolTimer2 -= Time.deltaTime; }



        if (PatrolTimer <= 5)
        {
            agent.SetDestination(target3.position);
            if (PatrolTimer <= 0)
            {
                PatrolTimer2 = 5;
                PatrolTimer = 6;
            }
        }
        if (PatrolTimer2 <= 5)
        {
            agent.SetDestination(target2.position);
            if (PatrolTimer2 <= 0)
            {
                PatrolTimer = 5;
                PatrolTimer2 = 6;
            }
        }
    }

}

