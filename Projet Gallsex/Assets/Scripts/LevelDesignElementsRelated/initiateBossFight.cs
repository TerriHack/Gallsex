using Cinemachine;
using UnityEngine;

public class Trigger_Boss_Fight : MonoBehaviour
{
    public GameObject cam;
    public CinemachineVirtualCamera virtualCamera;
    public GameObject cloud;
    public void Horizontal(Vector3 startPos,Vector3 waypoint1, Vector3 waypoint2, float transitionTime, bool horizontal,float cloudRotation)
    {
        //cam.GetComponent<camerafollow>().BossFight(startPos, waypoint1,waypoint2,transitionTime,horizontal);
        cloud.transform.SetParent(cam.transform);
        if (cloud.transform.rotation != Quaternion.Euler(0, 0, cloudRotation))
        {
            cloud.transform.localRotation = Quaternion.Euler(0, 0, cloudRotation);
        }
    }
}
