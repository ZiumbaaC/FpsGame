using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private PlayerInput input;
    private Vector3 velocity;
    private bool grounded;

    public float speed = 5f;
    public float gravity = -9.8f;
    public float jumpHeight = 3f;
    public float sprintingSpeedBoost = 3f;

    private float movementSpeed;

    // Start is called before the first frame update
    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        input = GetComponent<PlayerInput>();
    }

    // Update is called once per frame
    void Update()
    {
        grounded = controller.isGrounded;
        movementSpeed = speed + sprintingSpeedBoost * Convert.ToInt32(input.sprinting);
    }

    public void HandleMovement(Vector2 input)
    {
        Vector3 move = Vector3.zero;
        move.x = input.x;
        move.z = input.y;

        controller.Move(movementSpeed * Time.deltaTime * transform.TransformDirection(move));
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        if(grounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
    }

    public void Jump()
    {
        if (grounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -3 * gravity);
        }
    }
}
