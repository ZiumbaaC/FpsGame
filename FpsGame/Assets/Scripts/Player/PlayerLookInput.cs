using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLookInput : MonoBehaviour
{

    [SerializeField] private Camera cam;
    private float xRotation = 0f;
    private float sens = 50f;
    [SerializeField] private float sensDistribution = 0.6f;

    [SerializeField] private float sensitivity = 1;


    public void HandleLook(Vector2 input)
    {
        float mouseX = input.x;
        float mouseY = input.y;

        float xSens = sens * sensDistribution;
        float ySens = sens * (1 - sensDistribution);

        xRotation -= mouseY * Time.deltaTime * ySens * sensitivity;
        xRotation = Mathf.Clamp(xRotation, -80, 80);

        cam.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);

        transform.Rotate(mouseX * Time.deltaTime * sensitivity * xSens * Vector3.up);
    }

    public void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
