using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.LowLevel.InputStateHistory;

public class Recoil : MonoBehaviour
{
    private Vector3 currentRotation;
    private Vector3 targetRotation;
    private Vector3 targetRotation2;

    public GunSystem gun;

    public float snappiness;

    private float timer = 0;

    private void Update()
    {

        targetRotation -= new Vector3(targetRotation2.x / gun.timeBetweenShooting * Time.deltaTime, 0, 0);
        timer += Time.deltaTime;

        if(timer >= gun.timeBetweenShooting)
        {
            targetRotation2 = Vector3.zero;
        }
        currentRotation = Vector3.Slerp(currentRotation, targetRotation, snappiness * Time.fixedDeltaTime);
        transform.localRotation = Quaternion.Euler(new(currentRotation.x, 0, 0));
    }

    public void HandleRecoil()
    {
        timer = 0;
        targetRotation += new Vector3(gun.recoil, 0, 0);
        targetRotation2 = targetRotation;
    }
}
