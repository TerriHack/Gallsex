using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weirdo : MonoBehaviour
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
        eyes.transform.position = new Vector3(player.transform.position.x, startPosY, 0);
        if (eyes.transform.position.x > startPosX + 0.05f)
        {
            eyes.transform.position = new Vector3(startPosX + 0.05f, startPosY, 0);
        }

        if (eyes.transform.position.x < startPosX - 0.1f)
        {
            eyes.transform.position = new Vector3(startPosX - 0.1f, startPosY, 0);
        }
    }
}