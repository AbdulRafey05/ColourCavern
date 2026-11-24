using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // Audio Sources
    private AudioSource backgroundMusicSource;
    private AudioSource soundEffectsSource;

    // Audio Clips
    public AudioClip backgroundMusic;
    public AudioClip diamondCollectSound;
    public AudioClip swordSound;
    public AudioClip hurtSound;    // New sound
    public AudioClip jumpSound;    // New sound
    public AudioClip winSound;     // New sound
    public AudioClip loseSound;    // New sound

    void Start()
    {
        // Create Audio Sources
        backgroundMusicSource = gameObject.AddComponent<AudioSource>();
        soundEffectsSource = gameObject.AddComponent<AudioSource>();

        // Set up background music
        backgroundMusicSource.clip = backgroundMusic;
        backgroundMusicSource.loop = true;
        backgroundMusicSource.volume = 0.5f; // Adjust volume as needed
        backgroundMusicSource.Play();
    }

    public void PlayDiamondCollectSound()
    {
        soundEffectsSource.PlayOneShot(diamondCollectSound);
    }

    public void PlaySwordSound()
    {
        soundEffectsSource.PlayOneShot(swordSound);
    }

    public void PlayHurtSound()
    {
        soundEffectsSource.PlayOneShot(hurtSound);
    }

    public void PlayJumpSound()
    {
        soundEffectsSource.PlayOneShot(jumpSound);
    }

    public void PlayWinSound()
    {
        soundEffectsSource.PlayOneShot(winSound);
    }

    public void PlayLoseSound()
    {
        soundEffectsSource.PlayOneShot(loseSound);
    }
}