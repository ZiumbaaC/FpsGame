using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Multiplier : MonoBehaviour
{
    public string Type;
    public float Number;
    public Multiplier(string type, float number)
    {
        Type = type;
        Number = number;
    }
}
