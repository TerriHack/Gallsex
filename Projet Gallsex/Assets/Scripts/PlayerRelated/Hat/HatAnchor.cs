using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HatAnchor : MonoBehaviour
{
    public Vector2 partOffset = Vector2.zero;
    public float lerpSpeed = 10f;
    [SerializeField] Transform[] hairParts;
    //private Transform hairAnchor;

    Vector2 targetPosition;
    Vector2 newPositionLerped;
    private void Update()
    {
        //Transform piecetoFollow = hairAnchor;
        for (int i = 1; i < hairParts.Length; i++)
        {
            targetPosition = (Vector2)hairParts[i-1].position + partOffset;
            newPositionLerped = Vector2.Lerp(hairParts[i].position, targetPosition, Time.deltaTime * lerpSpeed);

            hairParts[i].position = newPositionLerped;
        }
    }
}
