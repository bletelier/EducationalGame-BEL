using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class User
{
    public string rut;
    public string nickName;
    public string localId;
    public int edad;
    public int puntajeAcumulado;
    public string carrera;
    public string sexo;
    public Nivel[] niveles;
    public User()
    {

    }
    public User(string _localId)
    {
        rut = "";
        localId = _localId;
        nickName = "";
        sexo = "";
        carrera = "";
        edad = 0;
        puntajeAcumulado = 0;
        niveles = new Nivel[5];
        for (int i = 0; i < niveles.Length; i++)
        {
            niveles[i] = new Nivel();
        }
    }
}
