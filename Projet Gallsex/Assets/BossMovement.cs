using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMovement : MonoBehaviour
{
    public float maxSpeed;
    public float newMaxSpeed;
    public float changeMaxSpeed;
    public float minSpeed;
    public float newMinSpeed;
    public float speedFactor;
    [SerializeField] private float speed;

    public GameObject player;

    [SerializeField] private List<Vector3> waypoints;
    private int currentWaypointIndex;
    public float toNextWaypoint;

    private float yPositionDifference;
    public float speedUpDistance;
    public float slowDownDistance;

    public bool isHorizontal;

    // Start is called before the first frame update
    void Start()
    {
        if (maxSpeed == 0)
        {
            maxSpeed = 12;
            newMaxSpeed = maxSpeed;
        }

        if (minSpeed == 0)
        {
            minSpeed = 1;
            newMinSpeed = minSpeed;
        }

        if (speedFactor == 0)
        {
            speedFactor = 1.018f;
        }

        if (speed == 0)
        {
            speed = 0.5f;
        }

        if (changeMaxSpeed == 0)
        {
            changeMaxSpeed = 30;
        }
    }

    // Update is called once per frame
    void Update()
    {
        ChangeSpeed();
        NormalMovement();
        MoveMouth();
        transform.position = Vector3.MoveTowards(transform.position, waypoints[currentWaypointIndex], Time.deltaTime * speed);
        
    }


    private void NormalMovement()
    {
        
        if (Vector2.Distance(waypoints[currentWaypointIndex], transform.position) < .5f) // if at a waypoint
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -10);
            currentWaypointIndex++;
            if (currentWaypointIndex >= waypoints.Count)
            {
                GetComponent<BossMovement>().enabled = false;
                currentWaypointIndex = 0;
            }
        }

    }

    private void MoveMouth()
    {
        if (isHorizontal)
        {
            if (Vector2.Distance(new Vector2(0, transform.position.y), new Vector2(0, player.transform.position.y)) < yPositionDifference)
            {
                Vector3.MoveTowards(transform.position,
                    new Vector3(transform.position.x, transform.position.y + 1, transform.position.z),
                    Time.deltaTime * speed);
            }
            else if (Vector2.Distance(new Vector2(0, transform.position.y), new Vector2(0, player.transform.position.y)) > -yPositionDifference)
            {
            
                Vector3.MoveTowards(transform.position,
                    new Vector3(transform.position.x, transform.position.y - 1, transform.position.z),
                    Time.deltaTime * speed);
            }
        }
        else
        {
            if (Vector2.Distance(new Vector2(transform.position.x,0), new Vector2(player.transform.position.x, 0)) < yPositionDifference)
            {
                Vector3.MoveTowards(transform.position,
                    new Vector3(transform.position.x + 1, transform.position.y, transform.position.z),
                    Time.deltaTime * speed);
            }
            else if (Vector2.Distance(new Vector2(transform.position.x, 0), new Vector2(player.transform.position.x, 0)) > -yPositionDifference)
            {
            
                Vector3.MoveTowards(transform.position,
                    new Vector3(transform.position.x - 1, transform.position.y, transform.position.z),
                    Time.deltaTime * speed);
            }
        }
    }

    private void ChangeSpeed()
    {
        if (speed < newMaxSpeed)
        {
            speed *= speedFactor;
        }
        else if (speed < minSpeed)
        {
            speed = minSpeed;
        }
        else if (speed > newMaxSpeed)
        {
            speed /= speedFactor;
        }



        if (Vector2.Distance(transform.position, player.transform.position) > speedUpDistance)
        {
            newMaxSpeed = changeMaxSpeed;
            Debug.Log("speeding up");
        }
        else if (Vector2.Distance(transform.position, player.transform.position) < slowDownDistance)
        {
            newMaxSpeed = maxSpeed;
            Debug.Log("slowing");
        }
        else
        {
            newMaxSpeed = 20;
        }
    }
    
    public void Activate(Vector3 startPos, List<Vector3> list)
    {
        Start();
        GetComponent<BossMovement>().enabled = true;
        transform.position = startPos;
        for (int i = 0; i < list.Count; i++)
        {
            waypoints.Add(list[i]);
        }
    }
}
