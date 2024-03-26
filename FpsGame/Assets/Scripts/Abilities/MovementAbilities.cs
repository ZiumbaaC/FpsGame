using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class MovementAbilities : MonoBehaviour
{
    private PlayerController player;
    private string[] passiveAbilities;

    private void Awake()
    {
        player = GetComponent<PlayerController>();
        passiveAbilities = GetComponent<AbilityHandler>().passiveAbilities;
    }
    public void DASH()
    {
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

    public void LEAP()
    {
        float leapForce = 10 * (1 + 0.25f * BoolToFloat(passiveAbilities.Contains("Enhanced Movement")));


        if (passiveAbilities.Contains("Momentum Saver"))
        {
            player.velocity.y += leapForce;
        }
        else
        {
            player.velocity.y = leapForce;
            player.movementVelocity = new Vector3(0, 0, 0);
        }
    }

    public void GROUNDSLAM()
    {
        float slamForce = -10 * (1 + 0.25f * BoolToFloat(passiveAbilities.Contains("Enhanced Movement")));


        if (passiveAbilities.Contains("Momentum Saver"))
        {
            player.velocity.y += slamForce;
        }
        else
        {
            player.velocity.y = slamForce;
            player.movementVelocity = new Vector3(0, 0, 0);
        }
    }

    float BoolToFloat(bool b)
    {
        return b ? 1 : 0;
    }
}