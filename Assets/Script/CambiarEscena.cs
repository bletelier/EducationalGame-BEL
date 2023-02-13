using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CambiarEscena : MonoBehaviour
{
    public void ChangeScene(string escena)
    {
        SceneManager.LoadScene(escena);
    }
    public void ResetearPuntuacionAcum()
    {
        DatabaseManager.user.puntajeAcumulado = 0;
    }


}
