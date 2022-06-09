using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject lvlTutoButton;
    [SerializeField] private GameObject lvl1Button;
    [SerializeField] private GameObject lvl2Button;
    [SerializeField] private GameObject lvlBossButton;
    [SerializeField] private GameObject lvlTuto;
    [SerializeField] private GameObject lvl1;
    [SerializeField] private GameObject lvl2;
    [SerializeField] private GameObject lvlBoss;
    [SerializeField] private AudioSource soundUI;
    [SerializeField] private AudioClip navSound;
    [SerializeField] private GameObject runVFX;
    [SerializeField] private GameObject optionVFX;
    [SerializeField] private GameObject scoreVFX;
    [SerializeField] private GameObject quitVFX;
    [SerializeField] private GameObject selectionVFX;
    [SerializeField] private GameObject lvlSelection;
    [SerializeField] private GameObject quit;
    [SerializeField] private GameObject option;
    [SerializeField] private GameObject score;
    [SerializeField] private GameObject run;
    [SerializeField] private ScoreMenu scoreMenuScript;
    [SerializeField] private GameObject gameManagerPrefab;
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private DoorsUI doors;
    [SerializeField] private CloudsUI clouds;
    [SerializeField] private GameObject levelSelectionMenu;
    [SerializeField] private GameObject optionMenu;
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject scoreMenu;
    [SerializeField] private GameObject pressAnyButtonScreen;
    [SerializeField] private TMP_Dropdown resolutionDropdown;
    [SerializeField] private Animation pressAnyButtonTxt;
    [SerializeField] private Animator blackScreen;
    public GameObject firstButtonSelected, firstOptionButton ,selectionClosedButton, firstLevelSelectionButton, optionClosedButton;
    [SerializeField] private MusicDisplayer music;
    
    
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
        
        _selectedScene = "Level_Tuto_Scene";
        
        _playing = false;

        StartCoroutine(Beginning());
    }

    private void Start()
    {
        if (GameObject.FindWithTag("GameManager") == null)
        {
            Instantiate(gameManagerPrefab);
        }
        
        _gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();

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

        if (EventSystem.current.currentSelectedGameObject == lvlSelection)
        {
            selectionVFX.SetActive(true);
            runVFX.SetActive(false);
            optionVFX.SetActive(false);
            quitVFX.SetActive(false);
            scoreVFX.SetActive(false);
        }
        
        if (EventSystem.current.currentSelectedGameObject == quit)
        {
            selectionVFX.SetActive(false);
            runVFX.SetActive(false);
            optionVFX.SetActive(false);
            quitVFX.SetActive(true);
            scoreVFX.SetActive(false);
        }
        
        if (EventSystem.current.currentSelectedGameObject == run)
        {
            selectionVFX.SetActive(false);
            runVFX.SetActive(true);
            optionVFX.SetActive(false);
            quitVFX.SetActive(false);
            scoreVFX.SetActive(false);
        }
        
        if (EventSystem.current.currentSelectedGameObject == score)
        {
            
            selectionVFX.SetActive(false);
            runVFX.SetActive(false);
            optionVFX.SetActive(false);
            quitVFX.SetActive(false);
            scoreVFX.SetActive(true);
        }   
        
        if (EventSystem.current.currentSelectedGameObject == option)
        {
            selectionVFX.SetActive(false);
            runVFX.SetActive(false);
            optionVFX.SetActive(true);
            quitVFX.SetActive(false);
            scoreVFX.SetActive(false);
        }

        if (EventSystem.current.currentSelectedGameObject == lvlTutoButton)
        {
            lvlTuto.SetActive(true);
            lvl1.SetActive(false);
            lvl2.SetActive(false);
            lvlBoss.SetActive(false);
        }
        
        if (EventSystem.current.currentSelectedGameObject == lvl1Button)
        {
            lvlTuto.SetActive(false);
            lvl1.SetActive(true);
            lvl2.SetActive(false);
            lvlBoss.SetActive(false);
        }
        
        if (EventSystem.current.currentSelectedGameObject == lvl2Button )
        {
            lvlTuto.SetActive(false);
            lvl1.SetActive(false);
            lvl2.SetActive(true);
            lvlBoss.SetActive(false);
        }   
        
        if (EventSystem.current.currentSelectedGameObject == lvlBossButton)
        { 
            lvlTuto.SetActive(false);
            lvl1.SetActive(false);
            lvl2.SetActive(false);
            lvlBoss.SetActive(true);
        }

        if (Input.GetButtonDown("Pause") && scoreMenuOn)
        {
            ResetTimers();
            scoreMenuScript.SetTheScore();
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

    private void ResetTimers()
    {
        
        PlayerPrefs.SetFloat("goldTime",0);
        PlayerPrefs.SetFloat("silverTime",0); 
        PlayerPrefs.SetFloat("bronzeTime",0);
        
        PlayerPrefs.SetFloat("bestLevel1Time",0);
        PlayerPrefs.SetFloat("bestLevel2Time",0);
        PlayerPrefs.SetFloat("bestLevel3Time",0);
        PlayerPrefs.SetFloat("bestLevel4Time",0);
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
        blackScreen.SetBool("isPressed", true);

        yield return new WaitForSeconds(1f);
        pressAnyButtonScreen.SetActive(false);
        _splashScreened = false;
        mainMenu.SetActive(true);

        yield return new WaitForSeconds(0.2f);
        
        blackScreen.SetBool("isPressed", false);
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
