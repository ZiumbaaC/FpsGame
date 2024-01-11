using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    // Start is called before the first frame update
    private FpsGameInput input;
    private FpsGameInput.PlayerActions playerActions;

    private PlayerController playerController;
    private PlayerLookInput look;
    private GunSystem gun;

    void Awake()
    {
        input = new();
        playerActions = input.Player;
        playerController = GetComponent<PlayerController>();
        look = GetComponent<PlayerLookInput>();
        gun = GetComponent<GunSystem>();
        playerActions.Jump.performed += ctx => playerController.Jump();
        playerActions.Fire.performed += ctx => gun.Shoot();
        playerActions.Reload.performed += ctx => gun.Reload();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        playerController.HandleMovement(playerActions.Move.ReadValue<Vector2>());
    }

    private void LateUpdate()
    {
        look.HandleLook(playerActions.Look.ReadValue<Vector2>());
    }

    private void OnEnable()
    {
        playerActions.Enable();
    }

    private void OnDisable()
    {
        playerActions.Disable();
    }
}