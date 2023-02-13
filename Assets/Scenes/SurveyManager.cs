using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SurveyManager : MonoBehaviour
{
    public Transform[] preguntas;
    public bool[] esTextField;
    public Image boton;
    public Sprite spriteBoton;
    public int preguntasQueLlevo = 0;
    private Survey survey;
    public Text faltante;
    private void Awake()
    {
        for (int i = 0; i < preguntas.Length; ++i)
        {
            preguntas[i] = preguntas[i].gameObject.GetComponent<Transform>();
            if (i == 0) continue;
            preguntas[i].gameObject.SetActive(false);
            faltante.text = (preguntasQueLlevo+1) + "/" + (preguntas.Length);
        }
    }
    private void Start()
    {
        
        if(DatabaseManager.user != null)
        {
            survey = new Survey();
        }
        else
        {
            Debug.Log("Debo Loguear");
        }

    }

    public void SubmitToDatabase()
    {
        if(survey == null)
        {
            Debug.Log("No puedo, debo Loguear");
            ChangeSceneManager.Instance.ChangeScene("SignIn");
        }
        else if(preguntasQueLlevo < preguntas.Length)
        {
            if(esTextField[preguntasQueLlevo])
            {
                InputField dropdownAux = preguntas[preguntasQueLlevo].transform.GetChild(1).GetComponent<InputField>();
                if (dropdownAux.text.Length == 0)
                {
                    Debug.Log("Nada seleccionado");
                    return;
                }
                survey.respuestas[preguntasQueLlevo] = dropdownAux.text;
            }
            else
            {
                Dropdown dropdownAux = preguntas[preguntasQueLlevo].transform.GetChild(1).GetComponent<Dropdown>();
                if (dropdownAux.value == 0)
                {
                    Debug.Log("Nada seleccionado");
                    return;
                }
                survey.respuestas[preguntasQueLlevo] = dropdownAux.options[dropdownAux.value].text;                
            }
            preguntas[preguntasQueLlevo].gameObject.SetActive(false);
            preguntasQueLlevo++;
            faltante.text = (preguntasQueLlevo+1) + "/" + (preguntas.Length );
            if (preguntasQueLlevo == preguntas.Length-1)
            {
                boton.sprite = spriteBoton;
            }
            if (preguntasQueLlevo < preguntas.Length)
            {
                
                preguntas[preguntasQueLlevo].gameObject.SetActive(true);
            }
            else
            {
                faltante.gameObject.SetActive(false);
                boton.gameObject.SetActive(false);
                DatabaseManager.Instance.PutSurveyInDatabase(survey);
            }
            
        }
    }
    
}
