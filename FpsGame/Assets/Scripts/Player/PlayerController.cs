using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI healthText;
    private CharacterController controller;
    private PlayerInput input;
    public Vector3 velocity;
    public Vector3 movementVelocity;
    private bool grounded;

    [SerializeField] private float maxHealth = 200f;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float gravity = -9.8f;
    [SerializeField] private float jumpHeight = 3f;
    [SerializeField] private float sprintingSpeedBoost = 3f;

    [SerializeField] private int bHoppingStacks = 0;
    [SerializeField] private float bHoppingSpeedIncrease = 0.1f;
    [SerializeField] private bool bHoppable = false;
    [SerializeField] private int bHoppingCounter = 0;
    [SerializeField] private int bHoppingFrameWindow = 2;

    [HideInInspector] public float health;
    private float movementSpeed;

    // Start is called before the first frame update
    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        input = GetComponent<PlayerInput>();
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        grounded = controller.isGrounded;
        movementSpeed = speed + sprintingSpeedBoost * Convert.ToInt32(input.sprinting) + bHoppingStacks * bHoppingSpeedIncrease;
        healthText.text = NormalizeText($"Health: {health}");
    }

    private void FixedUpdate()
    {
        if (grounded)
        {
            if (bHoppingCounter > bHoppingFrameWindow)
            {
                bHoppable = false;
            }
            else
            {
                bHoppable = true;
            }
            bHoppingCounter++;
        }
        else
        {
            bHoppable = false;
        }

    }

    public void HandleMovement(Vector2 input)
    {
        movementVelocity.x += input.x;
        movementVelocity.z += input.y;

        controller.Move(movementSpeed * Time.deltaTime * transform.TransformDirection(movementVelocity));
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        movementVelocity /= 1.15f;

        if(grounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
    }

    public void Jump()
    {
        if (grounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * 3 * -gravity);
            bHoppingCounter = -1;
            if (bHoppable)
            {
                bHoppingStacks++;
            }
        }
    }

    string NormalizeText(string input)
    {
        string output = "";

        for (int i = 0; i < input.Length; i++)
        {
            output += input[^(i + 1)];
        }

        return output;
    }
}
