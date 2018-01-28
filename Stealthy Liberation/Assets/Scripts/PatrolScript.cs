using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolScript : MonoBehaviour
{
    NavMeshAgent agent;
    public float PatrolTimer = 5f;
    public float PatrolTimer2 = 7f;
    public Transform target;
    public Transform target2;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }


    // Update is called once per frame
    void Update()
    {

        //if (PatrolTimer == 5 || PatrolTimer2 == 5)
        if(PatrolTimer < 6)
        { PatrolTimer -= Time.deltaTime; }
        if (PatrolTimer2 < 6)
        { PatrolTimer2 -= Time.deltaTime; }

        
        
        if (PatrolTimer <= 5)
        {
            agent.SetDestination(target2.position);
            if (PatrolTimer <= 0)
            {
                PatrolTimer2 = 5;
                PatrolTimer = 6;
            }
        }
        if (PatrolTimer2 <= 5)
        {
            agent.SetDestination(target.position);
            if (PatrolTimer2 <= 0)
            {
                PatrolTimer = 5;
                PatrolTimer2 = 6;
            }
        }
    }
}
