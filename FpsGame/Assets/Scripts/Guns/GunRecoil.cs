using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunRecoil : MonoBehaviour
{
    private Vector3 currentRotation;
    private Vector3 targetRotation;
    private Vector3 targetRotation2;

    public GunSystem gun;

    public float snappiness;

    private float timer = 0;

    private void Update()
    {

        targetRotation -= new Vector3(0, -90, targetRotation2.z / gun.timeBetweenShooting * Time.deltaTime);
        timer += Time.deltaTime;

        if (timer >= gun.timeBetweenShooting)
        {
            targetRotation2 = Vector3.zero;
        }
        currentRotation = Vector3.Slerp(currentRotation, targetRotation, snappiness * Time.fixedDeltaTime);
        transform.localRotation = Quaternion.Euler(new(0, -90, currentRotation.z));
    }

    public void HandleRecoil()
    {
        timer = 0;
        targetRotation += new Vector3(0, -90, -gun.recoil);
        targetRotation2 = targetRotation;
    }
}
