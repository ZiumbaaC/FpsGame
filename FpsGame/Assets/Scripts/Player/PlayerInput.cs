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

    public bool sprinting = false;

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
        playerActions.Damage.performed += ctx => PlaceHolder("Damage");
        playerActions.Movement1.performed += ctx => PlaceHolder("Movement1");
        playerActions.Movement2.performed += ctx => PlaceHolder("Movement2");
        playerActions.SprintKeyboard.started += ctx => BeginSprint();
        playerActions.SprintKeyboard.canceled += ctx => CancelSprint();
        
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

    private void PlaceHolder(string id)
    {
        Debug.Log(id);
    }

    private void BeginSprint()
    {
        sprinting = true;
    }

    private void CancelSprint()
    {
        sprinting = false;
    }
}