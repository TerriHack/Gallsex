using UnityEngine;

public class PlatformRotation : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Animation animator;
    [Header("90 for Vertical platform / 0 for Horizontal platform")]
    [SerializeField] private float rotation = 0;
    [SerializeField] private float maxTimer;
    [SerializeField] private float maxBuffer;
    [SerializeField] private bool State = false;
    private float buffer = 10;
    private bool isBuffering = false; 
    private float timer = 0;

    private void Start()
    {
        State = false;
    }
    private void Update()
    {
        if (State)
        {
            if (timer > maxTimer)
            {
                animator.Stop();
                Turn();
            }

            else
            {
                timer += Time.deltaTime;
            }
        }
        Buffering();
    }
    private void Turn()
    {
        if (rotation < 80)
        {
            animator.Play("rotate platform 0to90");
            rotation = 90;

        }
        else if (rotation > 10)
        {
            animator.Play("rotate platform 90to0");
            rotation = 0;
        }
        

        State = false;
        //buffer = 0;
        isBuffering = true;
    }
    private void Buffering()
    {
        if (isBuffering)
        {
            buffer += Time.deltaTime;
            Debug.Log("isBuffering");
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Checks") && buffer >= maxBuffer)
        {
            if (!State)
            {
                State = true;
                timer = 0;
                buffer = 0;
            }

            if (rotation == 0)
            {
                animator.Play("rotate platform anim 2");
            }
            else if(rotation == 90)
            {
                animator.Play("rotate platform anim 1");
            }
        }
    }
}