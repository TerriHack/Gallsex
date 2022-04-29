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

    public void OnPlayerDeath()
    {
        for (int i = 0; i < childrenMovingPlatforms.Count; i++)
        {
            childrenMovingPlatforms[i].transform.GetChild(0).GetComponent<PlatformMovement>().active = false;
            childrenMovingPlatforms[i].transform.GetChild(0).transform.position = childrenMovingPlatforms[i].transform.GetChild(1).transform.position;
        }
    }
}
