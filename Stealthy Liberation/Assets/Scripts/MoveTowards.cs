using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveTowards : MonoBehaviour
{

    public NavMeshAgent navMeshAgent;
    //public GameObject target;
    private NavMeshPath navMeshPath;

    // Use this for initialization
    void Start()
    {

        navMeshAgent = GetComponent<NavMeshAgent>();

        //navMeshAgent.CalculatePath(target.transform.position, navMeshPath);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100))
            {
                navMeshAgent.SetDestination(hit.point);
            }
        }

    }
}
