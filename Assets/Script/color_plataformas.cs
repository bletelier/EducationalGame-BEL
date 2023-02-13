using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class color_plataformas : MonoBehaviour
{
    public AudioClip death;
    public SpriteRenderer sp;
    public Light luz;
    public Color[] clrs;
    public bool esInicial = false;
    private bool mori;
    public bool yaPase;
    public bool soyImportante;
    public color_plataformas plataformaPareada;
    
    // Start is called before the first frame update
    void Awake()
    {
        sp = sp.gameObject.GetComponent<SpriteRenderer>();
        luz = luz.gameObject.GetComponent<Light>();     
        if(plataformaPareada != null)
        {
            plataformaPareada = plataformaPareada.gameObject.GetComponent<color_plataformas>();
        }
    }

    private void Start()
    {
        if(esInicial)
        {
            sp.color = clrs[clrs.Length - 1];
            luz.color = clrs[clrs.Length - 1];
        }
        else
        {
            int color = (int)Random.Range(0, clrs.Length-1);
            sp.color = clrs[color];
            luz.color = clrs[color];
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if (sp.color != botonMovement.instance.colorActual)
            {
                mori = true;
                botonMovement.instance.perdi = true;
                movimiento.instance.mori = true;
                BeaAudioManager.audioSrc.Stop();
                SoundManagerTutorial.PlaySound(death);
            }
            else
            {
                if (plataformaPareada != null && !plataformaPareada.yaPase && !yaPase && soyImportante)
                {
                    yaPase = true;
                    plataformaPareada.yaPase = true;
                    PorcentajeManager.Instance.porcentajeAvance = PorcentajeManager.Instance.porcentajeAvance + PorcentajeManager.Instance.puntajePorPlataforma;
                    PorcentajeManager.Instance.porcentajeAvance = PorcentajeManager.Instance.porcentajeAvance + PorcentajeManager.Instance.puntajePorPlataforma;
                }
                else if (plataformaPareada == null && !yaPase && soyImportante)
                {
                    yaPase = true;
                    PorcentajeManager.Instance.porcentajeAvance = PorcentajeManager.Instance.porcentajeAvance + PorcentajeManager.Instance.puntajePorPlataforma;
                }
            }            
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && !mori)
        {
            sp.color = botonMovement.instance.colorActual;
            luz.color = botonMovement.instance.colorActual;
        }
    }

}
