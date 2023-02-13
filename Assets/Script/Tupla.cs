using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class Tupla: IComparable<Tupla>
{
    public int first;
    public string second;
    public Tupla(int _first, string _second)
    {
        first = _first;
        second = _second;
    }
    public int CompareTo(Tupla other)
    {
        return this.first.CompareTo(other.first) * -1; //Realizamos la operacion (* -1) para que los datos se ordenen de mayor a menor
    }
}
