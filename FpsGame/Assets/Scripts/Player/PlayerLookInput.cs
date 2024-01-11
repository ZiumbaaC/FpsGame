using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLookInput : MonoBehaviour
{

    public Camera cam;
    private float xRotation = 0f;
    private float xSens = 30f, ySens = 20f;

    public float sensitivity = 1;


    public void HandleLook(Vector2 input)
    {
        float mouseX = input.x;
        float mouseY = input.y;

        xRotation -= mouseY * Time.deltaTime * ySens * sensitivity;
        xRotation = Mathf.Clamp(xRotation, -80, 80);

        cam.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);

        transform.Rotate(mouseX * Time.deltaTime * sensitivity * xSens * Vector3.up);
    }
}
