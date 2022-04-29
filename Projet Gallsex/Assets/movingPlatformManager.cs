using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movingPlatformManager : MonoBehaviour
{
    public List<GameObject> childrenMovingPlatforms;
    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform child in transform)
        {
            childrenMovingPlatforms.Add(child.gameObject);
        }
    }
}
