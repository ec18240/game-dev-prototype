using System.Collections;
using System.Collections.Generic;
using Unity.Profiling;
using UnityEngine;

public class ThirdPersonMummyMovement : MonoBehaviour
{

    public CharacterController mummyController;

    public float speed = 6f;

    public float smoothMummy = 0.1f;
    private float smoothTurnMummyVelocity;
    public Transform camera;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

    }

    // Update is called once per frame
    void Update()
    {
        float horizonal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 mummyDirection = new Vector3(horizonal, 0f, vertical).normalized;
        if (mummyDirection.magnitude >= 0.1f)
        {
            float AngleTargeted = Mathf.Atan2(mummyDirection.x, mummyDirection.z) * Mathf.Rad2Deg + camera.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, AngleTargeted, ref smoothTurnMummyVelocity, smoothMummy);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 DirectionToMove = Quaternion.Euler(0f, AngleTargeted, 0f) * Vector3.forward;

            mummyController.Move(DirectionToMove.normalized * speed * Time.deltaTime);
        }


    }
}