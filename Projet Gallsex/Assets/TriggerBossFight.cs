using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTriggers : MonoBehaviour
{
   public Vector3 startPosition;
   public Vector3 waypoint1;
   public Vector3 waypoint2;
   public float speed;
   public bool startsHorizontally;
   public float cloudStartRotation;
   
   private void OnTriggerEnter2D(Collider2D other)
   {
      if (other.CompareTag("Player"))
      {
         //other.GetComponent<initiateBossFight>().Horizontal(startPosition, waypoint1, waypoint2, speed,startsHorizontally,cloudStartRotation);
         other.GetComponent<initiateBossFight>().Horizontal(startPosition, waypoint1,waypoint2,speed);
      }
   }
}
