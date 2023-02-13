using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class DialogueManager : MonoBehaviour
{
    private Queue<string> sentences;
    public AudioClip talk;
    public static int contadorDeDialogos = 0;
    public Button botonEjecutar;
    public Transform dialogBox;
    public string escena;
    public static DialogueManager instance { get; private set; }
    public Text nombreNPC;
    public Text dialogoNPC;
    public bool puedoPasar;
    public ContinueButton cntBtn;

    public DoctorAnimManager dcrAnim;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        sentences = new Queue<string>();
        if(contadorDeDialogos>=1)
        {
            botonEjecutar = botonEjecutar.gameObject.GetComponent<Button>();
            dialogBox = dialogBox.gameObject.GetComponent<Transform>();
            botonEjecutar.interactable = false;
        }
        nombreNPC = nombreNPC.gameObject.GetComponent<Text>();
        dialogoNPC = dialogoNPC.gameObject.GetComponent<Text>();
        puedoPasar = true;
}

    public void StartDialogue(Dialogue dialogue)
    {
        nombreNPC.text = dialogue.name;
        sentences.Clear();
        foreach(string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (puedoPasar)
        {
            if (sentences.Count == 0)
            {
                EndDialogue();
                return;
            }
            string sentence = sentences.Dequeue();
            StopAllCoroutines();
            StartCoroutine(TypeSentence(sentence));
        }
    }
    IEnumerator TypeSentence(string sentence)
    {
        puedoPasar = false;
        dialogoNPC.text = "";
        if(dcrAnim!=null)
        {
            dcrAnim.DoctorHablando();
        }
        foreach(char letter in sentence.ToCharArray())
        {
            if(cntBtn.adelanta)
            {
                SoundManagerTutorial.PlaySound(talk);
                break;
            }
            dialogoNPC.text += letter;
            SoundManagerTutorial.PlaySound(talk);
            yield return null;
        }
        dialogoNPC.text = sentence;
        if (dcrAnim != null)
        {
            dcrAnim.DoctorCallado();
        }
        puedoPasar = true;
    }

    public void EndDialogue()
    {
        contadorDeDialogos++;
        if(contadorDeDialogos <= 1)
        {
            SceneManager.LoadScene(escena);
        }
        else
        {
            CleanDialogueBox();
            botonEjecutar.interactable = true;
            dialogBox.gameObject.SetActive(false);
            Destroy(this.gameObject);
        }
    }

    public void CleanDialogueBox()
    {
        dialogoNPC.text = "";
        nombreNPC.text = "";
    }
}
