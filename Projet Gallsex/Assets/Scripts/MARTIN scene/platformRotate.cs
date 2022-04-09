using UnityEngine;

public class platformRotate : MonoBehaviour
{
    public float MaxTimer = 60;
    private float Timer = 0;
    public  bool State = false;
    public float rotation = 0;
    public Animation animator;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (State)
            {
                
            }
            else
            {
                State = true;
                Timer = 0;
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
        State = false;
    }


    private void Update()
    {
        if (State)
        {
            if (Timer > MaxTimer)
            {
                animator.Stop();
                turn();
            }
            
            else
            {
                Timer = Timer + Time.deltaTime;
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

        State = false;
        Debug.Log(State);
    }
}
