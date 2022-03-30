using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NuageSauveur : MonoBehaviour
{
    [SerializeField] private List<GameObject> Triggers;
    public GameObject target;
    public GameObject Cloud;
    public float SPEED;

    private void Start()
    {
        for (int i = 0; i < target.transform.childCount; i++)
        {
            Triggers.Add(target.transform.GetChild(i).gameObject);
            Triggers[i].GetComponent<triggerNuage>().variable = i;
        }
    }

    public void Action(int Go,float AppearBelow)
    {
        Cloud.transform.position = new Vector2(Triggers[Go].transform.position.x,Triggers[Go].transform.position.y - AppearBelow);
    }
}
