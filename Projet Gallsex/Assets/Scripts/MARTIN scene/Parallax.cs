using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    private float length,height, startposX, startposY;
    public bool repeatable = false;
    public float offsetX, offsetY,boundsMinX,boundsMinY,boundsMaxX,boundsMaxY;

    public GameObject cam;

    public float parallaxEffect;
    // Start is called before the first frame update
    void Start()
    {
        startposX = transform.position.x;
        startposY = transform.position.y;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
        height = GetComponent<SpriteRenderer>().bounds.size.y;
    }

    // Update is called once per frame
    void Update()
    { 
        float distX = (cam.transform.position.x * parallaxEffect);
        float distY = (cam.transform.position.y * parallaxEffect);

        transform.position = new Vector3(startposX + distX + offsetX, startposY + distY + offsetY, transform.position.z);

        if (repeatable)
        {
            infiniteScroll();
        }

        if (transform.position.x > boundsMaxX) transform.position = new Vector2(boundsMaxX,transform.position.y);
        else if (transform.position.x < boundsMinX) transform.position = new Vector2(boundsMinX, transform.position.y);
        else if (transform.position.y > boundsMaxY) transform.position = new Vector2(transform.position.x,boundsMaxY);
        else if (transform.position.y < boundsMinY) transform.position = new Vector2(transform.position.x, boundsMinY);
        

    }

    void infiniteScroll()
    {
        
        float tempX = cam.transform.position.x * (1 - parallaxEffect);
        float tempY = cam.transform.position.y * (1 - parallaxEffect);
        
        if (tempX > startposX + length) startposX += length;
        else if (tempX < startposX - length) startposX -= length;
        else if (tempY > startposY + height / 2)
        {
            startposY += height;
        }
        else if (tempY < startposY - height / 2)
        {
            startposY -= height ;
        }
    }
    
}
