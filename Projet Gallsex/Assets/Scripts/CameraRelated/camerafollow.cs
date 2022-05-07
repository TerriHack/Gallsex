using Cinemachine;
using DG.Tweening;
using UnityEngine;

public class Camerafollow : MonoBehaviour
{
   public Transform target;

   public Vector3 offset;
   public float movementType = 0; // 0(cineMachine), 1(Boss), 2(Tween After Boss)
   public GameObject cinemachine;
   public float changeSpeed;
   public float minSpeed;
   public float maxSpeed;
   public GameObject cloud;
   
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
            }
            else
            {
               //cloud.transform.Rotate(0,0,-90);
               DOTween.To( () => cloud.transform.rotation, x => cloud.transform.rotation= x,
                  new Vector3(cloud.transform.rotation.x,cloud.transform.rotation.y, 180 ), 2);
               DOTween.To( () => cloud.transform.position, x => cloud.transform.position= x,
                  new Vector3(cloud.transform.position.x  - offset.x,cloud.transform.position.y - offset.y, cloud.transform.position.z), 2);
            }
         }
         transform.position = Vector3.MoveTowards(new Vector3(transform.position.x, transform.position.y,offset.z), new Vector3(waypoints[currentWaypointIndex].transform.position.x,waypoints[currentWaypointIndex].transform.position.y,offset.z),
            Time.deltaTime * speed);
      }
      else if (movementType == 2)
      {
         cloud.transform.SetParent(null);
         DOTween.To( () => transform.position, x => transform.position = x,
            new Vector3(target.transform.position.x, target.transform.position.y + 1,-10), 2);
         if (Vector2.Distance(transform.position,new Vector2(target.transform.position.x, target.transform.position.y + 3)) < 1)
         {
            movementType = 0;
            cinemachine.GetComponent<CinemachineVirtualCamera>().enabled = true;
            //cloud.transform.SetParent();
         }
      }
   }

   public void BossFight(Vector3 waypoint1, Vector3 waypoint2, float transitionTime)
   {
      waypoints[0].transform.position = new Vector3(waypoint1.x,waypoint1.y,-10);
      waypoints[1].transform.position = new Vector3(waypoint2.x, waypoint2.y, -10);
      movementType = 1;
      speed = transitionTime;
      //cinemachine.GetComponent<CinemachineVirtualCamera>().enabled = false;
   }

}
