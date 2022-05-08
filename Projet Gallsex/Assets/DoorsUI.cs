using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorsUI : MonoBehaviour
{
    [SerializeField] private Animator animRightDoor;
    [SerializeField] private Animator animLeftDoor;
    
    private float coolDown;
    
    private bool isClosing;
    
    private string _currentRightDoorState;
    private string _currentLeftDoorState;
    
    private const string RightOpen = "DoorOpen";
    private const string RightClose = "DoorClose";  
    private const string LeftOpen = "LeftDoorOpening";
    private const string LeftClose = "LeftDoorClosing";


    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    private void Update()
    {

        coolDown -= Time.deltaTime;

        if (coolDown < 0f && isClosing)
        {
            OpenTheDoors();
            isClosing = false;
        }
    }

    public void ChangeRightDoorAnimationState(string newState)
    {
        if(_currentRightDoorState == newState) return;
        animRightDoor.Play(newState);
        _currentRightDoorState = newState;
    } 
    public void ChangeLeftDoorAnimationState(string newState)
    {
        if(_currentLeftDoorState == newState) return;
        animLeftDoor.Play(newState);
        _currentLeftDoorState = newState;
    }

    public void CloseTheDoors()
    {
        ChangeRightDoorAnimationState(RightClose);
        ChangeLeftDoorAnimationState(LeftClose);
        isClosing = true;
        coolDown = 3f;
    }
    
    public void OpenTheDoors()
    {
        ChangeRightDoorAnimationState(RightOpen);
        ChangeLeftDoorAnimationState(LeftOpen);
    }
}
