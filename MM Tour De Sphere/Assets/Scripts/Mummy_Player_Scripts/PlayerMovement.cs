using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    CharacterController controller;

    Animator MummyAnimator;

    public float rotationSpeed = 8;
    public float movementSpeed = 7;
    public float gravity = 10;
    public float jumpspeed = 4f;

    Vector3 movementVector = Vector3.zero;

    private float desiredAngleRoation = 0;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        MummyAnimator = GetComponent<Animator>();

    }

    public void MovementHandle(Vector2 input)
    {
        if (controller.isGrounded)
        {
            if (input.y > 0)
            {
                movementVector = transform.forward * movementSpeed;
            }
            else
            {
                movementVector = Vector3.zero;
                //MummyAnimator.SetFloat("move", 0);
            }
        }
    }

    public void HandleDirectionMovement(Vector3 direction)
    {
        desiredAngleRoation = Vector3.Angle(transform.forward, direction);
        var crossProduct = Vector3.Cross(transform.forward, direction).y;
        if (crossProduct < 0)
        {
            desiredAngleRoation *= -1;
        }

    }

    private void RotatePlayer()
    {
        if (desiredAngleRoation > 10 || desiredAngleRoation < -10)
        {
            transform.Rotate(Vector3.up * desiredAngleRoation * rotationSpeed * Time.deltaTime);
        }
    }


    public void Update()
    {
        //RotatePlayer();
        movementVector.y -= gravity;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            movementVector.y = jumpspeed;

        }
        controller.Move(movementVector * Time.deltaTime);

    }


}
