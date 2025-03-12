using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Cinemachine;
using UnityEditor;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float walkSpeed = 5f;
    public float runSpeed = 10f;
    public float jumpForce = 5f;
    public float sensitivity = 2f;
    public float zoomSpeed = 2f;
    public float minZoom = 2f;
    public float maxZoom = 10f;

    [Header("References")]
    public CharacterController controller;
    public Transform cameraTransform;
    public CinemachineOrbitalFollow freeLookCamera;

    private PlayerControls controls;
    private Vector2 moveInput;
    private Vector2 lookInput;
    private float zoomInput;
    private bool isRunning;
    private bool isJumping;
    private Vector3 velocity;
    private float gravity = -9.81f;
    private bool isQuitting;

    private void Awake()
    {
        controls = new PlayerControls();
    }

    private void OnEnable()
    {
        controls.Player.Enable();

        controls.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        controls.Player.Move.canceled += ctx => moveInput = Vector2.zero;

        controls.Player.Look.performed += ctx => lookInput = ctx.ReadValue<Vector2>();
        controls.Player.Look.canceled += ctx => lookInput = Vector2.zero;

        controls.Player.Sprint.performed += ctx => isRunning = true;
        controls.Player.Sprint.canceled += ctx => isRunning = false;

        controls.Player.Jump.performed += ctx => { if (controller.isGrounded) isJumping = true; };

        controls.Player.Scroll.performed += ctx => zoomInput = ctx.ReadValue<Vector2>().y;

        controls.Player.Escape.performed += ctx => isQuitting = true;
        controls.Player.Escape.canceled += ctx => isQuitting = false;
    }

    private void OnDisable()
    {
        controls.Player.Disable();
    }

    private void Update()
    {
        Move();
        RotateCamera();
        ZoomCamera();
        QuitGame();
    }

    private void Move()
    {
        if (freeLookCamera == null) return;

        // Get the camera's forward and right directions (ignoring Y-axis)
        Vector3 cameraForward = freeLookCamera.transform.forward;
        cameraForward.y = 0;
        cameraForward.Normalize();

        Vector3 cameraRight = freeLookCamera.transform.right;
        cameraRight.y = 0;
        cameraRight.Normalize();

        // Calculate movement direction based on input
        Vector3 moveDirection = (cameraForward * moveInput.y + cameraRight * moveInput.x).normalized;

        // Apply movement
        float speed = isRunning ? runSpeed : walkSpeed;
        controller.Move(moveDirection * speed * Time.deltaTime);

        // Jumping logic
        if (isJumping)
        {
            velocity.y = Mathf.Sqrt(jumpForce * -2f * gravity);
            isJumping = false;
        }

        // Gravity
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        // Rotate player in movement direction (if moving)
        if (moveDirection.magnitude > 0.1f)
        {
            transform.rotation = Quaternion.LookRotation(moveDirection);
        }
    }

    private void RotateCamera()
    {
        if (freeLookCamera != null)
        {
            freeLookCamera.HorizontalAxis.Value += lookInput.x * sensitivity; // Horizontal rotation
            freeLookCamera.VerticalAxis.Value -= lookInput.y * sensitivity * 0.01f; // Vertical orbit (normalized 0-1)
        }
    }

    private void ZoomCamera()
    {
        if (freeLookCamera != null && zoomInput != 0)
        {
            float newRadius = Mathf.Clamp(
                freeLookCamera.Orbits.Center.Radius - zoomInput * zoomSpeed,
                minZoom,
                maxZoom
            );

            // Apply the zoom to all orbits

            freeLookCamera.Orbits.Top.Radius = newRadius;
            freeLookCamera.Orbits.Center.Radius = newRadius;
            freeLookCamera.Orbits.Bottom.Radius = newRadius;
        }
    }

    private void QuitGame()
    {
        if (isQuitting)
        {
            EditorApplication.ExitPlaymode();
        }
    }
}
