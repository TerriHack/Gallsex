using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    private bool horizontal;

    // Start is called before the first frame update
    void Start()
    {
        
    }


    private void LateUpdate()
    {
        if (Vector3.Distance(transform.position, waypoints[currentWaypointIndex]) > toNextWaypoint)
        {
            speed *= speedFactor;
            if (speed > maxSpeed)
            {
                speed = maxSpeed;
            }
        }
        else if (currentWaypointIndex+1 < waypoints.Count)
        {
            if (Vector3.Distance(transform.position, waypoints[currentWaypointIndex]) < toNextWaypoint &&
                hasTweened == false)
            {
                //rotate via tween
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
        horizontal = isHorizontal;
        respawnPosition = startPos;
        waypoints = waypointList;
    }
    
}
