using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Light))]
[RequireComponent(typeof(SphereCollider))]
public class LightSource : MonoBehaviour
{

    public Light attachedLight;
    public float maxBrightnessProportion = 0.25f;
    public float minBrightnessProportion = 0.9f;

    private SphereCollider _sphereCollider;

    private void Awake()
    {
        _sphereCollider = GetComponent<SphereCollider>();
        _sphereCollider.radius = attachedLight.range;
        _sphereCollider.isTrigger = true;
    }

    void Start () {
		
	}

    void Update () {
		
	}

    void OnDrawGizmos()
    {
        if (!attachedLight)
            return;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, attachedLight.range);
    }

    private void OnTriggerStay(Collider collider)
    {
        // if inside sphere, make a ray cast on light layer to find if object is lit
        RaycastHit raycastHit;
        if (Physics.Raycast(transform.position, collider.transform.position - transform.position, out raycastHit, attachedLight.range, LightState.Instance.visibleMask))
        { // hit an object that can be seen
            var objectHideProbability = CalculateHideProbability(raycastHit.distance);
            if (objectHideProbability < 1f)
            {
                LightState.Instance.ReportObjectLit(this, collider.gameObject, objectHideProbability);
            }
        }
    }

    private float CalculateHideProbability(float distance)
    {
        // get proportions of light range for minimum and maximum hide probabilities
        var minHideDistance = attachedLight.range * maxBrightnessProportion;
        var maxHideDistance = attachedLight.range * minBrightnessProportion;
        // shift distances down for proportion calculation
        var shiftedMaxDistance = maxHideDistance - minHideDistance;
        var shiftedDistance = distance - minHideDistance;

        return Mathf.Clamp(shiftedDistance / shiftedMaxDistance, 0, 1);
    }
}
