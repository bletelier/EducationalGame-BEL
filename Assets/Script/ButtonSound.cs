using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSound : MonoBehaviour
{
    public AudioClip press;
    public AudioClip changed;
    
    public void ButtonPressed()
    {
        SoundManagerTutorial.PlaySound(press);
    }
    public void ValueChanged()
    {
        SoundManagerTutorial.PlaySound(changed);
    }

}
