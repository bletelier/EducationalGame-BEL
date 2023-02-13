using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class botonMovement : MonoBehaviour
{
    [Header("Colores y Sprites a cambiar")]
    public Color[] colores;
    public Light luz;
    public SpriteRenderer aura;
    public SpriteRenderer ojos;

    public bool canMoveToThereIzq = true;
    public bool canMoveToThereDer = true;

    public int valorNivel;

    public string level;
    public string levelVictoria;


    public static botonMovement instance { get; private set; }

    private static int intentos = 0;
    private static int tiempo = 0;
    private int accionActual;
    public bool puedoCambiar;
    public bool perdi;
    private bool pressedPlay;
    private bool aux;
    private static bool restarted;
    public Color colorActual;
    public float time = 0.0f;
    private int frameCounts;
    private bool ganePorPrimeraVez = true;

    #region DatabaseData
    public static int intentosFallidos;
    public static int numeroRestart;
    private int tiempoPensando = 0;
    private string spritesAEjecutar = "";
    private int tiempoPensandoAux = 0;
    private string tipoIntento = "";
    #endregion
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
        void Start()
    {
        luz = luz.gameObject.GetComponent<Light>();
        aura = aura.gameObject.GetComponent<SpriteRenderer>();
        ojos = ojos.gameObject.GetComponent<SpriteRenderer>();
        StartCoroutine(ContarTiempo());
        botonMovementManager.instance.setIntentosTxt(intentos);
    }
    private void Update()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!perdi && botonMovementManager.instance.GetWin() && ganePorPrimeraVez)
        {
            ganePorPrimeraVez = false;
            DatabasePosting();
            StartCoroutine(EsperarVictoria(7.0f));
            
        }
        else if (!perdi && ganePorPrimeraVez && puedoCambiar && pressedPlay && botonMovementManager.instance.saltoSiguiente && movimiento.instance.canJump)
        {
            puedoCambiar = false;
            accionActual = botonMovementManager.instance.MirarAccionActual();
            switch (accionActual)
            {
                case 1://IZQUIERDA
                    botonMovementManager.instance.movioIzq = true;
                    break;
                case 2: //SALTO
                    botonMovementManager.instance.salto = true;
                    botonMovementManager.instance.saltoSiguiente = false;
                    break;
                case 3: //DERECHA
                    botonMovementManager.instance.movioDer = true;                    
                    break;
                case 4: //ROJO
                    StartCoroutine(CambiarColor(0));
                    break;
                case 5://VERDE
                    StartCoroutine(CambiarColor(1));
                    break;
                case 6://AZUL
                    StartCoroutine(CambiarColor(2));
                    break;
                case 7://NARANJO
                    StartCoroutine(CambiarColor(3));
                    break;
                case 8://ROSADO
                    StartCoroutine(CambiarColor(4));
                    break;
                case 9://COLORX
                    StartCoroutine(CambiarColor(5));
                    break;
                case 0://No hay Acciones
                    StartCoroutine(Esperar(0.8f));
                    
                    break;   
            }
        }
        
    }
    IEnumerator Mover(float time)
    {
        yield return new WaitForSeconds(time);
        GridAccionesManager.instance.EliminarAccionActual();
        botonMovementManager.instance.movioDer = false;
        botonMovementManager.instance.movioIzq = false;
        puedoCambiar = true;
    }

    public IEnumerator Esperar(float time)
    {
        if (!perdi)
        {
            perdi = true;
            yield return new WaitForSeconds(time);
            Restart();
        }
        else
        {
            yield return new WaitForSeconds(time);
            Restart();
        }
        

    }
    private int CalcularPuntuacion(int t, int i)
    {
        if(t*i <= 6000)
        {
            return (int)(7010 - 1.15f * (t * i));
        }
        else
        {
            return 100;
        }
        
    }

    private void DatabasePosting()
    {
        if (valorNivel > -1)
        {
            
            DatabaseManager.user.niveles[valorNivel].intentosFallidos = intentosFallidos;
            DatabaseManager.user.niveles[valorNivel].intentosTotales = intentos;
            DatabaseManager.user.niveles[valorNivel].tiempoTotal = tiempo;
            DatabaseManager.user.niveles[valorNivel].puntuacion = Mathf.Clamp(CalcularPuntuacion(tiempo, intentos), 100, 7000);
            DatabaseManager.user.puntajeAcumulado += DatabaseManager.user.niveles[valorNivel].puntuacion;
            DatabaseManager.user.niveles[valorNivel].numeroRestart = numeroRestart;
            LevelData.puntuacion = DatabaseManager.user.niveles[valorNivel].puntuacion;
            DatabaseManager.Instance.PostLevelScoresToDatabase();
        }
    }

    private IEnumerator EsperarVictoria(float time)
    {

        yield return new WaitForSeconds(time);
        intentos = 0;
        tiempo = 0;
        numeroRestart = 0;
        intentosFallidos = 0;
        restarted = false;
        //MOSTAR PANTALLA VICTORIA


        SceneManager.LoadScene(levelVictoria);

    }

    IEnumerator CambiarColor(int color)
    {
        yield return new WaitForSeconds(0.1f);
        colorActual = colores[color];
        luz.color = colores[color];
        aura.color = colores[color];
        ojos.color = colores[color];        
        GridAccionesManager.instance.EliminarAccionActual();
        yield return new WaitForSeconds(0.05f);
        puedoCambiar = true;
    }
    IEnumerator ContarTiempo()
    {
        if(!restarted)
        {
            botonMovementManager.instance.setTiempoTxt(tiempo);
            yield return new WaitForSeconds(1.5f);
        }
        while(true)
        {
            tiempo += 1;
            tiempoPensandoAux += 1;
            botonMovementManager.instance.setTiempoTxt(tiempo);
            yield return new WaitForSeconds(1.0f);
        }
    }

    public void PlayPressed()
    {
        if(InstruccioneraManager.instance.GetEsJugable())
        {
            spritesAEjecutar = "";
            bool primeraVez = true;
            for (int i = 0; i < GridAccionesManager.instance.spritesEnPantalla.Count; i++)
            {
                if(primeraVez)
                {
                    primeraVez = false;
                    spritesAEjecutar = spritesAEjecutar + GridAccionesManager.instance.spritesEnPantalla[i].name;
                }
                else
                {
                    spritesAEjecutar = spritesAEjecutar + "," + GridAccionesManager.instance.spritesEnPantalla[i].name;
                }
                
            }
            botonMovementManager.instance.setIntentosTxt(++intentos);
            tiempoPensando = tiempoPensandoAux;
            if (valorNivel > -1)
            {
                if (intentos == 1)
                {   
                    DatabaseManager.user.niveles[valorNivel].tipoIntento = new List<string>();
                    DatabaseManager.user.niveles[valorNivel].porcentajeAvance = new List<float>();
                    DatabaseManager.user.niveles[valorNivel].secuenciaInstucciones = new List<string>();
                    DatabaseManager.user.niveles[valorNivel].accionesEliminadas = new List<int>();
                    DatabaseManager.user.niveles[valorNivel].cambiosAcciones = new List<int>();
                    DatabaseManager.user.niveles[valorNivel].tiempoPensando = new List<int>();
                    DatabaseManager.user.niveles[valorNivel].mApretadas = new List<int>();
                    DatabaseManager.user.niveles[valorNivel].tabsApretados = new List<int>();
                }
                tipoIntento = "Initiated(+1),";
                DatabaseManager.user.niveles[valorNivel].secuenciaInstucciones.Add(spritesAEjecutar);
                DatabaseManager.user.niveles[valorNivel].accionesEliminadas.Add(GridAccionesManager.instance.accionesEliminadas);
                DatabaseManager.user.niveles[valorNivel].cambiosAcciones.Add(GridAccionesManager.instance.cambiosAcciones);
                DatabaseManager.user.niveles[valorNivel].tiempoPensando.Add(tiempoPensando);
                DatabaseManager.user.niveles[valorNivel].mApretadas.Add(movimiento.instance.mApretadas);
                DatabaseManager.user.niveles[valorNivel].tabsApretados.Add(movimiento.instance.tabsApretados);
            }
            
            puedoCambiar = true;
            pressedPlay = true;
            botonMovementManager.instance.setJugable(true);
            InstruccioneraManager.instance.SetEsJugable(false);
            BotoneraManager.instance.SetEsJugable(false);
        }
        
    }

    public void RestartObligado()
    {
        numeroRestart++;
        botonMovementManager.instance.setIntentosTxt(++intentos);
        if (!pressedPlay)
        {
            if (valorNivel > -1)
            {
                if (intentos == 1)
                {
                    DatabaseManager.user.niveles[valorNivel].tipoIntento = new List<string>();
                    DatabaseManager.user.niveles[valorNivel].porcentajeAvance = new List<float>();
                    DatabaseManager.user.niveles[valorNivel].secuenciaInstucciones = new List<string>();
                    DatabaseManager.user.niveles[valorNivel].accionesEliminadas = new List<int>();
                    DatabaseManager.user.niveles[valorNivel].cambiosAcciones = new List<int>();
                    DatabaseManager.user.niveles[valorNivel].tiempoPensando = new List<int>();
                    DatabaseManager.user.niveles[valorNivel].mApretadas = new List<int>();
                    DatabaseManager.user.niveles[valorNivel].tabsApretados = new List<int>();
                }
                DatabaseManager.user.niveles[valorNivel].tipoIntento.Add(tipoIntento + "Restarted(+1)");
                DatabaseManager.user.niveles[valorNivel].porcentajeAvance.Add((float)(Math.Round(PorcentajeManager.Instance.porcentajeAvance * 10000.0f) / 10000.0f) * 100.0f);
                DatabaseManager.user.niveles[valorNivel].secuenciaInstucciones.Add(spritesAEjecutar);
                DatabaseManager.user.niveles[valorNivel].accionesEliminadas.Add(GridAccionesManager.instance.accionesEliminadas);
                DatabaseManager.user.niveles[valorNivel].cambiosAcciones.Add(GridAccionesManager.instance.cambiosAcciones);
                DatabaseManager.user.niveles[valorNivel].tiempoPensando.Add(tiempoPensando);
                DatabaseManager.user.niveles[valorNivel].mApretadas.Add(movimiento.instance.mApretadas);
                DatabaseManager.user.niveles[valorNivel].tabsApretados.Add(movimiento.instance.tabsApretados);
            }

        }
        if (!botonMovementManager.instance.isTutorial && pressedPlay)
        {
            DatabaseManager.user.niveles[valorNivel].tipoIntento.Add(tipoIntento + "Restarted(+1)");
            DatabaseManager.user.niveles[valorNivel].porcentajeAvance.Add((float)(Math.Round(PorcentajeManager.Instance.porcentajeAvance * 10000.0f) / 10000.0f) * 100.0f);
        }
        InstruccioneraManager.instance.SetEsJugable(true);
        BotoneraManager.instance.SetEsJugable(true);
        //MOSTRAR PANTALLA DE MUERTE. CON COROUTINE
        restarted = true;
        SceneManager.LoadScene(level);
    }

    public void Restart()
    {
        
        intentosFallidos++;
        if (!botonMovementManager.instance.isTutorial)
        {
            DatabaseManager.user.niveles[valorNivel].tipoIntento.Add(tipoIntento + "Failed(+0)");
            DatabaseManager.user.niveles[valorNivel].porcentajeAvance.Add((float)(Math.Round(PorcentajeManager.Instance.porcentajeAvance * 10000.0f) / 10000.0f) * 100.0f);
        }
        InstruccioneraManager.instance.SetEsJugable(true);
        BotoneraManager.instance.SetEsJugable(true);
        //MOSTRAR PANTALLA DE MUERTE. CON COROUTINE
        restarted = true;
        SceneManager.LoadScene(level);
    }   
}
