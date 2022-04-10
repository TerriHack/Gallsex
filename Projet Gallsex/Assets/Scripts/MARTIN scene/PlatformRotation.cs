using UnityEngine;

public class PlatformRotation : MonoBehaviour
{
    [SerializeField] private float maxTimer;
    private float timer = 0;
    private  bool State = false;
    private float rotation = 0;
    public float maxBuffer;
    private float buffer = 10;
    private bool isBuffering = false;
    [SerializeField] private Animation animator;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && buffer >= maxBuffer)
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
        Debug.Log(buffer);
    }

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
        buffering();
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

    private void buffering()
    {
        if (isBuffering)
        {
            buffer += Time.deltaTime;
            Debug.Log("isBuffering");
        }
    }
}