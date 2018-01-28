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
	
	void Update () {

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
                    if (ObjectSeen != null)
                        ObjectSeen(col);
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
    }

    private Vector3 CalculateVisionLimit(Vector3 angle)
    {
        return transform.position + transform.rotation * Quaternion.Euler(angle) * Vector3.forward * visionRadius;
    }
}
