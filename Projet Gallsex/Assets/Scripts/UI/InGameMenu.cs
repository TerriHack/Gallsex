using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace UI
{
    public class InGameMenu : MonoBehaviour
    {
        [SerializeField] private Animator anim;
        [SerializeField] private GameObject optionMenu;
        [SerializeField] private TMP_Dropdown resolutionDropdown;
        public GameObject firstButtonSelected, firstOptionButton, optionClosedButton;

        private bool _isPaused;
    
        private const String Paused = "Pause";
        private const String UnPaused = "UnPause";
    
        private string _currentState;
        
        private Resolution[] _resolutions;

        private void Awake()
        {
            QualitySettings.SetQualityLevel(GameManager.instance.quality);
        }

        private void Start()
        {
            _resolutions = Screen.resolutions;
        
            resolutionDropdown.ClearOptions();

            List<string> options = new List<string>();

            int currentResolutionIndex = 0;
            for (int i = 0; i < _resolutions.Length; i++)
            {
                string option = _resolutions[i].width + "x" + _resolutions[i].height;
                options.Add(option);

                if (_resolutions[i].width == Screen.currentResolution.width && _resolutions[i].height == Screen.currentResolution.height)
                {
                    currentResolutionIndex = i;
                }
            } 

            resolutionDropdown.AddOptions(options);
            resolutionDropdown.value = currentResolutionIndex;
            resolutionDropdown.RefreshShownValue();
        }

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
                
                //Reset the event system
                EventSystem.current.SetSelectedGameObject(null);
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
        
        public void SetQuality(int qualityIndex)
        {
            QualitySettings.SetQualityLevel(qualityIndex);
            GameManager.instance.quality = qualityIndex;
        }

        public void SetFullscreen(bool isFullscreen)
        {
            Screen.fullScreen = isFullscreen;
        }

        public void SetResolution(int resolutionIndex)
        {
            Resolution resolution = _resolutions[resolutionIndex];
            Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        }
    }
}
