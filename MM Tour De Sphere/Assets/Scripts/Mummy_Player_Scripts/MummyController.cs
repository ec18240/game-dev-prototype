using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MummyController : MonoBehaviour
{
    IInput input;
    PlayerMovement movement;
    // Start is called before the first frame update
    private void OnEnable()
    {
        input = GetComponent<IInput>();
        movement = GetComponent<PlayerMovement>();
        input.OnMovementDirectionInput += movement.HandleDirectionMovement;
        input.OnMovementInput += movement.MovementHandle;
          
    }

    private void OnDisable()
    {
        input.OnMovementDirectionInput -= movement.HandleDirectionMovement;
        input.OnMovementInput -= movement.MovementHandle;

    }

}
