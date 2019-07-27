using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySFX : MonoBehaviour {

    protected AudioSource audioSource;
    public AudioClip audioClip;

    private void Start()
    {
        if(audioSource == null)
        {
            audioSource = GameObject.FindWithTag("SFXSource").GetComponent<AudioSource>();
        }
    }

    public void playClip()
    {
        audioSource.clip = audioClip;
        audioSource.Play();
    }
}
