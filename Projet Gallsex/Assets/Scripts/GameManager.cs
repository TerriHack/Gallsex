using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int quality;
    
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        quality = 2;
    }
}
