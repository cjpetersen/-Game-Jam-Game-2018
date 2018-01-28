using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FollowScript : MonoBehaviour
{

    NavMeshAgent agent;
    Vision vision;
    public Transform target;
    public Transform[] targetArr;
    private bool playerFound = false;
    public float LostPlayerTimer = 5f;
    public float PatrolTimer = 5f;
    public int PatrolIndex = 0;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        vision = GetComponent<Vision>();

        if (vision)
        {
            vision.ObjectSeen += OnObjectSeen;
        }
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
        
        if (!playerFound)
        {
            PatrolTimer -= Time.deltaTime;
        }
        else if (target != null)
        {
            agent.SetDestination(target.position);
        }
        

        if (PatrolTimer <= 5 && playerFound == false && PatrolIndex < targetArr.Length && targetArr[PatrolIndex] != null)
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

    private void OnObjectSeen(Collider col)
    {
        Debug.Log("In view");
        if (col.gameObject.CompareTag("Player"))
        {
            var playerHideProbability = LightState.Instance.GetHideProbability(col.gameObject);
            // seen instantly when less than .5 hidden
            // 1/120 chance of being seen when less than 1 hidden (approx once per 2 seconds)
            if (playerHideProbability < .5 || (playerHideProbability < 1 && UnityEngine.Random.Range(1, 120) == 1))
            {
                PlayerFound(col.gameObject);
            }
        }
    }

    private void PlayerFound(GameObject player)
    {
        playerFound = true;
        target = player.transform;
        LostPlayerTimer = 5f;
    }

}

