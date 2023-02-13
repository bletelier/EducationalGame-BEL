using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MostratEnPantalla : MonoBehaviour
{
    public AudioClip write;
    public AudioClip outS;
    public AudioClip xplosion;
    public Text textoX;
    public Text programadores;
    public string prog;
    public Text gameDes;
    public string gme;
    public Text lvlDes;
    public string lvl;
    public Text artistas;
    public string art;
    public Button salir;
    public SpriteRenderer benja;
    public Text benjaT;
    public SpriteRenderer paula;
    public Text paulaT;
    void Start()
    {
        benja.gameObject.SetActive(false);
        paula.gameObject.SetActive(false);
        benjaT.gameObject.SetActive(false);
        paulaT.gameObject.SetActive(false);
        textoX.gameObject.SetActive(false);
        salir.gameObject.SetActive(false);
        StartCoroutine(MostrarImg(benja, 0,benjaT));
        StartCoroutine(MostrarImg(paula, 1, paulaT));
        
        
    }
    IEnumerator MostrarImg(SpriteRenderer img, int num, Text text)
    {
        yield return new WaitForSeconds(0.2f);
        img.gameObject.SetActive(true);
        SoundManagerTutorial.PlaySound(xplosion);
        yield return new WaitForSeconds(0.25f);
        text.gameObject.SetActive(true);
        SoundManagerTutorial.PlaySound(outS);
        if (num == 1)
        {
            StartCoroutine(Mostrar(prog, programadores, false));
            StartCoroutine(Mostrar(art, artistas, false));
            StartCoroutine(Mostrar(gme, gameDes, false));
            StartCoroutine(Mostrar(lvl, lvlDes, true));
        }
    }
    IEnumerator Mostrar(string text, Text text1, bool final)
    {
        yield return new WaitForSeconds(0.3f);
        string texto = text;
        string aux = "";
        foreach  (char letter in texto)
        {
            aux += letter;
            text1.text = aux;
            SoundManagerTutorial.PlaySound(write);
            yield return null;
        }
        if(final)
        {
            yield return new WaitForSeconds(0.3f);
            textoX.gameObject.SetActive(true);
            SoundManagerTutorial.PlaySound(outS);
            yield return new WaitForSeconds(0.1f);
            salir.gameObject.SetActive(true);
            SoundManagerTutorial.PlaySound(outS);
        }
    }
}
