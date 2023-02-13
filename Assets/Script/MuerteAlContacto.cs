using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuerteAlContacto : MonoBehaviour
{
    public AudioClip death;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            movimiento.instance.mori = true;
            botonMovement.instance.perdi = true;
            BeaAudioManager.audioSrc.Stop();
            SoundManagerTutorial.PlaySound(death);
        }
    }

}
