using UnityEngine;

public class ChangeCamera : MonoBehaviour
{
    public GameObject bossCam;
    public GameObject playerCam;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerCam.transform.position = bossCam.transform.position;
            bossCam.SetActive(false);
            playerCam.SetActive(true);
            bossCam.GetComponent<CameraBoss>().offsetY = 0;
        }
    }
}
