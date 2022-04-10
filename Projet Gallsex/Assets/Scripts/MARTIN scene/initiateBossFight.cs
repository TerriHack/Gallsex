using Cinemachine;
using UnityEngine;

public class initiateBossFight : MonoBehaviour
{
    public GameObject camera;
    public GameObject cloud;
    public float cloudOffset;
    public void Horizontal(Vector3 startPos,Vector3 waypoint1, Vector3 waypoint2, float transitionTime)
    {
        camera.GetComponent<camerafollow>().BossFight(startPos, waypoint1,waypoint2,transitionTime);
        cloud.transform.SetParent(camera.transform);
        //float orthographicSize = camera.GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize;
        cloud.transform.position = new Vector3(camera.transform.position.x - cloudOffset, camera.transform.position.y);
        if (cloud.transform.rotation != Quaternion.Euler(0, 0, 90))
        {
            cloud.transform.localRotation = Quaternion.Euler(0, 0, 90);
        }

        /*cloud.transform.position =
            new Vector3(camera.transform.position.x, orthographicSize * Screen.width / Screen.height);*/

    }
}
