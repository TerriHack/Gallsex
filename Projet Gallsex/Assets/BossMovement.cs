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

    public List<Vector3> waypoints;
    private int currentWaypointIndex;

    private float yPositionDifference;

    public bool hasBossStarted = false;
    public float tweenSpeed;
    public GameObject boss;

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

    void Update()
    {
        if (hasBossStarted)
        {
            ChangeSpeed();
            NormalMovement();
            transform.position = Vector3.MoveTowards(transform.position, waypoints[currentWaypointIndex], Time.deltaTime * speed);
        }
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
}
