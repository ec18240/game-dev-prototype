using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoverController : MonoBehaviour
{

    public Vector3 starting_point;
    public Vector3 end_point;

    private Vector3 target_point;

    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        target_point = end_point;
        speed = 5f;
    }

    // Update is called once per frame
    void Update()
    {
        float delta = speed * Time.deltaTime;
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

    void ChangePoint(Vector3 vector_point)
    {
        target_point = vector_point;
    }
}
