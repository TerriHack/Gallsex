using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weirdoy : MonoBehaviour
{
    public GameObject player;
    public GameObject eyes;
    private float startPosX, startPosY;
    void Start()
    {
        startPosX = eyes.transform.position.x;
        startPosY = eyes.transform.position.y;
    }

    void Update()
    {
        eyes.transform.position = new Vector3(startPosX, player.transform.position.y, 0);
        if (eyes.transform.position.y > startPosY + 0.08f)
        {
            eyes.transform.position = new Vector3(startPosX, startPosY + 0.08f, 0);
        }

        if (eyes.transform.position.y < startPosY - 0.08f)
        {
            eyes.transform.position = new Vector3(startPosX, startPosY - 0.08f, 0);
        }
    }
}