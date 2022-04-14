using UnityEngine;

public class PlatformMovement : MonoBehaviour
{
    public bool active;
    [SerializeField] private GameObject[] wayPoints;
    private int currentWaypointIndex = 0;

    [SerializeField] public float speed = 2f;

    private void OnTriggerEnter2D(Collider2D collision2D)
    {
        if (collision2D.gameObject.CompareTag("Player"))
        {
            collision2D.gameObject.transform.SetParent(transform);
            if (active == false)
            {
                active = true;
            }
        }
    }
    
    void Update()
    {
        if (active)
        {
            if (Vector2.Distance(wayPoints[currentWaypointIndex].transform.position, transform.position) < .1f)
            {
                currentWaypointIndex++;
                if (currentWaypointIndex >= wayPoints.Length)
                {
                    currentWaypointIndex = 0;
                }
            }
            transform.position = Vector2.MoveTowards(transform.position, wayPoints[currentWaypointIndex].transform.position,
                Time.deltaTime * speed);
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
