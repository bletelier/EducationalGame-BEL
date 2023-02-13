using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadoDerechoController : MonoBehaviour
{
    public static LadoDerechoController instance { get; private set; }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            botonMovement.instance.canMoveToThereDer = false;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            botonMovement.instance.canMoveToThereDer = true;
        }
    }
}