using System.Collections.Generic;
using UnityEngine;

public class ArrayCheckpoint : MonoBehaviour
{
    public List<Vector2> checkpointArray;

    public void AddingCheckpoint(Vector2 newCheckpoint)
    {
        if (checkpointArray.Count > 0)
        {
            for (int i = 0; i < checkpointArray.Count; i++)
            {
                if (checkpointArray[i] == newCheckpoint)
                {
                    checkpointArray.RemoveAt(i);
                }
            }
        }
        
        checkpointArray.Add(newCheckpoint);
    }

    private void Start()
    {
        checkpointArray[0] = gameObject.transform.position;
    }
}
