using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour {

	void Awake () {

    }
	
	void Update () {
		transform.Rotate(Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"), 0);
    }
}
