using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    #region AudioSources

    [SerializeField] private AudioSource playerAudioSource;

    [Header("AudioClips")] 
    
    [SerializeField] private AudioClip run;
    [SerializeField] private AudioClip jump;
    [SerializeField] private AudioClip landing;
    [SerializeField] private AudioClip wallSlide;
    [SerializeField] private AudioClip sit;
    [SerializeField] private AudioClip crouch;
    [SerializeField] private AudioClip dash;

    public List<AudioClip> soundList = new List<AudioClip>();
    
    #endregion
    
    #region Volumes

    [Header("Volumes")] 
    
    [Range(0f, 1f)]
    [SerializeField] private float runVolume;
    [Range(0f, 1f)]
    [SerializeField] private float jumpVolume;
    [Range(0f, 1f)]
    [SerializeField] private float landingVolume;
    [Range(0f, 1f)]
    [SerializeField] private float wallSlideVolume;
    [Range(0f, 1f)]
    [SerializeField] private float sitVolume;
    [Range(0f, 1f)]
    [SerializeField] private float crouchVolume;
    [Range(0f, 1f)]
    [SerializeField] private float dashVolume;
    
    public List<float> volumeList = new List<float>();

    #endregion

    #region PlayerSounds

    public void Awake()
    {
        soundList.AddRange(new AudioClip[] {run, jump, landing, wallSlide, sit, crouch, dash});
        volumeList.AddRange(new float[] {runVolume, jumpVolume, landingVolume, wallSlideVolume, sitVolume, crouchVolume, dashVolume});
    }

    public void StartSound(int _clip)
    {
        playerAudioSource.PlayOneShot(soundList[_clip], volumeList[_clip]);
    }

    #endregion
}
