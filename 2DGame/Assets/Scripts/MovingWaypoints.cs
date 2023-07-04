using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingWaypoints : MonoBehaviour
{
    [SerializeField] Transform Waypoint1, Waypoint2;
    public float speed = 2f;

    Vector2 targetPos;
    private void Start()
    {
        targetPos = Waypoint2.position;
    }
    private void Update()
    {
        if (Vector2.Distance(transform.position, Waypoint1.position) < .1f)
        {
            targetPos = Waypoint2.position;
        }
        if (Vector2.Distance(transform.position, Waypoint2.position) < .1f)
        {
            targetPos = Waypoint1.position;
        }

        transform.position = Vector2.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
    }
}
