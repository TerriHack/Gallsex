using UnityEngine;

public class platformRotate : MonoBehaviour
{
    public float maxTimer = 60;
    private float timer = 0;
    private bool state = false;
    public float rotation = 0;
    public Animation animator;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (state)
            {
                
            }
            else
            {
                state = true;
                timer = 0;
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

    private void Start()
    {
        state = false;
    }


    private void Update()
    {
        if (state)
        {
            if (timer > maxTimer)
            {
                animator.Stop();
                turn();
            }
            
            else
            {
                timer = timer + Time.deltaTime;
            }
        }
    }

    private void turn()
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

        state = false;
        Debug.Log(state);
    }
}
