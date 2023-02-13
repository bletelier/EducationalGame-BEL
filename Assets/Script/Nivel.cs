using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Nivel
{
    public int dificultad;
    public int frustracion;
    public int puntuacion;
    public int tiempoTotal;
    public int intentosTotales;
    public int numeroRestart;
    public int intentosFallidos;
    public List<string> secuenciaInstucciones;
    public List<int> tiempoPensando;
    public List<int> cambiosAcciones;
    public List<int> accionesEliminadas;
    public List<float> porcentajeAvance;
    public List<int> mApretadas;
    public List<int> tabsApretados;
    public List<string> tipoIntento;

    public Nivel()
    {
        dificultad = 0;
        frustracion = 0;
        puntuacion = 0;
        tiempoTotal = 0;
        intentosFallidos = 0;
        numeroRestart = 0;
        intentosTotales = 0;
        tipoIntento = new List<string>();
        secuenciaInstucciones = new List<string>();
        tiempoPensando = new List<int>();
        cambiosAcciones = new List<int>();
        accionesEliminadas = new List<int>();
        porcentajeAvance = new List<float>();
        mApretadas = new List<int>();
        tabsApretados = new List<int>();
    }
}
