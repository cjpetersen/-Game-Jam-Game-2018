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
    private HashSet<GameObject> _triggersThisFrame;
    //private List<GameObject> _litObjects = new List<GameObject>();
    private List<LitObject> _litObjects = new List<LitObject>();

    private void Awake()
    {
        _sphereCollider = GetComponent<SphereCollider>();
        _sphereCollider.radius = attachedLight.range;
        _sphereCollider.isTrigger = true;
        _triggersThisFrame = new HashSet<GameObject>();
    }

    void Start () {
		
	}

    void LateUpdate () {
        if (_triggersThisFrame.Count == 0)
        {
            foreach (var litObject in _litObjects)
            {
                LightState.Instance.ReportObjectLit(this, litObject.ObjectLit, litObject.HideProbability);
            }
        }
        _triggersThisFrame.Clear();
    }

    void OnDrawGizmos()
    {
        if (!attachedLight)
            return;
 
        foreach (var litObject in _litObjects)
        {
            Gizmos.color = litObject.HideProbability < .5 ? Color.red : litObject.HideProbability < 1 ? Color.yellow : Color.blue;
            Gizmos.DrawRay(transform.position, litObject.ObjectLit.transform.position - transform.position);
        }
    }

    void OnDrawGizmosSelected()
    {
        if (!attachedLight)
            return;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, attachedLight.range);
    }

        private void OnTriggerStay(Collider collider)
    { // since OnTriggerStay gets called every FixedUpdate, throttle it to only report once per update
        if (!_triggersThisFrame.Contains(collider.gameObject))
        {
            if (_triggersThisFrame.Count == 0)
            {
                _litObjects.Clear();
            }
            _triggersThisFrame.Add(collider.gameObject);
            // if inside sphere, make a ray cast on light layer to find if object is lit
            RaycastHit raycastHit;
            if (Physics.Raycast(transform.position, collider.transform.position - transform.position, out raycastHit, attachedLight.range))
            { // hit an object
                //_litObjects.Add(collider.gameObject);
                //Debug.Log("Shining on object " + collider.gameObject.name);
                if ((LightState.Instance.visibleMask & 1 << raycastHit.collider.gameObject.layer) != 0)
                { // hit an object that we actually care about
                    var objectHideProbability = CalculateHideProbability(raycastHit.distance);
                    if (objectHideProbability < 1f)
                    {
                        _litObjects.Add(new LitObject { ObjectLit = collider.gameObject, HideProbability = objectHideProbability });
                        LightState.Instance.ReportObjectLit(this, collider.gameObject, objectHideProbability);
                    }
                }
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

    private class LitObject
    {
        public GameObject ObjectLit { get; set; }
        public float HideProbability { get; set; }
    }
}
