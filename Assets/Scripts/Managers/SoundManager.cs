using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class SoundManager : MonoBehaviour
{
    [Header("Audio Clips")]
    [SerializeField] private AudioClip targetSuccessAudio;
    [SerializeField] private AudioClip targetFailureAudio;

    AudioSource audioSource;
    private void Start()    
    {
        audioSource = GetComponent<AudioSource>();
        GameBuilder.Instance.onSuccess += GameBuilder_OnSuccess;
        GameBuilder.Instance.onFailure += GameBuilder_OnFailure;

    }

    private void GameBuilder_OnSuccess(Target target)
    {
        PlayAudio(targetSuccessAudio);
    }

    private void GameBuilder_OnFailure(Target target)
    {
        PlayAudio(targetFailureAudio);
    }

    private void PlayAudio(AudioClip audioClip)
    { 
        audioSource.PlayOneShot(audioClip);
    }
}
