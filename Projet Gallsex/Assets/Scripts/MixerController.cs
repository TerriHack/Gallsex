using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MixerController : MonoBehaviour
{
    [SerializeField] private AudioMixer myAudioMixer;

    public void SetMasterVolume(float sliderValue)
    {
        myAudioMixer.SetFloat("MasterVolume", Mathf.Log10(sliderValue) * 20);
    }

    public void SetPlayerVolume(float sliderValue)
    {
        myAudioMixer.SetFloat("PlayerVolume", Mathf.Log10(sliderValue) * 20);
    }
    
    public void SetMusicVolume(float sliderValue)
    {
        myAudioMixer.SetFloat("MusicVolume", Mathf.Log10(sliderValue) * 20);
    }
    
    public void SetAmbiantVolume(float sliderValue)
    {
        myAudioMixer.SetFloat("AmbiantVolume", Mathf.Log10(sliderValue) * 20);
    }
}
