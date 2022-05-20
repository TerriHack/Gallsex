using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int quality;

    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogError("ALED SINGLETON");
        }
        
        instance = this;
    }
}
