using System.Collections;
using System.Collections.Generic;
using System.Text;
using DG.Tweening;
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
                GetComponent<DotweenCam>().enabled = true;
                currentWaypointIndex = 0;
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
    }
    
    public void Activate(Vector3 startPos, List<Vector3> list)
    {
        Start();
        GetComponent<BossMovement>().enabled = true;
        GetComponent<DotweenCam>().enabled = false;
        transform.position = startPos;
        for (int i = 0; i < list.Count; i++)
        {
            waypoints.Add(list[i]);
        }
    }
}
