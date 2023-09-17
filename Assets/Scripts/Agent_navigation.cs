using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Agent_navigation : MonoBehaviour
{
    
    
    public GameObject player;

    public List<Vector3> waypoints;

    public int waypointIndex = 0;

    public int speed;

    public Graph_generation graphGeneration;

    public Transform target;


    public void Start()
    {
        waypoints = graphGeneration.wayPoints;
        graphGeneration.resetWaypoints();
    }

    public void Update()
    {
        waypoints = graphGeneration.wayPoints;
        move();
    }

    public void move()
    {
        if ((target.position - transform.position).magnitude > 1f && waypoints.Count == 0)
        {
            graphGeneration.resetWaypoints();
        }

        else if (waypoints.Count == 0)
        {
            moveFinalApproach();
        }

        else
        {
            followPath();
        }
    }


    public void followPath()
    {
        
        if (waypointIndex >= waypoints.Count)
        {
            return;
        }

        Vector3 movement = ((waypoints[waypointIndex] - transform.position).normalized / 100) * speed;
        transform.position = transform.position + movement;

        if ((waypoints[waypointIndex] - transform.position).magnitude < .3f)
        {
            waypointIndex++;
            graphGeneration.resetWaypoints();
        }
    }


    public void moveFinalApproach()
    {
          Vector3 movement = ((target.position - transform.position).normalized / 100) * speed;
          transform.position = transform.position + movement;
    }
    
     void OnTriggerEnter()
    {
       
            Destroy(player);
            Destroy(gameObject);
        
    }
}
