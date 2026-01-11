using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MixerController : MonoBehaviour
{
    [SerializeField] private AudioMixer myAudioMixer;
    [SerializeField] private Slider sliderMaster;
    [SerializeField] private Slider sliderSound;
    [SerializeField] private Slider sliderMusic;
    [SerializeField] private Slider sliderAmbient;

    private void Start()
    {
        sliderMaster.value = PlayerPrefs.GetFloat("MasterVolume");
        sliderMusic.value = PlayerPrefs.GetFloat("MusicVolume");
        sliderSound.value = PlayerPrefs.GetFloat("PlayerVolume");
        sliderAmbient.value = PlayerPrefs.GetFloat("AmbientVolume");
    }

    public void SetMasterVolume(float sliderValue)
    {
        myAudioMixer.SetFloat("MasterVolume", Mathf.Log10(sliderValue) * 20);
        PlayerPrefs.SetFloat("MasterVolume",sliderValue);
    }

    public void SetPlayerVolume(float sliderValue)
    {
        myAudioMixer.SetFloat("PlayerVolume", Mathf.Log10(sliderValue) * 20);
        PlayerPrefs.SetFloat("PlayerVolume",sliderValue);
    }
    
    public void SetMusicVolume(float sliderValue)
    {
        myAudioMixer.SetFloat("MusicVolume", Mathf.Log10(sliderValue) * 20);
        PlayerPrefs.SetFloat("MusicVolume",sliderValue);
    }
    
    public void SetAmbiantVolume(float sliderValue)
    {
        myAudioMixer.SetFloat("AmbiantVolume", Mathf.Log10(sliderValue) * 20);
        PlayerPrefs.SetFloat("AmbientVolume",sliderValue);
    }
    
}
