using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadoIzquierdoController : MonoBehaviour
{
    public static LadoIzquierdoController instance { get; private set; }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            botonMovement.instance.canMoveToThereIzq = false;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            botonMovement.instance.canMoveToThereIzq = true;
        }
    }
}
