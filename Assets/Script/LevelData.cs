using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelData : MonoBehaviour
{
    public static int puntuacion;
    public static string levelName;
    public string levelNameAux;
    public static string levelNext;
    public string levelNextAux;

    private void Start()
    {
        if(levelName == null && levelNext == null)
        {
            levelName = levelNameAux;
            levelNext = levelNextAux;
        }
    }
    public static void CleanStaticsData()
    {
        levelName = null;
        levelNext = null;
    }
}
