using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vision : MonoBehaviour {
    public delegate void ObjectSeenHandler(Collider col);
    public ObjectSeenHandler ObjectSeen;

    public float visionRadius = 10;
    public float visionAngle = 120;
    public float checkFrequency = .25f;

    private float checkTimer = 0;

    private List<GameObject> colliderList = new List<GameObject>();
	
	void Update () {
        colliderList.Clear();
        if (checkTimer <= 0)
        {
            checkTimer = checkFrequency;
            var overlappingColliders = Physics.OverlapSphere(transform.position, visionRadius, LightState.Instance.visibleMask);
            foreach (var col in overlappingColliders)
            {
                var directionVector = col.transform.position - transform.position;
                var viewAngle = Vector3.Angle(transform.rotation * Vector3.forward, directionVector);
                if (viewAngle <= visionAngle / 2 && viewAngle >= -visionAngle / 2)
                {
                    // now cast a ray at the object and see if it's blocked
                    colliderList.Add(col.gameObject);
                    RaycastHit raycastHit;
                    if (Physics.Raycast(transform.position, directionVector, out raycastHit, Vector3.Distance(transform.position, col.transform.position), LightState.Instance.visionMask) && raycastHit.collider.gameObject == col.gameObject)
                    {
                        if (ObjectSeen != null)
                            ObjectSeen(col);
                    }
                }
            }
        }
        else
        {
            checkTimer -= Time.deltaTime;
        }
	}

    void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, visionRadius);
        Gizmos.color = Color.red;
        var visionConeCenter = CalculateVisionLimit(Vector3.zero);
        var visionConeLeft = CalculateVisionLimit(new Vector3(0, -visionAngle / 2, 0));
        var visionConeRight = CalculateVisionLimit(new Vector3(0, visionAngle / 2, 0));
        var visionConeTop = CalculateVisionLimit(new Vector3(visionAngle / 2, 0, 0));
        var visionConeBottom = CalculateVisionLimit(new Vector3(-visionAngle / 2, 0, 0));

        Gizmos.DrawLine(transform.position, visionConeCenter);
        Gizmos.DrawLine(transform.position, visionConeLeft);
        Gizmos.DrawLine(transform.position, visionConeRight);
        Gizmos.DrawLine(transform.position, visionConeTop);
        Gizmos.DrawLine(transform.position, visionConeBottom);

        Gizmos.color = Color.yellow;
        foreach( var collidedObject in colliderList)
        {
            var directionVector = collidedObject.transform.position - transform.position;
            Gizmos.DrawRay(transform.position, directionVector);
        }
    }

    private Vector3 CalculateVisionLimit(Vector3 angle)
    {
        return transform.position + transform.rotation * Quaternion.Euler(angle) * Vector3.forward * visionRadius;
    }
}
