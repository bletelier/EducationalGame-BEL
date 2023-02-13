using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridAccionesManager : MonoBehaviour
{
    public AudioClip xplosion;
    public AudioClip change;
    public static GridAccionesManager instance { get; private set; }
    public Sprite[] imagenesBotones;
    private List<GameObject> listaImagenesEnPantalla;
    private List<int> posAcciones;
    public List<Sprite> spritesEnPantalla;

    public int accionesEliminadas = 0;
    public int cambiosAcciones = 0;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        posAcciones = new List<int>();
        posAcciones.Add(-1);
        posAcciones.Add(-1);
    }
    private void Start()
    {
        listaImagenesEnPantalla = new List<GameObject>();
        spritesEnPantalla = new List<Sprite>();
    }

    public void AgregarImagen(int imagen, int pos)
    {
        this.transform.GetChild(pos).GetComponent<Image>().sprite = imagenesBotones[imagen];
        this.transform.GetChild(pos).GetComponent<Image>().color = new Color(255, 255, 255, 255);
        GameObject aux = this.transform.GetChild(pos).gameObject;
        listaImagenesEnPantalla.Add(aux);
        spritesEnPantalla.Add(imagenesBotones[imagen]);
    }
    public void EliminarAccionActual()
    {
        if(listaImagenesEnPantalla[0].GetComponent<Image>().enabled)
        {
            listaImagenesEnPantalla[0].GetComponent<Image>().enabled = false;
            listaImagenesEnPantalla.RemoveAt(0);
            spritesEnPantalla.RemoveAt(0);
        }
        else
        {
            listaImagenesEnPantalla.RemoveAt(0);
            EliminarAccionActual();
        }
        
    }

    public void cambiarAcciones(int pos)
    {
        LlenarListaDeAccionesClickeadas(pos);
        EliminarOAlternarAcciones(pos);
        if (posAcciones[1] > -1)
        {
            posAcciones[0] = -1;
            posAcciones[1] = -1;
        }
    }

    private void EliminarOAlternarAcciones(int pos)
    {

        if (posAcciones[0] == posAcciones[1])//eliminar accion
        {
            accionesEliminadas++;
            if (pos >= 20)//Si es la ultima opcion
            {
                EliminarAccionCompletamente(pos);
            }
            else if (pos < 20)//Si es cualquier otra
            {
                if (!this.transform.GetChild(pos + 1).GetComponent<Button>().enabled)//Si no tengo accion siguiente
                {
                    EliminarAccionCompletamente(pos);
                }
                else
                {
                    int primerValorPostEliminado = pos + 1;
                    EliminarAccionCompletamente(pos);                    
                    int cantHijosRestantes = botonMovementManager.instance.pos;
                    for (int i = primerValorPostEliminado; i <=cantHijosRestantes; ++i)
                    {
                        this.transform.GetChild(i-1).GetComponent<Image>().sprite = spritesEnPantalla[i-1];
                        this.transform.GetChild(i-1).GetComponent<Button>().enabled = true;
                        this.transform.GetChild(i-1).GetComponent<Image>().color = new Color(255, 255, 255, 255);
                    }
                    this.transform.GetChild(cantHijosRestantes).GetComponent<Image>().sprite = null;
                    this.transform.GetChild(cantHijosRestantes).GetComponent<Button>().enabled = false;
                    this.transform.GetChild(cantHijosRestantes ).GetComponent<Image>().color = new Color(255, 255, 255, 0);
                }
            }
            SoundManagerTutorial.PlaySound(xplosion);
        }
        else if (posAcciones[0] > -1 && posAcciones[1] > -1)//intercambiar acciones
        {
            cambiosAcciones++;
            this.transform.GetChild(posAcciones[0]).GetComponent<Image>().sprite = spritesEnPantalla[posAcciones[1]];
            this.transform.GetChild(posAcciones[1]).GetComponent<Image>().sprite = spritesEnPantalla[posAcciones[0]];
            Sprite aux = spritesEnPantalla[posAcciones[0]];
            spritesEnPantalla[posAcciones[0]] = spritesEnPantalla[posAcciones[1]];
            spritesEnPantalla[posAcciones[1]] = aux;
            botonMovementManager.instance.CambiarAccionesEnLista(posAcciones[0], posAcciones[1]);
            SoundManagerTutorial.PlaySound(change);
        }
        
    }

    private void LlenarListaDeAccionesClickeadas(int pos)
    {
        if (posAcciones[0] <= -1)
        {
            posAcciones[0] = pos;
        }
        else if (posAcciones[1] <= -1)
        {
            posAcciones[1] = pos;
        }
    }

    public void EliminarAccionCompletamente(int pos)
    {
        this.transform.GetChild(pos).GetComponent<Button>().enabled = false;
        this.transform.GetChild(pos).GetComponent<Image>().sprite = null;
        this.transform.GetChild(pos).GetComponent<Image>().color = new Color(255, 255, 255, 0);
        spritesEnPantalla.RemoveAt(pos);
        botonMovementManager.instance.EliminarDeListaAcciones(pos);
        botonMovementManager.instance.pos--;
    }

    public void ActivateButton(int pos)
    {
        this.transform.GetChild(pos).GetComponent<Button>().enabled = true;
    }
}