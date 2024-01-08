using StarterAssets;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class DamageAbilities : MonoBehaviour
{
    public StarterAssetsInputs _input;
    public string Ability;
    

    void Start()
    {
        _input = GetComponent<StarterAssetsInputs>();
    }

    private void Update()
    {
        if(_input.damage)
        {
            Invoke(Ability, 0);
        }
    }

    void Balls()
    {
        Debug.Log("balls");
    }
}