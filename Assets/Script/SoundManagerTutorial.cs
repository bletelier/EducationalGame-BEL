using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerTutorial : MonoBehaviour
{
    static AudioSource aSrc;
    // Start is called before the first frame update
    void Awake()
    {
        aSrc = GetComponent<AudioSource>();
    }

    public static void PlaySound(AudioClip sound)
    {
        aSrc.PlayOneShot(sound);
    }
}
