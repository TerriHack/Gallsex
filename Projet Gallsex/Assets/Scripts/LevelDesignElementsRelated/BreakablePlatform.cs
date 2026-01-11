using UnityEngine;

public class BreakablePlatform : MonoBehaviour
{
    public float timer;
    private bool willDisappear = false;
    private float time;
    public bool active = false;
    
    public SpriteRenderer spriteRenderer;
    public BoxCollider2D boxCollider2D;
    public Animation anim;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (active == false && other.CompareTag("Player"))
        {
            willDisappear = true;
            time = 0;
            anim.Play();
            active = true;
        }
    }
    
    void Update()
    {
        if (active)
        {
            if (willDisappear)
            {
                time = time += Time.deltaTime;
                if (time > timer)
                {
                    willDisappear = false;
                    spriteRenderer.enabled = false;
                    boxCollider2D.enabled = false;
                    time = 0;
                    tag = "JumpableGround";
                }
            }
            else
            {
                time = time += Time.deltaTime;
                anim.Stop();
                if (time > timer)
                {
                    spriteRenderer.enabled = true;
                    boxCollider2D.enabled = true;
                    time = 0;
                    tag = "JumpableGround";
                    active = false;
                }
            }
        }

        /*
        if (willDisappear)
        {
            time = time += Time.deltaTime;
            if (time > timer)
            {
                willDisappear = false;
                spriteRenderer.enabled = false;
                boxCollider2D.enabled = false;
                time = 0;
                tag = "JumpableGround";
            }
        }
        else
        {
            time = time += Time.deltaTime;
            animation.Stop();
            if (time > timer)
            {
                spriteRenderer.enabled = true;
                boxCollider2D.enabled = true;
                time = 0;
                tag = "JumpableGround";
            }
        }*/
    }

}
