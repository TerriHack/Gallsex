using Cinemachine;
using DG.Tweening;
using UnityEngine;

public class camerafollow : MonoBehaviour
{
   public Transform target;

   public float smoothSpeed = 0.125f;
   public Vector3 offset;
   public float movementType = 0; // 0(cineMachine), 1(Boss), 2(Tween After Boss)
   public GameObject cinemachine;
   public float changeSpeed;
   public float minSpeed;
   public float maxSpeed;
   private Vector3 respawnPosition;
   
   [SerializeField] private GameObject[] waypoints;
   private int currentWaypointIndex = 0;
   private Vector3 StartPosition;
   private float speed;

   private void FixedUpdate()
   {
      if (movementType == 0)
      {
         
      }
      else if(movementType == 1)
      {
         if (Vector2.Distance(transform.position, waypoints[currentWaypointIndex].transform.position ) < 10f)
         {
            speed /= changeSpeed;
            if (speed <= minSpeed)
            {
               speed = minSpeed;
            }
         }
         else
         {
            speed *= changeSpeed;
            if (speed > maxSpeed)
            {
               speed = maxSpeed;
            }
            Debug.Log("multiplication");
         }
         
         if (Vector2.Distance(waypoints[currentWaypointIndex].transform.position, transform.position) < .1f)
         {
            currentWaypointIndex++;
            if (currentWaypointIndex >= waypoints.Length)
            {
               movementType = 2;
               currentWaypointIndex = 0;
               //cinemachine.GetComponent<CinemachineVirtualCamera>().enabled = true;
            }
         }
         transform.position = Vector3.MoveTowards(new Vector3(transform.position.x, transform.position.y,offset.z), new Vector3(waypoints[currentWaypointIndex].transform.position.x,waypoints[currentWaypointIndex].transform.position.y,offset.z),
            Time.deltaTime * speed);
      }
      else if (movementType == 2)
      {
         DOTween.To( () => transform.position, x => transform.position = x,
            new Vector3(target.transform.position.x, target.transform.position.y + 2,-10), 2);
         if (Vector2.Distance(transform.position,new Vector2(target.transform.position.x, target.transform.position.y + 2)) < 1)
         {
            movementType = 0;
            cinemachine.GetComponent<CinemachineVirtualCamera>().enabled = true;
         }
      }
   }

   public void BossFight(Vector3 StartPos, Vector3 waypoint1, Vector3 waypoint2, float transitionTime)
   {
      respawnPosition = StartPos;
      waypoints[0].transform.position = new Vector3(waypoint1.x,waypoint1.y,-10);
      waypoints[1].transform.position = new Vector3(waypoint2.x, waypoint2.y, -10);
      movementType = 1;
      speed = transitionTime;
      cinemachine.GetComponent<CinemachineVirtualCamera>().enabled = false;
   }

}
