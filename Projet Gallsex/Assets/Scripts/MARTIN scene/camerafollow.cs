using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camerafollow : MonoBehaviour
{
   public Transform target;

   public float smoothSpeed = 0.125f;
   public Vector3 offset;
   public bool tracking = true;

   private void FixedUpdate()
   {
      if (tracking)
      {
         Vector3 desiredPosition = target.position + offset;
         Vector3 smoothedPosition = Vector3.Lerp(transform.position,desiredPosition,smoothSpeed);
         transform.position = smoothedPosition;
      }
   }

   public void changeScale(Vector2 scale)
   {
      transform.localScale = scale;
   }

   public void changeOffset(Vector2 changedTo)
   {
      offset = changedTo;
   }
   
}
