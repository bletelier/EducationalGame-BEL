using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Survey
{
    public string[] respuestas = new string[7];

    public Survey()
    {
        for (int i = 0; i < respuestas.Length; i++)
        {
            respuestas[i] = "";
        }
    }
}
