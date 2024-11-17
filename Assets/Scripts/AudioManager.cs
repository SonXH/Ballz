using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Audio Source")]
    [SerializeField]
    private AudioSource musicSource;
    [SerializeField]
    private AudioSource SFXSource;

    [Header("Audio Clips")]
    public AudioClip background;
    public AudioClip pause;

    // Start is called before the first frame update
    void Start()
    {
        musicSource.clip=background;
        musicSource.loop = true;
        musicSource.Play();
    }

    public void PlayPauseMusic()
    {
        // Set ignoreListenerPause to true and play pause music
        musicSource.ignoreListenerPause = true;
        musicSource.clip = pause;
        musicSource.Play();
    }

    public void ResumeBackgroundMusic()
    {
        // Reset ignoreListenerPause and resume background music
        musicSource.ignoreListenerPause = false;
        musicSource.clip = background;
        musicSource.Play();
    }

    public void playSFX(AudioClip sound)
    {
        SFXSource.PlayOneShot(sound);
    }
}
