using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AsignData : MonoBehaviour
{
    public AudioClip write;
    public AudioClip appear;
    public Text levelText;
    public Text scoreText;
    public Dropdown dificultadDD;
    public Dropdown frustracionDD;
    public Button nextButton;

    private void Start()
    {
        nextButton.gameObject.SetActive(false);
        levelText.text = "";
        scoreText.text = "";
        StartCoroutine(SetLevelName());
    }

    public void ClickedNext()
    {
        
    }

    IEnumerator SetLevelName()
    {
        yield return new WaitForSeconds(0.3f);
        string levelTextAux = "";
        foreach  (char letter in LevelData.levelName)
        {
            levelTextAux = levelTextAux + letter;
            levelText.text = levelTextAux;
            SoundManagerTutorial.PlaySound(write);
            yield return null;
        }
        yield return new WaitForSeconds(0.2f);
        StartCoroutine(SetLevelScore());
    }

    IEnumerator SetLevelScore()
    {
        string textoAMostrar = "Puntuación: " + LevelData.puntuacion.ToString();
        string scoreTextAux = "";
        foreach (char letter in textoAMostrar)
        {
            scoreTextAux = scoreTextAux + letter;
            scoreText.text = scoreTextAux;
            SoundManagerTutorial.PlaySound(write);
            yield return null;
        }
        yield return new WaitForSeconds(0.1f);
        SoundManagerTutorial.PlaySound(appear);
        dificultadDD.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        SoundManagerTutorial.PlaySound(appear);
        frustracionDD.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        SoundManagerTutorial.PlaySound(appear);
        nextButton.gameObject.SetActive(true);
    }
}
