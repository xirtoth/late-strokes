using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Sound
{
    HitBump,
    Hit,
    GameOver,
    Win,
    Blob
}

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    public AudioSource audioSource;


    public AudioClip hitBump;

    public AudioClip blob;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void PlayAudio(Sound sound)
    {
        switch (sound)
        {
            case Sound.HitBump:
                audioSource.PlayOneShot(hitBump);
                break;

            case Sound.Hit:
                audioSource.Play();
                break;

            case Sound.GameOver:
                audioSource.Play();
                break;

            case Sound.Win:
                audioSource.Play();
                break;
            case Sound.Blob:
                audioSource.PlayOneShot(blob);
                break;
        }
    }
}