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
         maxSpeed = 18;
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
      
   }

   public void Activate()
   {
      GetComponent<camerafollow>().enabled = true;
      GetComponent<DotweenCam>().enabled = false;
   }
}
