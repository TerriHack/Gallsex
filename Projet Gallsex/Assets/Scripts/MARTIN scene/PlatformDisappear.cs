using UnityEngine;

public class PlatformDisappear : MonoBehaviour
{
    public float timer;
    private bool willDisappear = false;
    private float time;
    
    public SpriteRenderer parentSpriteRenderer;
    public BoxCollider2D parentBoxCollider2D;
    public Animation parentAnimation;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (willDisappear == false)
        {
            willDisappear = true;
            time = 0;
            parentAnimation.Play("disappear platform anim");
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        if (willDisappear)
        {
            time = time += Time.deltaTime;
            if (time > timer)
            {
                willDisappear = false;
                parentSpriteRenderer.enabled = false;
                parentBoxCollider2D.enabled = false;
                time = 0;
                tag = "JumpableGround";
            }
        }
        else
        {
            time = time += Time.deltaTime;
            parentAnimation.Stop();
            if (time > timer)
            {
                parentSpriteRenderer.enabled = true;
                parentBoxCollider2D.enabled = true;
                time = 0;
                tag = "JumpableGround";
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        time = 0;
        willDisappear = false;
        
    }
}
