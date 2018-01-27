using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    public enum MoveSpeed
    {
        Walk,
        Run
    }

    public MoveSpeed moveSpeed;
    public float runSpeed;
    public float walkSpeed;

    CharacterController characterController;

    public float CurrentMoveSpeed {
        get {
            return moveSpeed == MoveSpeed.Run ? runSpeed : walkSpeed;
        }
    }

    void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }
    
    void Start () {
		
	}
	
	void Update () {
        var zMove = 0f;
        var xMove = 0f;
        if (Input.GetKey(KeyCode.W))
        {
            zMove = 1;
        } else if (Input.GetKey(KeyCode.S))
        {
            zMove = -1;
        }

        if (Input.GetKey(KeyCode.D))
        {
            xMove = 1;
        } else if (Input.GetKey(KeyCode.A))
        {
            xMove = -1;
        }

        moveSpeed = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift) ? MoveSpeed.Run : MoveSpeed.Walk;
        characterController.SimpleMove(transform.rotation * new Vector3(xMove, 0, zMove) * CurrentMoveSpeed);
	}
}
