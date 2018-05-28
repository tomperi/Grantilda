using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    private static SoundManager instance = null;
    public AudioSource zoomInBackgroundAudioSource;
    public AudioSource zoomOutBackgroundAudioSource;
    public AudioSource soundEffectAudioSource;
    public AudioClip zoomInBackgroundMusic;
    public AudioClip zoomOutBackgroundMusic;
    public AudioClip zoomInSoundEffect;
    public AudioClip zoomOutSoundEffect;
    public AudioClip rotateSoundEffect;
    private float zoomActionSoundEffectLength;
    private float fadeThreshold = 0.95f;
    public static SoundManager Instance
    {
        get
        {
            return instance;
        }
    }

    private void Start()
    {
        zoomInBackgroundAudioSource.clip = zoomInBackgroundMusic;
        zoomOutBackgroundAudioSource.clip = zoomOutBackgroundMusic;
        zoomActionSoundEffectLength = (zoomInSoundEffect.length + zoomOutSoundEffect.length) / 2;
        initPlayBackgroundMusic();
    }

    private void Awake()
    {
        if (instance != null && instance != this) {
            Destroy(this.gameObject);
            return;
        } else {
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }

    private void initPlayBackgroundMusic()
    {
        zoomOutBackgroundAudioSource.volume = 0;
        zoomInBackgroundAudioSource.volume = 1;
        zoomInBackgroundAudioSource.Play();
        zoomOutBackgroundAudioSource.Play();
    }

    private void playZoomInAction()
    {
        playAction(zoomInBackgroundAudioSource, zoomOutBackgroundAudioSource, zoomInSoundEffect);
    }

    private void playZoomOutAction()
    {
        playAction(zoomOutBackgroundAudioSource, zoomInBackgroundAudioSource, zoomOutSoundEffect);
    }

    private void playAction(AudioSource fadeInSource, AudioSource fadeOutSource, AudioClip zoomSoundClip) 
    {
        fadeOutSource.volume = 1;
        fadeInSource.volume = 0;
        soundEffectAudioSource.PlayOneShot(zoomSoundClip);
        StartCoroutine(FadeIn(fadeInSource));
        StartCoroutine(FadeOut(fadeOutSource));
    }

    public void ToggleZoomSoundAction(bool zoomIn)
    {
        if(zoomIn)
        {
            playZoomInAction();
        }
        else
        {
            playZoomOutAction();
        }
    }

    IEnumerator FadeOut(AudioSource audioSource)
    {
        while (audioSource.volume > 1 - fadeThreshold)
        {
            audioSource.volume -= Time.deltaTime / zoomActionSoundEffectLength;

            yield return null;
        }

        audioSource.volume = 0;
    }

    IEnumerator FadeIn(AudioSource audioSource)
    {
        while (audioSource.volume < fadeThreshold)
        {
            audioSource.volume += Time.deltaTime / zoomActionSoundEffectLength;

            yield return null;
        }

        audioSource.volume = 1;
    }
    
    public void PlayRotateFrameSoundEffect()
    {
        if (!soundEffectAudioSource.isPlaying)
        {
            soundEffectAudioSource.PlayOneShot(rotateSoundEffect);
        }
    }

}
