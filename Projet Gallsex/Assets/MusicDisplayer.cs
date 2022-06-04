using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicDisplayer : MonoBehaviour
{
    [SerializeField] AudioSource musicDisplayer;
    public AudioClip[] music;
    private TeleporterToNextLevel tp;
    private bool menuMusicDisplayed;
    private bool musicDisplayed;
    private bool stopSearching;

    public void PlayMenuTheme()
    {
        musicDisplayer.clip = music[1];
        musicDisplayer.Play();
    }

    public void PlayMainTheme()
    {
        musicDisplayer.clip = music[0];
        musicDisplayer.Play();
    }

    public void PlayBossTheme()
    {
        musicDisplayer.clip = music[2];
        musicDisplayer.Play();

    }

    private void Update()
    {
        if(SceneManager.GetActiveScene().name == "Main_Menu_Scene" && musicDisplayer.clip != music[1])
        {
            Debug.Log("Imdisplaying");
            PlayMenuTheme();
        }
        
        if (SceneManager.GetActiveScene().name == "Level_Tuto_Scene" && musicDisplayer.clip != music[0] || SceneManager.GetActiveScene().name == "Level_1_Scene" && musicDisplayer.clip != music[0]
            ||SceneManager.GetActiveScene().name == "Level_2_scene" && musicDisplayer.clip != music[0])
        {
            Debug.Log("Imdisplaying");
            PlayMainTheme();
        }

        if (SceneManager.GetActiveScene().name == "Main_Boss_Scene" && musicDisplayer.clip != music[2])
        {
            Debug.Log("Imdisplaying");
            PlayBossTheme();
        }
            
    }
}
