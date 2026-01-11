using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int quality;
    public float currentTime;
    public bool timerActive = true;
    public int isFinishingAllLevel;
    public bool scoreSet;
    private AudioSource music;
    private bool musicReset;
    void Awake()
    {
        Cursor.visible = false;
        DontDestroyOnLoad(this.gameObject);
        quality = 0;
        timerActive = false;
        music = GetComponent<AudioSource>();
        
    }
    
    void Start()
    {
        music.volume = 1f;
        PlayerPrefs.SetFloat("MasterVolume", 1);
        PlayerPrefs.SetFloat("MusicVolume", 1);
        PlayerPrefs.SetFloat("PlayerVolume", 1);
        PlayerPrefs.SetFloat("AmbientVolume", 1);
        PlayerPrefs.SetInt("SpikesOn", 1);
        
        currentTime = 0;
        isFinishingAllLevel = 0;
        scoreSet = true;
    }

    void Update()
    {

        Debug.Log( PlayerPrefs.GetFloat("MasterVolume") + "master");
        Debug.Log( PlayerPrefs.GetFloat("MusicVolume") + "music");
        Debug.Log( PlayerPrefs.GetFloat("PlayerVolume") + "sound" );
        Debug.Log( PlayerPrefs.GetFloat("AmbientVolume") + "ambience");
        Debug.Log( PlayerPrefs.GetInt("SpikesOn") + "Spikes");
        Debug.Log( PlayerPrefs.GetInt("resWidth") + "width");
        Debug.Log( PlayerPrefs.GetInt("resHeight") + "height");
        Debug.Log(PlayerPrefs.GetInt("Quality") + "quality");
        Debug.Log(PlayerPrefs.GetInt("isFullscreen") + "fullscreen");
        
        if (SceneManager.GetActiveScene().name == "Main_Menu_Scene")
        {
            timerActive = false;
            currentTime = 0;
        }
        else timerActive = true;
        
        if (timerActive)
        {
            currentTime += Time.deltaTime;
        }
    }


}
