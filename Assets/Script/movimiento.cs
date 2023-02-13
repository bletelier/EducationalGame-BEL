using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class movimiento : MonoBehaviour
{
    public AudioClip jump;
    public AudioClip fall;
    public float velocidad = 0.0f;
    public float jumpSpeed;
    private Rigidbody2D playerRigidbody;
    public bool canJump;
    private float tiempoUltimoSalto = 0;
    public float fallMultiplier;
    public float lowFallMultiplier;
    public float v = 0.1f;
    private bool first;
    private bool iFall;
    public string level;
    

    public int mApretadas = 0;
    public int tabsApretados = 0;

    public bool mori;
    private bool setted;

    public Animator[] anim;

    public Animator animCamera;

    public Transform groundChecker;

    public static movimiento instance { get; private set; }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        for (int i = 0; i < anim.Length; ++i)
        {
            anim[i] = anim[i].gameObject.GetComponent<Animator>();
        }
        animCamera = animCamera.gameObject.GetComponent<Animator>();
        canJump = false;
        first = false;
        iFall = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Movement();
        Raycasting();
    }

    private void Update()
    {
        CheckearMuerte();
        Saltar();
        AbrirBotonera();
    }
    private void CheckearMuerte()
    {
        if(mori && !setted)
        {
            botonMovement.instance.perdi = true;
            setted = true;
            for (int i = 0; i < anim.Length; ++i)
            {
                anim[i].SetBool("muerto", true);
            }
            StartCoroutine(botonMovement.instance.Esperar(0.8f));
        }
    }
    private void Saltar()
    {
        if (Mathf.Abs(playerRigidbody.velocity.y) < v && botonMovementManager.instance.salto && canJump)
        {
            if (anim[0].GetBool("izq"))
            {
                playerRigidbody.velocity = new Vector2(-0.23f, .85f) * jumpSpeed;
            }
            else if (!anim[0].GetBool("izq"))
            {
                playerRigidbody.velocity = new Vector2(0.23f, .85f) * jumpSpeed;
            }
            tiempoUltimoSalto = Time.time;
            SoundManagerTutorial.PlaySound(jump);
            canJump = false;
        }
    }


    public void AbrirBotonera()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            tabsApretados++;
            BotoneraManager.instance.AbrirCerrarBotonera();
           
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            mApretadas++;
            InstruccioneraManager.instance.AbrirCerrarInstruccionera();
        }
        if (Input.GetKeyDown(KeyCode.R) && !botonMovementManager.instance.GetWin())
        {
            if(botonMovementManager.instance.isTutorial && DialogueManager.contadorDeDialogos >= 2)
            {
                botonMovement.instance.RestartObligado();
            }
            else if(!botonMovementManager.instance.isTutorial)
            {
                botonMovement.instance.RestartObligado();
            }
        }
        
    }

   


    public void Movement()
    {
        if (botonMovementManager.instance.movioDer && botonMovement.instance.canMoveToThereDer)
        {
            transform.position += Vector3.right * velocidad * Time.deltaTime;
            for (int i = 0; i < anim.Length; ++i)
            {
                anim[i].SetBool("izq", false);
            }
        }
        else if(botonMovementManager.instance.movioDer && !botonMovement.instance.canMoveToThereDer)
        {                       
            GridAccionesManager.instance.EliminarAccionActual();
            botonMovementManager.instance.movioDer = false;
            botonMovement.instance.puedoCambiar = true;
        }
        if (botonMovementManager.instance.movioIzq && botonMovement.instance.canMoveToThereIzq)
        {
            transform.position += Vector3.left * velocidad * Time.deltaTime;
            for (int i = 0; i < anim.Length; ++i)
            {
                anim[i].SetBool("izq", true);
            }
        }
        else if (botonMovementManager.instance.movioIzq && !botonMovement.instance.canMoveToThereIzq)
        {                        
            GridAccionesManager.instance.EliminarAccionActual();
            botonMovementManager.instance.movioIzq = false;
            botonMovement.instance.puedoCambiar = true;
        }
        if (playerRigidbody.velocity.y < 0)
        {
            
            playerRigidbody.velocity += Vector2.up * Physics2D.gravity * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (playerRigidbody.velocity.y > 0 && (!botonMovementManager.instance.salto || (Time.time - tiempoUltimoSalto) > 0.15f))
        {
            
            playerRigidbody.velocity += Vector2.up * Physics2D.gravity * (lowFallMultiplier - 1) * Time.deltaTime;
            
        }
    }

    private void Raycasting()
    {
        iFall = canJump;
        canJump = Physics2D.Linecast(transform.position, groundChecker.position, 1 << LayerMask.NameToLayer("Ground"));
        for (int i = 0; i < anim.Length-1; ++i)
        {
            anim[i].SetBool("salto", !canJump);
        }
        if(iFall && !canJump)
        {
            first = true;
            
        }
        if (canJump && first && botonMovementManager.instance.salto) //AQUI SE CONTROLA CUANDO JUSTO TOCA EL SUELO
        {
            SoundManagerTutorial.PlaySound(fall);
            playerRigidbody.velocity = new Vector2(0, 0);
            botonMovement.instance.puedoCambiar = true;
            botonMovementManager.instance.salto = false;
            botonMovementManager.instance.saltoSiguiente = true;
            GridAccionesManager.instance.EliminarAccionActual();
            animCamera.SetTrigger("salto");
            first = false;
        }
        else if(canJump && first)
        {
            playerRigidbody.velocity = new Vector2(0, 0);
            animCamera.SetTrigger("salto");
            first = false;
        }
    }
}
