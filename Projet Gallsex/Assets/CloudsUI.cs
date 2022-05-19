using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudsUI : MonoBehaviour
{

    [SerializeField] private Animator anim;

    private string currentCloudsState;
    
    private bool _scoreMenuOn;
    
    private const string cloudsFalling = "CloudsToScoreMenu";
    private const string cloudsRising = "CloudsToMainMenu";
    
    public void ChangeAnimationState(string newState)
    {
        if(currentCloudsState == newState) return;
        anim.Play(newState);
        currentCloudsState = newState;
    }
    
    public void ToScoreMenu()
    {
        ChangeAnimationState(cloudsFalling);
    }
    
    public void ToMainMenu()
    {
        ChangeAnimationState(cloudsRising);
    }
}
