using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.LowLevel.InputStateHistory;

public class Recoil : MonoBehaviour
{
    private Vector3 currentRotation;
    private Vector3 targetRotation;
    private Vector3 targetRotation2;

    private GunSystem gun;

    public float snappiness;

    private float timer = 0;


    private void Awake()
    {
        gun = transform.parent.parent.GetComponent<GunSystem>();
    }
    private void Update()
    {
        targetRotation -= new Vector3(targetRotation2.x / gun.timeBetweenShooting * Time.deltaTime, 0, 0);
        timer += Time.deltaTime;

        if(timer >= gun.timeBetweenShooting)
        {
            targetRotation2 = Vector3.zero;
        }

        Debug.Log(Vector3.Lerp(targetRotation2, Vector3.zero, gun.timeBetweenShooting * Time.deltaTime));
        currentRotation = Vector3.Slerp(currentRotation, targetRotation, snappiness * Time.fixedDeltaTime);
        transform.localRotation = Quaternion.Euler(currentRotation);
    }

    public void HandleRecoil()
    {
        timer = 0;
        targetRotation += new Vector3(gun.recoil, 0, 0);
        targetRotation2 = targetRotation;
    }
}
