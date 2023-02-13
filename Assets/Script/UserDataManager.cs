using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserDataManager : MonoBehaviour
{
    public static UserDataManager Instance { get; private set; }
    public Nivel nivelData;
    public User userData;
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }
    public void NuevoNivel()
    {
        nivelData = new Nivel();
    }
}
