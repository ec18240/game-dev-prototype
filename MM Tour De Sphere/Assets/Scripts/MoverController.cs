using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoverController : MonoBehaviour
{

    public Vector3 starting_point; //Object's starting point
    public Vector3 end_point; //Object's end point

    private Vector3 target_point; //Target, this will switch between the start and end point

    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        target_point = end_point;
        speed = 5f;
    }

    // Update is called once per 
    /*
    Object moves to the target point. If the target point hits the starting_point, the target becomes the end_point
    If the target hits the end_point, the target becomes the start_point

    When target position == the position of the start_point or end_point, then the target has hit one of the points
     */
    void Update()
    {
        float delta = speed * Time.deltaTime; //Speed of the moving object in correspondence to the frame-rate
        transform.position = Vector3.MoveTowards(transform.position, target_point, delta);

        if(transform.position == end_point)
        {
            ChangePoint(starting_point);
        }
        else if(transform.position == starting_point)
        {
            ChangePoint(end_point);
        }
    }


    /*
     * Swaps the target position
     */
    void ChangePoint(Vector3 vector_point)
    {
        target_point = vector_point;
    }
}
