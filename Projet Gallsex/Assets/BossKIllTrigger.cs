using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Unity.Mathematics;

public class BossKIllTrigger : MonoBehaviour
{
    public float speed;
    public float speedFactor;
    public float maxSpeed;
    public float minSpeed;

    [SerializeField] List<Vector3> waypoints;
    private int currentWaypointIndex;
    public float toNextWaypoint;

    public float tweenTime;

    private Vector2 lastPosition;
    public GameObject player;

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
        GameObject playerWaypoint = GameObject.FindGameObjectWithTag("BossWaypoint");
        waypoints[0] = playerWaypoint.transform.position;
        
        if (Vector3.Distance(transform.position, player.transform.position) > toNextWaypoint)
        {
            speed *= speedFactor;
            if (speed > maxSpeed)
            {
                speed = maxSpeed;
            }

        }


        if (Vector2.Distance(player.transform.position, transform.position) < .5f)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -10);
            currentWaypointIndex++;
            if (currentWaypointIndex >= waypoints.Count)
            {
                GetComponent<BossKIllTrigger>().enabled = false;
                currentWaypointIndex = 0;
            }
        }
        
        transform.position = Vector3.MoveTowards(transform.position, waypoints[currentWaypointIndex], Time.deltaTime * speed);
        //transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, Vector2.Angle(playerWaypoint.transform.position,transform.position));
        /*transform.LookAt(mouthRotation);
        transform.rotation = new quaternion(0, 0, transform.rotation.z *-1, transform.rotation.w);*/
        //Debug.Log(Vector2.Angle(playerWaypoint.transform.position, transform.position));
        
        /*Vector2 angle = playerWaypoint.transform.position - transform.position;
        transform.eulerAngles = new Vector3( transform.rotation.x, transform.rotation.y, Vector2.Angle(playerWaypoint.transform.position-transform.position,Vector2.right));
        Debug.Log(angle);*/
        transform.LookAt(playerWaypoint.transform.position);
    }

    public void Activate(Vector3 startPos, List<Vector3> waypointList, bool isHorizontal)
    {
        GetComponent<BossKIllTrigger>().enabled = true;
        transform.position = startPos;
        waypoints = waypointList;
    }

}
