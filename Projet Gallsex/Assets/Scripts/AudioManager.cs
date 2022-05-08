using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;

    [Header("AudioClips")]
    [SerializeField] private AudioClip run;
    [SerializeField] private AudioClip jump;
    [SerializeField] private AudioClip landing;
    [SerializeField] private AudioClip wallSlide;
    [SerializeField] private AudioClip sit;
    [SerializeField] private AudioClip crouch;
    [SerializeField] private AudioClip dash;

    public List<AudioClip> soundList = new List<AudioClip>();

    public void Awake()
    {
        soundList.AddRange(new AudioClip[]{run, jump, landing, wallSlide, sit, crouch, dash});
    }
    public void StartSound(int _clip)
    {
        audioSource.PlayOneShot(soundList[_clip]);
    }
}
