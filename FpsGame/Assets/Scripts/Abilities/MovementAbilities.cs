using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class MovementAbilities : MonoBehaviour
{
    public void DASH()
    {
        PlayerController player = GetComponent<PlayerController>();
        string[] passiveAbilities = GetComponent<AbilityHandler>().passiveAbilities;
        float dashForce = 50 * (1 + 0.25f * BoolToFloat(passiveAbilities.Contains("Enhanced Movement")));


        player.movementVelocity += new Vector3(0, 0, dashForce);
        
        if (passiveAbilities.Contains("Momentum Saver"))
        {
            player.velocity.y += dashForce / 8;
        }
        else
        {
            player.velocity.y = dashForce / 8;
        }
    }

    float BoolToFloat(bool b)
    {
        return b ? 1 : 0;
    }
}