using UnityEngine;

public class TRAP : MonoBehaviour
{
    public GameObject player;
    public GameObject camera;

    [SerializeField] private Vector3 bossRespawn;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            int I = player.GetComponent<ArrayCheckpoint>().checkpointArray.Count;
            Vector2 pos = player.GetComponent<ArrayCheckpoint>().checkpointArray[I - 1];

            if (camera.GetComponent<camerafollow>().movementType == 1)
            {
                camera.GetComponent<camerafollow>().OnDeath();
                player.transform.position = new Vector3(camera.transform.position.x, camera.transform.position.y,
                    player.transform.position.z); //camera.transform.position;
            }
            else
            {
                player.transform.position = pos;
            }
            
            
        }
    }
}
