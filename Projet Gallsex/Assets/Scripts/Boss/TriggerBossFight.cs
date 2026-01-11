using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerBossFight : MonoBehaviour
{
   public Vector3 startPosition;
   public Vector3 waypoint1;
   public Vector3 waypoint2;
   public float speed;
   
   private void OnTriggerEnter2D(Collider2D other)
   {
      if (other.CompareTag("Player"))
      { 
         other.GetComponent<initiateBossFight>().Horizontal(startPosition,waypoint1,waypoint2,speed);
      }
   }
}
