using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PorcentajeManager : MonoBehaviour
{
    public static PorcentajeManager Instance { get; private set; }
    public List<Transform> plataformasImportantes;
    public float porcentajeAvance = 0.0f;
    public float puntajePorPlataforma;
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        for (int i = 0; i < plataformasImportantes.Count; i++)
        {
            plataformasImportantes[i] = plataformasImportantes[i].gameObject.GetComponent<Transform>();
        }
        puntajePorPlataforma = (float)(1.0f / (float)(plataformasImportantes.Count));
    }


    
}
