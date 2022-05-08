using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;


public class InGameMenu : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private GameObject OptionMenu;
    public GameObject firstButtonSelected, firstOptionButton, OptionClosedButton;

    private bool _isPaused;
    
    private const String Paused = "Pause";
    private const String UnPaused = "UnPause";
    
    private string _currentState;

    private void Update()
    {
        if (Input.GetButtonDown("Pause"))
        {
            Pause();
        }

        if (Input.GetButtonDown("Cancel"))
        {
            OptionMenu.SetActive(false);
            
            //Reset the event system
            EventSystem.current.SetSelectedGameObject(null);
            //Set the new state in the event system
            EventSystem.current.SetSelectedGameObject(firstButtonSelected);
        }
    }
    private void Pause()
    {
        _isPaused = !_isPaused;

        if (_isPaused)
        {
            Time.timeScale = 0f;
            ChangeAnimationState(Paused);
            
            //Reset the event system
            EventSystem.current.SetSelectedGameObject(null);
            //Set the new state in the event system
            EventSystem.current.SetSelectedGameObject(firstButtonSelected);
        }
        else
        {
            Time.timeScale = 1f;
            ChangeAnimationState(UnPaused);
        }
        
    }

    public void Resume()
    {
        if (_isPaused)
        {
            Time.timeScale = 1f;
            ChangeAnimationState(UnPaused);
            _isPaused = false;
        }

    }
    
    public void Restart()
    {
        if (_isPaused)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            Time.timeScale = 1f;
        }

    }

    public void Option()
    {
        if(_isPaused) OptionMenu.SetActive(true);
        
        //Reset the event system
        EventSystem.current.SetSelectedGameObject(null);
        //Set the new state in the event system
        EventSystem.current.SetSelectedGameObject(firstOptionButton);
    }

    // private void Quit()
    // {
    //     SceneManager.LoadScene("MainMenu");
    // }
    public void ChangeAnimationState(string newState)
    {
        if(_currentState == newState) return;
        anim.Play(newState);
        _currentState = newState;
    }
}
