using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FollowScript : MonoBehaviour
{

    NavMeshAgent agent;
    public Transform target;
    public Transform[] targetArr;
    private bool playerFound = false;
    public float LostPlayerTimer = 5f;
    public float PatrolTimer = 5f;
    public int PatrolIndex = 0;

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
        
        else
        {
            PatrolTimer -= Time.deltaTime;
        }
        

        if (PatrolTimer <= 5 && playerFound == false)
        {
            agent.SetDestination(targetArr[PatrolIndex].position);
            if (PatrolTimer <= 0)
            {
                PatrolIndex++;
                PatrolTimer = 5;
               
                if (PatrolIndex == targetArr.Length)
                {
                    PatrolIndex = 0;
                }
            }
        }
   
    }

}

