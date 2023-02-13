using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContinueButton : MonoBehaviour
{
    public bool adelanta = true;

    public void SiguienteOApurar()
    {
        if(!DialogueManager.instance.puedoPasar)
        {
            adelanta = true;
        }
        else
        {
            adelanta = false;
        }
    }
}
