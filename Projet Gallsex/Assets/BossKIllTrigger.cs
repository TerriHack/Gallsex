using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BossKIllTrigger : MonoBehaviour
{
    public float speed;
    public float speedFactor;
    public float maxSpeed;
    public float minSpeed;

    private Vector3 respawnPosition;
    [SerializeField] List<Vector3> waypoints;
    private int currentWaypointIndex;
    public float toNextWaypoint;

    [SerializeField] private bool hasTweened;
    public float tweenTime;
    private bool horizontal;

    // Start is called before the first frame update
    void Start()
    {
        if (maxSpeed == 0)
        {
            maxSpeed = 10;
        }

        if (minSpeed == 0)
        {
            minSpeed = 1;
        }

        if (speedFactor == 0)
        {
            speedFactor = 1.018f;
        }

        if (speed == 0)
        {
            speed = 0.5f;
        }

        if (toNextWaypoint == 0)
        {
            toNextWaypoint = 10;
        }

        if (tweenTime == 0)
        {
            tweenTime = 2;
        }
    }


    private void Update()
    {
        if (Vector3.Distance(transform.position, waypoints[currentWaypointIndex]) > toNextWaypoint)
        {
            speed *= speedFactor;
            if (speed > maxSpeed)
            {
                speed = maxSpeed;
            }

            if (hasTweened)
            {
                //hasTweened = false;
            }
        }
        else if (currentWaypointIndex + 1 < waypoints.Count)
        {
            if (Vector3.Distance(transform.position, waypoints[currentWaypointIndex]) < toNextWaypoint && hasTweened == false)
            {
                TweenRotate();
            }
        }


        if (Vector2.Distance(waypoints[currentWaypointIndex], transform.position) < .5f)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -10);
            currentWaypointIndex++;
            hasTweened = false;
            if (currentWaypointIndex >= waypoints.Count)
            {
                GetComponent<BossKIllTrigger>().enabled = false;
                currentWaypointIndex = 0;
            }
            else
            {
                respawnPosition = waypoints[currentWaypointIndex];
            }
        }

        transform.position = Vector3.MoveTowards(transform.position, waypoints[currentWaypointIndex], Time.deltaTime * speed);
    }

    public void Activate(Vector3 startPos, List<Vector3> waypointList, bool isHorizontal)
    {
        GetComponent<BossKIllTrigger>().enabled = true;
        horizontal = isHorizontal;
        respawnPosition = startPos;
        transform.position = startPos;
        waypoints = waypointList;
    }

    void TweenRotate()
    {
        if (horizontal)
        {
            if (waypoints[currentWaypointIndex].y > waypoints[currentWaypointIndex + 1].y)
            {
                // going down
            }
            else
            {
                //going up
                DOTween.To(() => transform.rotation, x => transform.rotation = x, new Vector3(transform.rotation.x, transform.rotation.y, 90), tweenTime);
            }

            horizontal = false;
        }
        else
        {
            if (waypoints[currentWaypointIndex].x < waypoints[currentWaypointIndex + 1].x)
            {
                //going right
                DOTween.To(() => transform.rotation, x => transform.rotation = x,
                    new Vector3(transform.rotation.x, transform.rotation.y, 0), tweenTime);
            }
            else
            {
                //going left
                DOTween.To(() => transform.rotation, x => transform.rotation = x,
                    new Vector3(transform.rotation.x, transform.rotation.y, 180), tweenTime);
            }
            Debug.Log("tweenHorizontal");

            horizontal = true;
        }

        hasTweened = true;
    }
}
