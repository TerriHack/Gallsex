using UnityEngine;

public class Parallax : MonoBehaviour
{
    private float length, height, startPosX, startPosY;
    public bool repeatable = false;
    public float offsetX, offsetY,boundsMinX,boundsMinY,boundsMaxX,boundsMaxY;

    public GameObject cam;

    public float parallaxEffect;
    void Start()
    {
        startPosX = transform.position.x;
        startPosY = transform.position.y;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
        height = GetComponent<SpriteRenderer>().bounds.size.y;
    }
    
    void Update()
    { 
        float distX = (cam.transform.position.x * parallaxEffect);
        float distY = (cam.transform.position.y * parallaxEffect);

        transform.position = new Vector3(startPosX + distX + offsetX, startPosY + distY + offsetY, transform.position.z);

        if (repeatable)
        {
            InfiniteScroll();
        }

        if (transform.position.x > boundsMaxX) transform.position = new Vector2(boundsMaxX,transform.position.y);
        else if (transform.position.x < boundsMinX) transform.position = new Vector2(boundsMinX, transform.position.y);
        else if (transform.position.y > boundsMaxY) transform.position = new Vector2(transform.position.x,boundsMaxY);
        else if (transform.position.y < boundsMinY) transform.position = new Vector2(transform.position.x, boundsMinY);
        

    }

    void InfiniteScroll()
    {
        
        float tempX = cam.transform.position.x * (1 - parallaxEffect);
        float tempY = cam.transform.position.y * (1 - parallaxEffect);
        
        if (tempX > startPosX + length) startPosX += length;
        else if (tempX < startPosX - length) startPosX -= length;
        else if (tempY > startPosY + height / 2)
        {
            startPosY += height;
        }
        else if (tempY < startPosY - height / 2)
        {
            startPosY -= height ;
        }
    }
    
}
