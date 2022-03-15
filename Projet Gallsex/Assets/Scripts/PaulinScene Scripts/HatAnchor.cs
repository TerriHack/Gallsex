using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HatAnchor : MonoBehaviour
{
    public Vector2 partOffset = Vector2.zero;
    public float lerpSpeed = 10f;
    private Transform[] hairParts;
    private Transform hairAnchor;
    private void Awake()
    {
        hairAnchor = GetComponent<Transform>();
        hairParts = GetComponentsInChildren<Transform>();
    }

    private void Update()
    {
        Transform piecetoFollow = hairAnchor;

        foreach(Transform hairPart in hairParts)
        {
            if (!hairPart.Equals(hairAnchor))
            {
                Vector2 targetPosition = (Vector2)piecetoFollow.position + partOffset;
                Vector2 newPositionLerped = Vector2.Lerp(hairPart.position, targetPosition, Time.deltaTime * lerpSpeed);

                hairPart.position = newPositionLerped;
                piecetoFollow = hairPart;
            }
        }
    }
}
