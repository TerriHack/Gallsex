using System;
using UnityEngine;

public class TRAP : MonoBehaviour
{
    public GameObject player;
    public GameObject cam;
    private GameObject movingPlatformManager;

    [SerializeField] private Vector3 bossRespawn;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            int I = player.GetComponent<ArrayCheckpoint>().checkpointArray.Count;
            Vector2 pos = player.GetComponent<ArrayCheckpoint>().checkpointArray[I - 1];

            if (cam.GetComponent<camerafollow>().movementType == 1)
            {
                cam.GetComponent<camerafollow>().OnDeath();
                player.transform.position = new Vector3(cam.transform.position.x, cam.transform.position.y, player.transform.position.z); //camera.transform.position;
            }
            else
            {
                player.transform.position = pos;
            }
            
            
        }
    }

    private void Start()
    {
        movingPlatformManager = GameObject.FindGameObjectWithTag("MovingPlatformManager");
    }
}
