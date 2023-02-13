using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class botonMovementManager : MonoBehaviour
{
    public static botonMovementManager instance { get; private set; }
    public Text tiempo_txt;
    public Text intentos_txt;
    private int accion;
    private List<int> acciones;
    private bool jugable;
    private bool iWin;
    public int pos = 0;
    public bool salto,movioIzq,movioDer,saltoSiguiente = true;
    public bool isTutorial;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    private void Start()
    {
        acciones = new List<int>();
        if(!isTutorial)
        {
            tiempo_txt = tiempo_txt.gameObject.GetComponent<Text>();
            intentos_txt = intentos_txt.gameObject.GetComponent<Text>();
        }
        
    }


    public void setTiempoTxt(int tiempo)
    {
        if (!isTutorial)
        {
            tiempo_txt.text = "Tiempo: " + tiempo.ToString() + " s.";
        }
    }

    public void setIntentosTxt(int intentos)
    {
        if (!isTutorial)
        {
            intentos_txt.text = "Intentos: " + intentos.ToString();
        }
    }


    public int MirarAccionActual()
    {
        if(acciones.Count > 0)
        {
            accion = acciones[0];
            acciones.RemoveAt(0);
            return accion;
        }
        return 0;
                                         // ACCIONES 1-9
                                         // 1:= IZQ   2:=SALTAR  3:=DER
    }                                    // 4:= ROJO  5:=VERDE   6:= AZUL
                                         // 7:= NARANJO 8:=ROSA  9:=COLORX

    public void AgregarAccion(int accionParaAgregar)
    {
        if(!jugable && acciones.Count < 21)
        {
            acciones.Add(accionParaAgregar);
            GridAccionesManager.instance.ActivateButton(pos);
            GridAccionesManager.instance.AgregarImagen(accionParaAgregar - 1,pos++);
        }
    }

    public void setJugable(bool esJugable)
    {
        jugable = esJugable;
    }

    public void SetWin(bool gane)
    {
        iWin = gane;
    }
    public bool GetWin()
    {
        return iWin;
    }

    public void CambiarAccionesEnLista(int pos1, int pos2)
    {
        int aux = acciones[pos1];
        acciones[pos1] = acciones[pos2];
        acciones[pos2] = aux;
    }
    public void EliminarDeListaAcciones(int pos)
    {
        acciones.RemoveAt(pos);
    }
}
