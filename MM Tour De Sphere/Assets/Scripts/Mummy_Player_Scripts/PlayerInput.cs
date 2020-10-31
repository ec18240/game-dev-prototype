using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerInput : MonoBehaviour, IInput
{
    //attributes for movement input
    public Action<Vector2> OnMovementInput { get; set; }

    //player direction target
    public Action<Vector3> OnMovementDirectionInput { get; set; }

    private void Start()
    {
        //locking the cursors in the centre of the screen
        Cursor.lockState = CursorLockMode.Locked;

    }
    private void Update()
    { //calling get methods for movement input and direction
        GetMovementInput();
        GetMovementDirection();

    }

    private void GetMovementDirection()
    {
        //method for player movement direction
        var cameraForwardDirection = Camera.main.transform.forward;


        var directionToMoveIn = Vector3.Scale(cameraForwardDirection, (Vector3.right + Vector3.forward));
       
        // check if isnt null, then invoke the movement direction
        OnMovementDirectionInput?.Invoke(directionToMoveIn);

    }

    private void GetMovementInput()
    {
        //method for player input
        Vector2 input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
       OnMovementInput?.Invoke(input);
    }
}

