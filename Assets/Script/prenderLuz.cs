using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class prenderLuz : MonoBehaviour
{
    public AudioClip winS;
    public AudioClip turnOn;
    public Transform luzTransform;
    private bool win;
    public Animator[] anim;
    public Sprite encendido;
    public bool isTutorial;

    // Start is called before the first frame update
    void Start()
    {
        luzTransform = luzTransform.gameObject.GetComponent<Transform>();
        win = false;
        if(isTutorial)
        {
            for (int i = 0; i < anim.Length; ++i)
            {
                anim[i] = anim[i].gameObject.GetComponent<Animator>();
            }
        }
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && !botonMovement.instance.perdi && !win)
        {
            StartCoroutine(CheckearSiPerdi(1));
        }
    }

    IEnumerator CheckearSiPerdi(int cantidad)
    {
        int cont = 0;
        bool perdi = botonMovement.instance.perdi;
        while(cont < cantidad)
        {
            yield return null;
            perdi = botonMovement.instance.perdi;
            cont++;
        }
        if(!perdi)
        {
            win = true;
            botonMovementManager.instance.SetWin(true);
            SoundManagerTutorial.PlaySound(winS);
            if (!botonMovementManager.instance.isTutorial)
            {
                DatabaseManager.user.niveles[botonMovement.instance.valorNivel].tipoIntento.Add("Initiated(+1),Victory(+0)");
                DatabaseManager.user.niveles[botonMovement.instance.valorNivel].porcentajeAvance.Add(100.0f);
            }
            else
            {
                DialogueManager.contadorDeDialogos = 0;
            }
            this.gameObject.GetComponent<SpriteRenderer>().sprite = encendido;

            StartCoroutine(PrenderLaLuz(5.0f));
        }
    }
    // Update is called once per frame
    void Update()
    {
    }

    IEnumerator PrenderLaLuz(float s)
    {
        SoundManagerTutorial.PlaySound(turnOn);
        float c = 0;
        while(c < s)
        {
            
            luzTransform.Rotate(Vector3.left * ((80*0.1f)/s));
            yield return new WaitForSeconds(0.1f);
            c += 0.1f;

        }

        /*
         * 
         *     GANE Y TERMINO DE PRENDERSE LA LUZ  
         *  
        */
        for (int i = 0; i < anim.Length; ++i)
        {
            anim[i].SetBool("luz_prendida", true);
        }
        
    }
}
