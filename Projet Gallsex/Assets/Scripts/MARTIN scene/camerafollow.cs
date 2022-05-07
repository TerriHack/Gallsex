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

   public float tweenTime;
   public bool hasTweened;


   private void Start()
   {
      target = GameObject.FindGameObjectWithTag("Player").transform;
      cloud = GameObject.FindGameObjectWithTag("BossKillTrigger");
      if (minSpeed == 0)
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

      if (tweenTime == 0)
      {
         tweenTime = 2;
      }
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
            TweenKillTrigger();
         } 
      }
      
      if (Vector2.Distance(waypoints[currentWaypointIndex], transform.position) < .5f)
      {
         transform.position = new Vector3(transform.position.x, transform.position.y, -10);
         currentWaypointIndex++;
         hasTweened = false;
         if (currentWaypointIndex >= waypoints.Count)
         {
            GetComponent<camerafollow>().enabled = false;
            GetComponent<DotweenCam>().enabled = true;
            currentWaypointIndex = 0;
            cloud.transform.parent = null;
         }
         else
         {
            respawnPosition = waypoints[currentWaypointIndex];
         }
      }
      transform.position = Vector3.MoveTowards(transform.position, waypoints[currentWaypointIndex], Time.deltaTime * speed);
   }

   public void BossFight(Vector3 startPos, List<Vector3> waypointList, float tweenSpeed, bool isHorizontal)
   {
      Start();
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
      cloud.transform.position = new Vector3(positionX / 2, startPos.y, 0);
      cloud.transform.localScale = new Vector3(1, transform.GetChild(0).GetComponent<Camera>().orthographicSize * 4, 1);
   }

   public void OnDeath()
   {
      speed = minSpeed;
      if (currentWaypointIndex == 0)
      {
         
      }
      else if (currentWaypointIndex == 1)
      {
         respawnPosition = waypoints[0];
         cloud.transform.rotation = new Quaternion(0,0,0,cloud.transform.rotation.w);
      }
      else if (currentWaypointIndex == 2)
      {
         respawnPosition = waypoints[1];
         //cloud.transform.rotation = new Quaternion();
      }

      transform.position = respawnPosition;
   }

   private void TweenKillTrigger()
   {
      Debug.Log("tweening");
      GameObject cam = transform.GetChild(0).transform.gameObject;
      if (horizontal)
      {
         if (waypoints[currentWaypointIndex].y > waypoints[currentWaypointIndex + 1].y)
         {
            // going down
         }
         else
         {
            //going up
            DOTween.To(() => cloud.transform.rotation, x => cloud.transform.rotation = x,
               new Vector3(cloud.transform.rotation.x, cloud.transform.rotation.y, 90), tweenTime);
         }
         horizontal = false;
      }
      else
      {
         Debug.Log("aaaaa");
         if (waypoints[currentWaypointIndex].x < waypoints[currentWaypointIndex + 1].x)
         {
            //going right
            DOTween.To(() => cloud.transform.rotation, x => cloud.transform.rotation = x, new Vector3(cloud.transform.rotation.x,cloud.transform.rotation.y,0), tweenTime);
            DOTween.To(() => cloud.transform.position, x => cloud.transform.position = x, new Vector3(cam.transform.position.x,cloud.transform.position.y, cloud.transform.position.y), tweenTime);
         }
         else
         {
            //going left
            DOTween.To(() => cloud.transform.rotation, x => cloud.transform.rotation = x, new Vector3(cloud.transform.rotation.x,cloud.transform.rotation.y,180), tweenTime);
            DOTween.To(() => cloud.transform.position, x => cloud.transform.position = x, new Vector3(cam.transform.position.x,cloud.transform.position.y, cloud.transform.position.y), tweenTime);
         }
         horizontal = true;
      }

      hasTweened = true;
   }
}
