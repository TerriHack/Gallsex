using UnityEngine;

public class initiateBossFight : MonoBehaviour
{
    public GameObject camera;
    public GameObject cloud;
    public void Horizontal(Vector3 startPos,Vector3 waypoint1, Vector3 waypoint2, float transitionTime)
    {
        camera.GetComponent<camerafollow>().BossFight(startPos, waypoint1,waypoint2,transitionTime);
        cloud.transform.SetParent(camera.transform);
        cloud.transform.position = transform.localPosition = new Vector3(0,0,0);
        if (cloud.transform.rotation != Quaternion.Euler(0, 0, 90))
        {
            cloud.transform.Rotate(new Vector3(0,0,90));
        }
        
    }
}
