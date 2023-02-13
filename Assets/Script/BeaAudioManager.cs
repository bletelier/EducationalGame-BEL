using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeaAudioManager : MonoBehaviour
{
    public static AudioSource audioSrc;
    private void Awake()
    {
        audioSrc = GetComponent<AudioSource>();
        audioSrc.Stop();
    }
    private void Start()
    {
        audioSrc.loop = true;
        audioSrc.Play();
    }

    
}
