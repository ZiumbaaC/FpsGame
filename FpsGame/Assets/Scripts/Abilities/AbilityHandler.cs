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
    public void Movement1()
    {
        if (movement1 != "") { movementAbilities.GetType().GetMethod(movement1.ToUpper()).Invoke(movementAbilities, null); }
    }

    public void Movement2()
    {
        if (movement2 != "") { movementAbilities.GetType().GetMethod(movement2.ToUpper()).Invoke(movementAbilities, null); }
    }

    public void Damage()
    {
        if (damage != "") { movementAbilities.GetType().GetMethod(damage.ToUpper()).Invoke(damageAbilities, null); }
    }
}
