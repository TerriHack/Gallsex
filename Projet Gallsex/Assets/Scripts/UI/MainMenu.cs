using System;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    [SerializeField] private DoorsUI doors;
    [SerializeField] private GameObject levelSelectionMenu;
    public GameObject firstButtonSelected, firstOptionButton, selectionClosedButton, firstLevelSelectionButton;

    private float coolDown;

    private bool _playing;
    private bool selectionMenuOn;

    private string _selectedScene;


    private void Awake()
    {
        Time.timeScale = 1f;
    
        _selectedScene = "Level_Tuto_Scene";
        
        _playing = false;
        
        //Reset the event system
        EventSystem.current.SetSelectedGameObject(null);
        //Set the new state in the event system
        EventSystem.current.SetSelectedGameObject(firstButtonSelected);
    }

    private void Update()
    {
        coolDown -= Time.deltaTime;
        StartGame();
        
        if (Input.GetButtonDown("Cancel") && selectionMenuOn)
        {
            StartCoroutine(CloseLevelSelectionMenu());
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
        doors.CloseTheDoors();
        coolDown = 1.5f;
        _playing = true;
        _selectedScene = "Level_Tuto_Scene";
    }
    
    public void StartGameInLevel1()
    {
        doors.CloseTheDoors();
        coolDown = 1.5f;
        _playing = true;
        _selectedScene = "Level_1_Scene";
    }
    
    public void StartGameInLevel2()
    {
        doors.CloseTheDoors();
        coolDown = 1.5f;
        _playing = true;
        _selectedScene = "Level_2_Scene";
    }
    
    public void StartGameInBoss()
    {
        doors.CloseTheDoors();
        coolDown = 1.5f;
        _playing = true;
        _selectedScene = "Level_Boss_Scene";
    }

    IEnumerator CloseLevelSelectionMenu()
    {
        doors.OpenSelectionMenu();

        //Reset the event system
        EventSystem.current.SetSelectedGameObject(null);
        //Set the new state in the event system
        EventSystem.current.SetSelectedGameObject(selectionClosedButton);

        yield return new WaitForSeconds(0.7f);
        
        levelSelectionMenu.SetActive(false);
    }
}
