using System;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    [SerializeField] private DoorsUI doors;
    public GameObject firstButtonSelected, firstOptionButton, optionClosedButton;

    private float coolDown;

    private bool _playing;

    private void Awake()
    {
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
    


    private void StartGame()
    {
        if (coolDown < 0f && _playing)
        {
            //Pour la prochaine fois SceneManager.LoadScene(Select scene);
            SceneManager.LoadScene("Level_Tuto_Scene");
        }
    }
}
