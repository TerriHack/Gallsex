using System;
using Cinemachine;
using UnityEngine;

public class initiateBossFight : MonoBehaviour
{
    public GameObject cam;
    public GameObject cloud;
    public float cloudOffset;

    private void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera");
        cloud = GameObject.FindGameObjectWithTag("Cloud");
    }

    public void Horizontal(Vector3 startPos,Vector3 waypoint1, Vector3 waypoint2, float transitionTime)
    {
        cam.GetComponent<Camerafollow>().BossFight(waypoint1,waypoint2,transitionTime);
        cloud.transform.SetParent(cam.transform);
        cloud.transform.position = new Vector3(cam.transform.position.x - cloudOffset, cam.transform.position.y);
        if (cloud.transform.rotation != Quaternion.Euler(0, 0, 90))
        {
            cloud.transform.localRotation = Quaternion.Euler(0, 0, 90);
        }

    }
}
