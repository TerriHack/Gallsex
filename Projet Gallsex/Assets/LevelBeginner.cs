using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBeginner : MonoBehaviour
{

    [SerializeField] private Animator blackScreen;
    [SerializeField] private PlayerBetterController playerController;

    private void Start()
    {
        blackScreen.SetTrigger("isBeginning");
        StartCoroutine(Begining());
    }

    IEnumerator Begining()
    {
        playerController.levelBeginning = true;
        yield return new WaitForSeconds(0.5f);
        playerController.levelBeginning = false;
    }
}
