using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

namespace UI
{
    public class InGameMenu : MonoBehaviour
    {
        [SerializeField] private Animator anim;
        [SerializeField] private GameObject optionMenu;
        public GameObject firstButtonSelected, firstOptionButton, optionClosedButton;

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
                optionMenu.SetActive(false);
            
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
            if(_isPaused) optionMenu.SetActive(true);
        
            //Reset the event system
            EventSystem.current.SetSelectedGameObject(null);
            //Set the new state in the event system
            EventSystem.current.SetSelectedGameObject(firstOptionButton);
        }

        public void Quit()
        {
            SceneManager.LoadScene("Main_Menu_Scene");
        }
        public void ChangeAnimationState(string newState)
        {
            if(_currentState == newState) return;
            anim.Play(newState);
            _currentState = newState;
        }
    }
}
