using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Player Data", order = 1)]

public class PlayerData : ScriptableObject
{
    [Header("Forces")] 
    public float jumpForce;
    [Tooltip("The Speed cannot exceed 100")]
    public float speed;
    public float airState;
}
