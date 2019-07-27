using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySFX : MonoBehaviour {

    protected AudioSource audioSource;
    public AudioClip audioClip;

    [SerializeField]
    private AudioSource overrideAudioSource;


    private void Start()
    {
        if(overrideAudioSource == null)
        {
            audioSource = GameObject.FindWithTag("SFXSource").GetComponent<AudioSource>();
        }
      else
      {
      audioSource = overrideAudioSource;
      }
    }

    public void playClip()
    {
        audioSource.clip = audioClip;
        audioSource.Play();
    }
}
