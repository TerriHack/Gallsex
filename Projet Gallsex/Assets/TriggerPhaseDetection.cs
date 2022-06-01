using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerPhaseDetection : MonoBehaviour
{
    [SerializeField] private CameraBoss bossCam;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.CompareTag("BossTrigger"))
        {
            bossCam.phaseCounter += 1;
            Destroy(col.gameObject);
        }
    }
}
