using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FollowScript : MonoBehaviour
{

    NavMeshAgent agent;
    public Transform target;
    private bool playerFound = false;
    public float LostPlayerTimer = 5f;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        
    }

    void Update()
    {
        if (playerFound == true)
        {
            LostPlayerTimer -= Time.deltaTime;
        }
        
        if (LostPlayerTimer <= 0)
        {
            playerFound = false;
        }

        if (Vector3.Distance(target.position, transform.position) < 5)
        {
            playerFound = true;

            agent.SetDestination(target.position);

                LostPlayerTimer = 5f;
              
        }



        



    }
}
