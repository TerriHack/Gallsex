using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class triggerland : MonoBehaviour
{
    public ParticleSystem DustLand;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            DustLand.Play();
        }
    }
}
