using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // this script does 2 main things:
    // 1. move the mummy forward
    // 2. rotates the player towards the directon (given from the input manager)
    CharacterController controller;

    //not really being used for the prototype
    Animator MummyAnimator;


    //rotation, movement and gravity is created for the mummy
    public float rotationSpeed =8;
    public float movementSpeed =7;
    public float gravity = 10;

    Vector3 movementVector = Vector3.zero;

    private float desiredAngleRoation = 0;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        
        //animation is just playing without any pause. this willl be fixed for the final game but is just being played.
        MummyAnimator = GetComponent<Animator>();

    }

    public void MovementHandle(Vector2 input) {
        //listening to input so the mummy can move forward 
        if (controller.isGrounded) {
            if (input.y > 0)
            {
                movementVector = transform.forward * movementSpeed;
            }
            else {
                movementVector = Vector3.zero;

                //MummyAnimator.SetFloat("move", 0);
                //this is commented out as animaton isnt being used for the prototype (properly)

            }

        }
    }

    public void HandleDirectionMovement(Vector3 direction) {
        //calculating the angle between forward direction and the direction we want to face; to know where to rotate.
        desiredAngleRoation = Vector3.Angle(transform.forward, direction);
        
        //cross product to determine whether we want to rotate to left or right.
        var crossProduct = Vector3.Cross(transform.forward, direction).y;
        if (crossProduct < 0)
        {
            desiredAngleRoation *= -1;
        }

    }

    private void RotatePlayer() {
        //player rotation method to check the desired rotation angle if greater or less than 10 (threshold)
        if (desiredAngleRoation > 10 || desiredAngleRoation < -10) {
            transform.Rotate(Vector3.up * desiredAngleRoation * rotationSpeed * Time.deltaTime);
        }
    }


    public void Update()
    {
        //calling the rotate method to rotate the player while moving
        RotatePlayer();

        //applying the movement with gravite for allow the mummy to move if grounde
        movementVector.y -= gravity;
        controller.Move(movementVector * Time.deltaTime);

    }


}
