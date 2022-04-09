using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class NuageSauveur : MonoBehaviour
{
    [SerializeField] private List<GameObject> triggers;
    [SerializeField] private GameObject[] bossWaypoints;
    public GameObject target;
    public GameObject cloud;
    public float speed;
    public bool bossTime;

    private void Start()
    {
        for (int i = 0; i < target.transform.childCount; i++)
        {
            triggers.Add(target.transform.GetChild(i).gameObject);
            triggers[i].GetComponent<triggerNuage>().variable = i;
        }
    }

    public void Action(int Go,float AppearBelow)
    { 
        DOTween.To( () => cloud.transform.position, x => cloud.transform.position = x,
            new Vector3(cloud.transform.position.x, triggers[Go].transform.position.y - AppearBelow), speed);


    }

    private void Update()
    {
        if (bossTime)
        {
            // à faire pour le boss fight
        }
    }
}
