using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private PlayerBetterController pbc;

    [Header("AudioClips")]
    [SerializeField] private AudioClip run;
    [SerializeField] private AudioClip jump;
    [SerializeField] private AudioClip landing;
    [SerializeField] private AudioClip wallSlide;
    [SerializeField] private AudioClip sit;
    [SerializeField] private AudioClip crouch;
    [SerializeField] private AudioClip dash;

    void Update()
    {
        if (rb.velocity.x != 0 && pbc.isGrounded && audioSource.clip != run) // Lance le son de course
        {
            LoopSound(run);
        }

        if (rb.velocity.y != 0 && (pbc.wallSliding) && audioSource.clip != wallSlide) // Lance le son de wallslide
        {
            LoopSound(wallSlide);
        }

        if ((rb.velocity.x == 0 && rb.velocity.y == 0) || (!pbc.isGrounded && !pbc.wallSliding)) // Coupe les sons de run et de wallslide
        {
            StopLoopSound();
        }

        if (pbc.isJumping)
        {
            StartSound(jump);
        }
    }

    void LoopSound(AudioClip _clip)
    {
        audioSource.clip = _clip;
        audioSource.loop = true;
        audioSource.Play();
    } // Lance les sons qui se loop (run et wallslide)

    void StopLoopSound()
    {
        audioSource.clip = null;
        audioSource.loop = false;
    } // Stop les sons qui se loop (run et wallslide)

    void StartSound(AudioClip _clip)
    {
        audioSource.PlayOneShot(_clip);
    }
}
