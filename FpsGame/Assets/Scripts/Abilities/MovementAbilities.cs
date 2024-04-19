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
    private AbilityHandler ab;

    private void Awake()
    {
        player = GetComponent<PlayerController>();
        ab = GetComponent<AbilityHandler>();
        passiveAbilities = ab.passiveAbilities;
    }
    public void DASH(int number)
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

    public void LEAP(int number)
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

    public void GROUND_SLAM(int number)
    {
        float slamForce = -50 * (1 + 0.25f * BoolToFloat(passiveAbilities.Contains("Enhanced Movement")));


        if (passiveAbilities.Contains("Momentum Saver"))
        {
            player.velocity.y = slamForce;
        }
        else
        {
            player.velocity.y = slamForce;
            player.movementVelocity = new Vector3(0, 0, 0);
        }
    }

    public void LEVITATE(int number)
    {
        float time = 4 * (1 + 0.25f * BoolToFloat(passiveAbilities.Contains("Enhanced Movement")));


        if (number == 1)
        {
            ab.movement1Timer = time;
            while (ab.movement1Timer > 0)
            {
                player.velocity = new Vector3(0, 0, 0);
            }
        }
        else
        {
            ab.movement2Timer = time;
            while (ab.movement2Timer > 0)
            {
                player.velocity = new Vector3(0, 0, 0);
            }
        }
    }

    float BoolToFloat(bool b)
    {
        return b ? 1 : 0;
    }
}