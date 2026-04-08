using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
public class PlayerMovement : MonoBehaviour
{   
    [SerializeField] private float speed = 5f;
    [SerializeField] private float  jumpForce = 2f;
    [SerializeField] private float gravity = -9.81f;
    public byte DashAmount = 3; 
    [SerializeField] private float  dashforce = 20f;
    [SerializeField] private float dashDuration = 0.2f;
    [SerializeField] private GameObject HUDManager;
    private CharacterController controller;
    private Vector3 moveInput;
    private Vector3 velocity;
    private Vector3 dashVelocity;
    private float dashTimer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed && controller.isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpForce * -2f * gravity);
        }
    }

    public void OnCrouch(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            controller.height = 0.3f;
        }
        else if (context.canceled)
        {
            controller.height = 2f;
        }
    }

    public void OnDash(InputAction.CallbackContext context)
    {
        if (DashAmount > 0 && context.performed)
        {
            DashAmount--;
            Vector3 dashDir = transform.forward; 
            dashVelocity = dashDir * dashforce;
            dashTimer = dashDuration;
            Invoke(nameof(DashCounter), 2f);
            CallManager(DashAmount);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 move = new Vector3(moveInput.x, 0, moveInput.y);
        Vector3 moveDirection = transform.TransformDirection(move); 
        controller.Move(moveDirection * speed * Time.deltaTime);
        Vector3 finalMove = moveDirection * speed * Time.deltaTime;
        if (dashTimer > 0)
        {
            finalMove = dashVelocity;
            dashTimer -= Time.deltaTime;
        }
        controller.Move(finalMove * Time.deltaTime);
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    void DashCounter()
    {
        if (DashAmount < 3)
        {
            DashAmount++;
        }
        CallManager(DashAmount);
    }
    void CallManager(byte DashAmount)
    {
        if (HUDManager != null)
        {
            PlayerHUD hud = HUDManager.GetComponent<PlayerHUD>();
            if (hud != null)
            {
                hud.RefreshDashUi(DashAmount);
            }
        }
    }
}