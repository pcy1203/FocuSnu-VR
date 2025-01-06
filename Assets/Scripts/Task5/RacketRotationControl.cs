using UnityEngine;
using UnityEngine.InputSystem;

public class RacketRotationControl : MonoBehaviour
{
    public float rotationSpeed = 50f; // Speed of rotation adjustment
    
    private PlayerControls playerControls;
    private Vector2 rotationInput;

    private void Awake()
    {
        // Initialize the PlayerControls input actions
        playerControls = new PlayerControls();
    }

    private void OnEnable()
    {
        // Enable the input actions and listen to joystick movement
        playerControls.Player.Rotate.performed += ctx => rotationInput = ctx.ReadValue<Vector2>();
        playerControls.Player.Rotate.canceled += ctx => rotationInput = Vector2.zero; // Stop rotation on release
        playerControls.Enable();
    }

    private void OnDisable()
    {
        // Disable input actions
        playerControls.Disable();
    }

    void Update()
    {
        AdjustRotation();
    }

    private void AdjustRotation()
    {
        // Rotate based on joystick input
        float rotationX = rotationInput.y * rotationSpeed * Time.deltaTime; // Up/down rotation
        float rotationY = rotationInput.x * rotationSpeed * Time.deltaTime; // Left/right rotation

        transform.Rotate(rotationX, rotationY, 0, Space.Self); // Adjust racket rotation
    }
}
