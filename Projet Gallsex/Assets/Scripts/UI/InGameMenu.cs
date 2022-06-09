using System;
using System.Collections.Generic;
using Boss;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace UI
{
    public class InGameMenu : MonoBehaviour
    {
        [SerializeField] private Toggle accessibilityToggle;
        [SerializeField] private GameManager _gameManager;
        [SerializeField] private Animator anim;
        [SerializeField] private GameObject optionMenu;
        [SerializeField] private GameObject quitMenu;
        [SerializeField] private TMP_Dropdown resolutionDropdown;
        private GameObject spikes;
        [SerializeField] private GameObject accessibilitySpikes;
        private prefabTimer timer;
        public GameObject firstButtonSelected, firstOptionButton, optionClosedButton, firstQuitButton;

        public bool _isPaused;
        public bool _quitMenued;
        public bool _optionMenued;
        public bool _noSpikes;

        private const String Paused = "Pause";
        private const String UnPaused = "UnPause";
    
        private string _currentState;
        
        private Resolution[] _resolutions;

        //**************************************************************

        private void Awake()
        {
            _gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
            timer = GetComponentInChildren<prefabTimer>();
            spikes = GameObject.FindWithTag("Spikes");
            accessibilitySpikes = GameObject.FindWithTag("noSpikes");
        }
        private void Start()
        {
            QualitySettings.SetQualityLevel(PlayerPrefs.GetInt("Quality"));

            if (PlayerPrefs.GetInt("SpikesOn") == 1) accessibilityToggle.isOn = true;
            else accessibilityToggle.isOn = false;

            QualitySettings.SetQualityLevel(_gameManager.quality);
            
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
                if (_optionMenued)
                {
                    optionMenu.SetActive(false);
                    _optionMenued = false;
                }
                else if(_quitMenued)
                {
                    quitMenu.SetActive(false);
                    _quitMenued = false;
                }
                else
                {
                    Pause();
                }
                
            }

            if (Input.GetButtonDown("Cancel"))
            {
                if (!_quitMenued && !_optionMenued)
                {
                    Resume();
                }
            }
            
            if (Input.GetButtonDown("Cancel"))
            {
                if (_optionMenued)
                {
                    optionMenu.SetActive(false);
                    _optionMenued = false;
                }

                if (_quitMenued)
                {
                    quitMenu.SetActive(false);
                    _quitMenued = false;
                }
                
                LockMenu();
                
                //Reset the event system
                EventSystem.current.SetSelectedGameObject(null);
                //Set the new state in the event system
                EventSystem.current.SetSelectedGameObject(firstButtonSelected);
            }

            if (!_isPaused) EventSystem.current.SetSelectedGameObject(null);
        }
        
        //**************************************************************
        
        private void Pause()
        {
            _isPaused = !_isPaused;

            if (_isPaused)
            {
                _gameManager.timerActive = false;
                Time.timeScale = 0f;
                ChangeAnimationState(Paused);
            
                //Reset the event system
                EventSystem.current.SetSelectedGameObject(null);
                //Set the new state in the event system
                EventSystem.current.SetSelectedGameObject(firstButtonSelected);
            }
            else
            {
                _gameManager.timerActive = true;
                Time.timeScale = 1f;
                ChangeAnimationState(UnPaused);
                
                //Reset the event system
                EventSystem.current.SetSelectedGameObject(null);
            }
        
        }
        private void LockMenu()
        {
            if (!_isPaused)
            {
                _quitMenued = false;
                _optionMenued = false;
            }
        }
        public void Resume()
        {
            if (_isPaused)
            {
                Time.timeScale = 1f;
                ChangeAnimationState(UnPaused);
                _isPaused = false;
                optionMenu.SetActive(false);
            }
        }
        public void Restart()
        {
            if (_isPaused)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                Time.timeScale = 1f;

                if (SceneManager.GetActiveScene().name == "Level_Boss_Scene")
                {
                    _gameManager.GetComponent<AudioSource>().Stop();
                }
                {
                    
                }
            }
        }
        public void Option()
        {
            if (_isPaused)
            {
                _optionMenued = true;
                optionMenu.SetActive(true);
            }
        
            //Reset the event system
            EventSystem.current.SetSelectedGameObject(null);
            //Set the new state in the event system
            EventSystem.current.SetSelectedGameObject(firstOptionButton);
        }
        public void QuitButton()
        {
            _quitMenued = true;
            quitMenu.SetActive(true);
            
            //Reset the event system
            EventSystem.current.SetSelectedGameObject(null);
            //Set the new state in the event system
            EventSystem.current.SetSelectedGameObject(firstQuitButton);
        }
        public void YesButton()
        {
            SceneManager.LoadScene("Main_Menu_Scene");
        }
        public void NoButton()
        {
            quitMenu.SetActive(false);
            _quitMenued = true;
            
            //Reset the event system
            EventSystem.current.SetSelectedGameObject(null);
            //Set the new state in the event system
            EventSystem.current.SetSelectedGameObject(firstButtonSelected);
        }
        public void SetAccessibilitySpikes()
        {
            int spikesOn;
            
            if(!_noSpikes) accessibilitySpikes.SetActive(true);
            else accessibilitySpikes.SetActive(false);
            
            if (_noSpikes) spikesOn = 1;
            else spikesOn = 0;
            
            PlayerPrefs.SetInt("SpikesOn", spikesOn);
        }
        
        

        public void AccessibilityToggle(bool noSpikes)
        {
            _noSpikes = noSpikes;
            spikes.SetActive(noSpikes);
            SetAccessibilitySpikes();
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
            _gameManager.quality = qualityIndex;
            
            PlayerPrefs.SetInt("Quality", qualityIndex);
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
