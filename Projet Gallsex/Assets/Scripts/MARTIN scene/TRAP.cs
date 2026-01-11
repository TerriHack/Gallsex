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
            
        }
    }

    private void Start()
    {
        movingPlatformManager = GameObject.FindGameObjectWithTag("MovingPlatformManager");
    }
}
