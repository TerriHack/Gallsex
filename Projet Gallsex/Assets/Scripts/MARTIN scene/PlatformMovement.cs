using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovement : MonoBehaviour
{
    [SerializeField] private GameObject[] waypoints;
    private int currentWaypointIndex = 0;

    [SerializeField] public float speed = 2f;

    private void OnTriggerEnter2D(Collider2D collision2D)
    {
        if (collision2D.gameObject.CompareTag("Player"))
        {
            collision2D.gameObject.transform.SetParent(transform);
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(waypoints[currentWaypointIndex].transform.position, transform.position) < .1f)
        {
            currentWaypointIndex++;
            if (currentWaypointIndex >= waypoints.Length)
            {
                currentWaypointIndex = 0;
            }
        }

        transform.position = Vector2.MoveTowards(transform.position, waypoints[currentWaypointIndex].transform.position,
            Time.deltaTime * speed);
    }

    private void OnTriggerExit2D(Collider2D Collision2D)
    {
        if (Collision2D.gameObject.CompareTag("Player"))
        {
            Collision2D.gameObject.transform.SetParent(null);
        }
    }
}
