using System;
using UnityEngine;

public class PlatformMovement : MonoBehaviour
{
    private int doIWait;
    public bool active;
    [SerializeField] private GameObject[] wayPoints;
    private int currentWaypointIndex = 0;
    public float waitTime = 1;
    public float waitFor;
    public bool waiting = false;

    public AudioSource soundMovement;
    public AudioSource soundStop;
    public AudioClip stopClip;

    [SerializeField] public float speed = 2f;

    private void OnTriggerEnter2D(Collider2D collision2D)
    {
        if (collision2D.gameObject.CompareTag("Player"))
        {
            collision2D.gameObject.transform.SetParent(transform);
            if (active == false)
            {
                active = true;
                if (Vector2.Distance(wayPoints[currentWaypointIndex].transform.position, transform.position) < .1f)
                {
                    waiting = false;
                }
            }
        }
    }

    private void Start()
    {
        waiting = false;
    }


    void Update()
    {
        if (active == true)
        {
            if (Vector2.Distance(wayPoints[currentWaypointIndex].transform.position, transform.position) < .1f)
            { currentWaypointIndex++;
                doIWait ++;
                if (currentWaypointIndex >= wayPoints.Length)
                {
                    currentWaypointIndex = 0;
                }
                waitFor = waitTime;
                waiting = true;
                soundStop.PlayOneShot(stopClip, 0.3f);
                if (doIWait < 2)
                {
                    
                }
                else
                {
                    active = false;
                }
            }

            transform.position = Vector2.MoveTowards(transform.position,
                    wayPoints[currentWaypointIndex].transform.position, Time.deltaTime * speed);
            if (waiting)
            {
                waitFor -= Time.deltaTime;
                if (waitFor <= 0)
                { 
                    waiting = false;
                    active = true;
                }
            }
        }
        else
        {
            
        }

        
        //Ajout du son (Thomas)
        if (active) 
        {
            soundMovement.enabled = true;
        }
        else
        {
            soundMovement.enabled = false;
        }
    }

    private void OnTriggerExit2D(Collider2D Collision2D)
    {
        if (Collision2D.gameObject.CompareTag("Player"))
        {
            Collision2D.gameObject.transform.SetParent(null);
        }
    }
}