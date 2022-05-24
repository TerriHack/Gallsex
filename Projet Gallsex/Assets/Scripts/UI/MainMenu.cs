using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.InputSystem.Controls;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class MainMenu : MonoBehaviour
{

    [SerializeField] private GameManager _gameManager;
    [SerializeField] private DoorsUI doors;
    [SerializeField] private CloudsUI clouds;
    [SerializeField] private GameObject levelSelectionMenu;
    [SerializeField] private GameObject optionMenu;
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject scoreMenu;
    [SerializeField] private GameObject pressAnyButtonScreen;
    [SerializeField] private TMP_Dropdown resolutionDropdown;
    [SerializeField] private Animation title;
    [SerializeField] private Animation pressAnyButtonTxt;
    [SerializeField] private Animator blackScreen;
    public GameObject firstButtonSelected, firstOptionButton ,selectionClosedButton, firstLevelSelectionButton, optionClosedButton;

    private float coolDown;

    private bool canPress;
    public bool _playing;
    public bool selectionMenuOn;
    public bool optionMenuOn;
    public bool scoreMenuOn;
    public bool _splashScreened;

    private string _selectedScene;

    private Resolution[] _resolutions;


    private void Awake()
    {
        Time.timeScale = 1f;
        
        blackScreen.SetTrigger("isBeginning");
        
        _splashScreened = true;
        pressAnyButtonTxt.Play();
        title.Play();
        
        _selectedScene = "Level_Tuto_Scene";
        
        _playing = false;

        StartCoroutine(Beginning());
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
        coolDown -= Time.deltaTime;
        StartGame();

        if (canPress)
        {
            if (Input.GetButtonDown("Jump") && _splashScreened)
            {
                StartCoroutine(SplashScreenToMain());
            }
        
            if (Input.GetButtonDown("Cancel") && selectionMenuOn)
            {
                StartCoroutine(CloseLevelSelectionMenu());
            }else if(Input.GetButtonDown("Cancel") && optionMenuOn)
            {
                StartCoroutine(CloseOptionMenu());
            }else if (Input.GetButtonDown("Cancel") && scoreMenuOn)
            {
                CloseScoreMenu();
            }
        }
    }

    public void Play()
    {
        doors.CloseTheDoors();
        coolDown = 1.5f;
        _playing = true;
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void LevelSelectionMenu()
    {
        levelSelectionMenu.SetActive(true);
        doors.CloseSelectionMenu();
        EventSystem.current.SetSelectedGameObject(null);
        //Set the new state in the event system
        EventSystem.current.SetSelectedGameObject(firstLevelSelectionButton);
        selectionMenuOn = true;
    }

    private void StartGame()
    {
        if (coolDown < 0f && _playing)
        {
            //Pour la prochaine fois SceneManager.LoadScene(Select scene);
            SceneManager.LoadScene(_selectedScene);
        }
    }

    public void StartGameInTuto()
    {
        _selectedScene = "Level_Tuto_Scene";
        StartCoroutine(StartWithLevelSelectionMenu());
    }
    
    public void StartGameInLevel1()
    {
        _selectedScene = "Level_1_Scene";
        StartCoroutine(StartWithLevelSelectionMenu());
    }
    
    public void StartGameInLevel2()
    {
        _selectedScene = "Level_2_Scene";
        StartCoroutine(StartWithLevelSelectionMenu());
    }
    
    public void StartGameInBoss()
    {
        _selectedScene = "Level_Boss_Scene";
        StartCoroutine(StartWithLevelSelectionMenu());
    }

    IEnumerator CloseLevelSelectionMenu()
    {
        doors.OpenSelectionMenu();

        selectionMenuOn = false;
        
        //Reset the event system
        EventSystem.current.SetSelectedGameObject(null);
        //Set the new state in the event system
        EventSystem.current.SetSelectedGameObject(selectionClosedButton);

        yield return new WaitForSeconds(0.7f);
        
        levelSelectionMenu.SetActive(false);
    }
    
    IEnumerator StartWithLevelSelectionMenu()
    {
        doors.OpenSelectionMenu();

        yield return new WaitForSeconds(1f);
        
        levelSelectionMenu.SetActive(false);
        
        doors.CloseTheDoors();
        coolDown = 1.5f;
        _playing = true;
    }
    
    IEnumerator CloseOptionMenu()
    {
        optionMenuOn = false;
        doors.OpenOptionMenu();

        //Reset the event system
        EventSystem.current.SetSelectedGameObject(null);
        //Set the new state in the event system
        EventSystem.current.SetSelectedGameObject(optionClosedButton);

        yield return new WaitForSeconds(0.7f);
        
       optionMenu.SetActive(false);
    }
    
    public void OpenScoreMenu()
    {
        StartCoroutine(CloudsFalling());
        scoreMenuOn = true;
    }
    
    public void CloseScoreMenu()
    {
        StartCoroutine(CloudsRising());
        scoreMenuOn = false;
    }

    IEnumerator CloudsFalling()
    {
        
        clouds.ToScoreMenu();
        
        yield return new WaitForSeconds(0.7f);

        mainMenu.SetActive(false);
        scoreMenu.SetActive(true);
    }  
    
    IEnumerator CloudsRising()
    {
        clouds.ToMainMenu();

        yield return new WaitForSeconds(0.7f);
        
        mainMenu.SetActive(true);
        scoreMenu.SetActive(false);
    }
    
    public void OptionMenu()
    {
        optionMenu.SetActive(true);
        doors.CloseOptionMenu();
        EventSystem.current.SetSelectedGameObject(null);
        //Set the new state in the event system
        EventSystem.current.SetSelectedGameObject(firstOptionButton);
        optionMenuOn = true;
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
        _gameManager.quality = qualityIndex;
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

    IEnumerator SplashScreenToMain()
    {
        blackScreen.SetBool("levelFinished", true);

        yield return new WaitForSeconds(1f);
        blackScreen.SetTrigger("levelTransition");
        pressAnyButtonScreen.SetActive(false);
        _splashScreened = false;
        mainMenu.SetActive(true);

        yield return new WaitForSeconds(0.2f);
        
        blackScreen.SetBool("levelFinished", false);
        //Reset the event system
        EventSystem.current.SetSelectedGameObject(null);
        //Set the new state in the event system
        EventSystem.current.SetSelectedGameObject(firstButtonSelected);
        
    }

    IEnumerator Beginning()
    {
        yield return new WaitForSeconds(2f);
        canPress = true;
    }
}
