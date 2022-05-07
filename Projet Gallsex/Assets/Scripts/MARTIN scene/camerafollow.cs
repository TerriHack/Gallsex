using System;
using System.Collections.Generic;
using Cinemachine;
using DG.Tweening;
using Unity.Mathematics;
using UnityEngine;

public class camerafollow : MonoBehaviour
{
   public bool horizontal;
   public Vector3 offset;
   
   private float speed;
   public float speedFactor;
   public float minSpeed;
   public float maxSpeed;
   public float toNextWaypoint;
   
   public GameObject cloud;
   public Vector3 respawnPosition;
   public Transform target;

   [SerializeField] private List<Vector3> waypoints;
   private int currentWaypointIndex = 0;


   private void Start()
   {
      target = GameObject.FindGameObjectWithTag("Player").transform;
      cloud = GameObject.FindGameObjectWithTag("BossKillTrigger");
      if (minSpeed > 0.1f || minSpeed == 0)
      {
         minSpeed = 0.1f;
      }

      if (maxSpeed == 0)
      {
         maxSpeed = 10;
      }

      if (toNextWaypoint == 0)
      {
         toNextWaypoint = 5;
      }
   }

   private void LateUpdate()
   {
      if (Vector3.Distance(transform.position, waypoints[currentWaypointIndex]) < toNextWaypoint)
      {
         speed /= speedFactor;
         //speed -= Time.deltaTime * 10;
         if (speed < minSpeed)
         {
            speed = minSpeed;
         }
      }
      else
      {
         speed *= speedFactor;
         if (speed > maxSpeed)
         {
            speed = maxSpeed;
         }
      }

      if (Vector2.Distance(waypoints[currentWaypointIndex], transform.position) < .5f)
      {
         transform.position = new Vector3(transform.position.x, transform.position.y, -10);
         currentWaypointIndex++;
         if (currentWaypointIndex >= waypoints.Count)
         {
            GetComponent<camerafollow>().enabled = false;
            GetComponent<DotweenCam>().enabled = true;
            currentWaypointIndex = 0;
            cloud.transform.parent = null;
         }
         else
         {
            TweenKillTrigger();
            respawnPosition = waypoints[currentWaypointIndex];
         }
      }
      transform.position = Vector3.MoveTowards(transform.position, waypoints[currentWaypointIndex], Time.deltaTime * speed);
   }

   public void BossFight(Vector3 startPos, List<Vector3> waypointList, float tweenSpeed, bool isHorizontal)
   {
      Start();
      cloud.transform.position = Vector3.zero;
      respawnPosition = startPos;
      waypoints = waypointList;
      speed = tweenSpeed;
      horizontal = isHorizontal;
      GetComponent<DotweenCam>().enabled = false;
      GetComponent<camerafollow>().enabled = true;
      for (int i = 0; i < waypoints.Count; i++)
      {
         waypoints[i] = new Vector3(waypoints[i].x, waypoints[i].y, -10);
      }

      float positionX = transform.GetChild(0).GetComponent<Camera>().orthographicSize;
      cloud.transform.parent = transform;
      cloud.transform.position = new Vector3(positionX, 0, 0);
      cloud.transform.localScale = new Vector3(1, transform.GetChild(0).GetComponent<Camera>().orthographicSize * 4, 1);
   }

   public void OnDeath()
   {
      transform.position = respawnPosition;
      speed = 0.1f;
      currentWaypointIndex = 0;
      cloud.transform.rotation = Quaternion.Euler(0,0,90);
      cloud.transform.localPosition = new Vector3(0, 0, 10);
      //cloud.transform.localScale.y = transform.GetChild(0).GetComponent<Camera>().orthographicSize;

   }

   private void TweenKillTrigger()
   {
      if (horizontal)
      {
         if (waypoints[currentWaypointIndex - 1].y > waypoints[currentWaypointIndex].y) // it do go down
         {
            Debug.Log("down");
         }
         else // no it don't
         {
            Debug.Log("Up");
         }
      }
      else
      {
         if (waypoints[currentWaypointIndex - 1].x > waypoints[currentWaypointIndex].x)
         {
            Debug.Log("right");
         }
         else
         {
            Debug.Log("left");
         }
      }
   }

}
