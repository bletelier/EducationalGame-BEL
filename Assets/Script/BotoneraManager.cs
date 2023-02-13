using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BotoneraManager : MonoBehaviour
{
    public static BotoneraManager instance { get; private set; }
    private CanvasGroup canvasG;
    private bool abierto, esJugable;

    private void Awake()
    {
        if(instance==null)
        {
            instance = this;
        }
        esJugable = true;
        canvasG = GetComponent<CanvasGroup>();
    }

    public void AbrirCerrarBotonera()
    {
        if (abierto)
        {
            canvasG.alpha = 0;
            canvasG.interactable = false;
            canvasG.blocksRaycasts = false;
            abierto = false;
        }
        else
        {
            canvasG.alpha = 1;
            if (esJugable)
            {
                canvasG.interactable = true;
            }
            canvasG.blocksRaycasts = true;
            abierto = true;
        }
    }
    public bool GetEsJugable()
    {
        return esJugable;
    }
    public void SetEsJugable(bool jugable)
    {
        esJugable = jugable;
        SetInteractuable();
    }
    public void SetInteractuable()
    {
        if (esJugable)
        {
            canvasG.interactable = true;
        }
        else
        {
            canvasG.interactable = false;
        }
    }

}
