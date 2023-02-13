using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MostrarDisclaimer : MonoBehaviour
{
    public Image att1;
    public Image att2;
    public Text title;
    public Text disclaimer;
    public Button next;
    public AudioClip write;
    public AudioClip appear;
    private string textoAMostrar;
    // Start is called before the first frame update
    void Start()
    {
        textoAMostrar = disclaimer.text;
        disclaimer.text = "";
        StartCoroutine(MostrarTexto());
    }

    IEnumerator MostrarTexto()
    {
        yield return new WaitForSeconds(0.1f);
        att1.gameObject.SetActive(true);
        title.gameObject.SetActive(true);
        att2.gameObject.SetActive(true);
        SoundManagerTutorial.PlaySound(appear);
        yield return new WaitForSeconds(0.1f);
        string aux = "";
        foreach(char c in textoAMostrar)
        {
            aux += c;
            disclaimer.text = aux;
            SoundManagerTutorial.PlaySound(write);
            yield return null;
        }
        yield return new WaitForSeconds(0.2f);
        next.gameObject.SetActive(true);
        SoundManagerTutorial.PlaySound(appear);
    }
}
