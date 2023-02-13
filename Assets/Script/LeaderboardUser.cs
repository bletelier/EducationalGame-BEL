using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LeaderboardUser
{
    public string nickName;
    public int puntajeAcumulado;
    public string carrera;
    public LeaderboardUser(string _nickName, int _puntajeAcumulado, string _carrera)
    {
        nickName = _nickName;
        puntajeAcumulado = _puntajeAcumulado;
        carrera = _carrera;
    }
}
