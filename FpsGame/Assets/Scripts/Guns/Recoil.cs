using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.LowLevel.InputStateHistory;

public class Recoil : MonoBehaviour
{
    private Vector3 currentRotation;
    private Vector3 targetRotation;

    private GunSystem gun;

    public float snappiness;


    private void Awake()
    {
        gun = transform.parent.parent.GetComponent<GunSystem>();
    }
    private void Update()
    {
        targetRotation = Vector3.Lerp(targetRotation, Vector3.zero, gun.timeBetweenShooting * 2 * Time.deltaTime);
        currentRotation = Vector3.Slerp(currentRotation, targetRotation, snappiness * Time.fixedDeltaTime);
        transform.localRotation = Quaternion.Euler(currentRotation);
    }

    public void HandleRecoil()
    {
        targetRotation += new Vector3(gun.recoil, 0, 0);
        Debug.Log(targetRotation);
    }
}
