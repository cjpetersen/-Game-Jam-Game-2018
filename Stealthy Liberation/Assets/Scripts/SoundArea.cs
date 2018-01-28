using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundArea : MonoBehaviour {
    [SerializeField]
    private List<SoundArea> _attachedAreas = new List<SoundArea>();
    private bool _initialized = false;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void LateUpdate () {
        if (!_initialized)
            _initialized = true;
	}

    private void OnTriggerEnter(Collider other)
    {
        if (!_initialized)
        {
            var otherSoundArea = other.GetComponent<SoundArea>();
            if (otherSoundArea)
            {
                _attachedAreas.Add(otherSoundArea);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;

        foreach( var soundArea in _attachedAreas)
        {
            Gizmos.DrawLine(transform.position, soundArea.transform.position);
        }
    }
}
