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
   public float movementType = 0; // 0(cineMachine), 1(Boss), 2(Tween After Boss)
   
   private float speed;
   public float speedDivision;
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
      cloud = GameObject.FindGameObjectWithTag("Cloud");
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

   private void FixedUpdate()
   {
      
      if (Vector2.Distance( waypoints[currentWaypointIndex], transform.position) < .1f)
      {
         currentWaypointIndex++;
         if (currentWaypointIndex > waypoints.Count)
         {
            // make it stop
         }
      }
      else if( Vector2.Distance( waypoints[currentWaypointIndex],transform.position) > toNextWaypoint)
      {
         Debug.Log( Vector2.Distance(waypoints[currentWaypointIndex], transform.position));
         if (speed ! > maxSpeed) // go faster
         {
            speed *= speedDivision;
            if (speed > maxSpeed)
            {
               speed = maxSpeed;
            }
         }
      }
      else if( Vector2.Distance( waypoints[currentWaypointIndex],transform.position) < toNextWaypoint)
      {
         speed /= speedDivision; // go slower
         if (speed < minSpeed)
         {
            speed = minSpeed;
         }
      }
      
      transform.position = Vector2.MoveTowards(transform.position, waypoints[currentWaypointIndex], Time.deltaTime * speed);
   }

   public void BossFight(Vector3 startPos, List<Vector3> waypointList, float tweenSpeed, bool isHorizontal)
   {
      Start();
      cloud.transform.position = Vector3.zero;
      respawnPosition = startPos;
      waypoints = waypointList;
      speed = tweenSpeed;
      horizontal = isHorizontal;
      transform.parent.GetComponent<DotweenCam>().enabled = false;
      GetComponent<camerafollow>().enabled = true;
   }

   public void OnDeath()
   {
      if (movementType != 0)
      {
         transform.position = respawnPosition;
         speed = 0.1f;
         currentWaypointIndex = 0;
         cloud.transform.rotation = Quaternion.Euler(0,0,90);
         cloud.transform.localPosition = new Vector3(0, 0, 10);
      }
   }

}
