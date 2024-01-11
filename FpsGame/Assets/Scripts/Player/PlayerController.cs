using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 velocity;
    private bool grounded;
    public float speed = 5f;
    public float gravity = -9.8f;
    public float jumpHeight = 3f;

    // Start is called before the first frame update
    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        grounded = controller.isGrounded;
    }

    public void HandleMovement(Vector2 input)
    {
        Vector3 move = Vector3.zero;
        move.x = input.x;
        move.z = input.y;

        controller.Move(transform.TransformDirection(move) * speed * Time.deltaTime);
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
