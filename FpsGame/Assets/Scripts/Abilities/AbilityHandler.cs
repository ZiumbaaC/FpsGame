using System;
using System.Reflection;
using UnityEngine;

public class AbilityHandler : MonoBehaviour
{
    [SerializeField] private MovementAbilities movementAbilities;
    [SerializeField] private DamageAbilities damageAbilities;
    [SerializeField] private string movement1 = "";
    [SerializeField] private string movement2 = "";
    [SerializeField] private string damage = "";
    public string[] passiveAbilities;

    public float movement1Timer, movement2Timer, damageTimer;


    public void Movement1()
    {
        if (movement1 != "") { movementAbilities.GetType().GetMethod(movement1.ToUpper()).Invoke(movementAbilities, new object[] { 1 }); }
    }

    public void Movement2()
    {
        if (movement2 != "") { movementAbilities.GetType().GetMethod(movement2.ToUpper()).Invoke(movementAbilities, new object[] { 2 }); }
    }

    public void Damage()
    {
        if (damage != "") { movementAbilities.GetType().GetMethod(damage.ToUpper()).Invoke(damageAbilities, null); }
    }

    private void Update()
    {
        movement1Timer = movement1Timer - Time.deltaTime < 0 ? 0 : movement1Timer - Time.deltaTime;
        movement2Timer = movement2Timer - Time.deltaTime < 0 ? 0 : movement2Timer - Time.deltaTime;
        damageTimer = damageTimer - Time.deltaTime < 0 ? 0 : damageTimer - Time.deltaTime;
    }
}
