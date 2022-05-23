using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class MoveMouth : MonoBehaviour
{
    public bool horizontal;
    public GameObject player;
    public float distance;
    public float speed;

    // Update is called once per frame
    void Update()
    {
        if (horizontal)
        {
            if (player.transform.position.y > transform.position.y && Vector2.Distance(new Vector2(0,transform.position.y), new Vector2(0,player.transform.position.y)) > distance)
            {
                transform.DOMoveY(transform.position.y + 1, speed);
            }
            else if (player.transform.position.y < transform.position.y && Vector2.Distance(new Vector2(0,transform.position.y), new Vector2(0,player.transform.position.y)) > distance)
            {
                transform.DOMoveY(transform.position.y - 1, speed);
            }
            else
            {
                
            }
        }
        else
        {
            if (player.transform.position.x > transform.position.x && Vector2.Distance(new Vector2(transform.position.x, 0), new Vector2(player.transform.position.x,0)) > distance)
            {
                transform.DOMoveX(transform.position.x + 1, speed);
            }
            else if (player.transform.position.x < transform.position.x && Vector2.Distance(new Vector2(transform.position.x, 0), new Vector2(player.transform.position.x,0)) > distance)
            {
                transform.DOMoveX(transform.position.x - 1, speed);
            }
            else
            {
                
            }
        }
    }
}
