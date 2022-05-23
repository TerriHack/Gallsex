using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RepositionWaypointsBoss : MonoBehaviour
{
    public List<Vector3> newPosition;
    void Start()
    {
        List<GameObject> list = GameObject.FindGameObjectsWithTag("BossWaypoint").ToList();
        for (int i = 0; i < list.Count; i++)
        {
            list[i].transform.position = new Vector3(newPosition[i].x, newPosition[i].y, -10);

        }
    }
}
