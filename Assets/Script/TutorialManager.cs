using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TutorialManager : MonoBehaviour
{
    public static TutorialManager instance { get; private set; }
    public Transform gameplayScene;
    public Transform tutorialScene;
    public Transform gameplayStuff;
    public Transform tutorialStuff;

    public Transform dialogBox;

    public Button botonEjecutar;

    public int contadorDeDialogos = 0;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        gameplayScene = gameplayScene.gameObject.GetComponent<Transform>();
        tutorialScene = tutorialScene.gameObject.GetComponent<Transform>();
        gameplayStuff = gameplayStuff.gameObject.GetComponent<Transform>();
        tutorialStuff = tutorialStuff.gameObject.GetComponent<Transform>();
        dialogBox = dialogBox.gameObject.GetComponent<Transform>();
        botonEjecutar = botonEjecutar.gameObject.GetComponent<Button>();
        botonEjecutar.interactable = false;
    }
    public void TutorialMessageToTutorialGameplay()
    {
        gameplayScene.gameObject.SetActive(true);
        tutorialScene.gameObject.SetActive(false);
        gameplayStuff.gameObject.SetActive(true);
        tutorialStuff.gameObject.SetActive(false);
    }
    public void TutorialGameplayToTutorialMessage()
    {
        tutorialScene.gameObject.SetActive(true);
        gameplayScene.gameObject.SetActive(false);
    }

    public void CloseTutorialMessageStuffs()
    {
        tutorialScene.gameObject.SetActive(false);
        tutorialStuff.gameObject.SetActive(false);
        dialogBox.gameObject.SetActive(false);
        botonEjecutar.interactable = true;
    }
}
