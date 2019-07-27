using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayBGMusic : MonoBehaviour {

    protected AudioSource audioSource;
    public AudioClip audioClip;

    private void Start()
    {
        if(audioSource == null)
        {
            audioSource = GameObject.FindWithTag("BGMusicSource").GetComponent<AudioSource>();
        }
    }

    public void playClip()
    {
        audioSource.clip = audioClip;
        audioSource.Play();
    }
}
