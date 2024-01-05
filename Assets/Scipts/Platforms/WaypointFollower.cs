using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointFollower : MonoBehaviour
{
    [SerializeField] private GameObject[] waypoints;
    private int currentWaypointIndex = 0;
    [SerializeField] private float speed = 2f;
    [SerializeField] private bool followsX = false;
    [SerializeField] private bool followsY = false;
    private Vector2 variablePosition;

    private void Start() 
    {
        setsPosition();
    }
    private void Update()
    {
        if (Vector2.Distance(variablePosition, transform.position) < .1f)
        {
            currentWaypointIndex ++;
            if (currentWaypointIndex >= waypoints.Length)
            {
                currentWaypointIndex = 0;
            }
            setsPosition();
        }
        transform.position = Vector2.MoveTowards(transform.position, variablePosition, speed * Time.deltaTime);
    }

    private void setsPosition()
    {
        if (followsX && followsY)
        {
            variablePosition = waypoints[currentWaypointIndex].transform.position;
        }
        else if (followsX)
        {
            variablePosition = new Vector2(waypoints[currentWaypointIndex].transform.position.x,transform.position.y);

        }
        else if (followsY)
        {
            variablePosition = new Vector2(transform.position.x,waypoints[currentWaypointIndex].transform.position.y);
        }
        else
        {
            variablePosition = transform.position;
        }
    }
}
