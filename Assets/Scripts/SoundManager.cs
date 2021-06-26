using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AudioType
{
    Correct,
    Wrong,
    Success
}

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
    private static SoundManager _instance;
    public static SoundManager Instance => _instance;

    private AudioSource audioSource;

    private Dictionary<AudioType, AudioClip> audioClips = new Dictionary<AudioType, AudioClip>();

    private AudioClip audio_correct;
    private AudioClip audio_wrong;
    private AudioClip audio_success;

    void Awake()
    {
        #region Singleton

        if (_instance != null && _instance != this)
            Destroy(gameObject);
        else
            _instance = this;

        #endregion
    }

    void Start()
    {
        Initialize();
    }

    public void Initialize()
    {
        audioSource = GetComponent<AudioSource>();

        audio_correct = Resources.Load<AudioClip>("Sounds/audio_correct");
        audio_wrong = Resources.Load<AudioClip>("Sounds/audio_wrong");
        audio_success = Resources.Load<AudioClip>("Sounds/audio_success");

        audioClips.Add(AudioType.Correct, audio_correct);
        audioClips.Add(AudioType.Wrong, audio_wrong);
        audioClips.Add(AudioType.Success, audio_success);
    }

    public void PlayAudio(AudioType _audioType)
    {
        audioSource.PlayOneShot(audioClips[_audioType]);
    }
}
