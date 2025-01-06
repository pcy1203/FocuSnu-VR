using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float mouseSensitivity = 100f;
    public Transform playerCamera;

    private Vector2 moveInput = Vector2.zero;
    private Vector2 lookInput = Vector2.zero;
    private float xRotation = 0f;
    private PlayerControls playerControls;

    private GameObject grabbedObject;
    public float grabDistance = 3f;

    public GameObject menuUI; // Reference to the menu UI

    private void Awake()
    {
        playerControls = new PlayerControls();
    }

    private void OnEnable()
    {
        if (playerControls == null)
        {
            playerControls = new PlayerControls();
        }

        playerControls.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        playerControls.Player.Move.canceled += ctx => moveInput = Vector2.zero;  // Reset input on release
        playerControls.Player.Look.performed += ctx => lookInput = ctx.ReadValue<Vector2>();
        playerControls.Player.Look.canceled += ctx => lookInput = Vector2.zero;  // Reset look input on release

        playerControls.Player.Click.performed += ctx => TryGrabObject(); // Detect click to grab
        // playerControls.Player.Escape.performed += ctx => HandleEscape(); // Detect escape key press
        playerControls.Player.Escape.performed += ctx => ToggleMenu(); // Detect menu key press
        playerControls.Enable();

        Cursor.lockState = CursorLockMode.Locked; // Lock the cursor to the center of the screen
        Cursor.visible = false; // Hide the cursor
    }

    private void OnDisable()
    {
        playerControls.Disable();
        Cursor.lockState = CursorLockMode.None; // Unlock the cursor
        Cursor.visible = true; // Show the cursor
    }

    void Update()
    {
        HandleMovement();
        HandleMouseLook();
        UpdateGrabbedObjectPosition();
    }

    private void HandleMovement()
    {
        Vector3 move = transform.right * moveInput.x + transform.forward * moveInput.y;
        transform.position += move * moveSpeed * Time.deltaTime;
    }

    private void HandleMouseLook()
    {
        float mouseX = lookInput.x * mouseSensitivity * Time.deltaTime;
        float mouseY = lookInput.y * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // Limit vertical rotation

        playerCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f); // Vertical rotation for the camera
        transform.Rotate(Vector3.up * mouseX); // Horizontal rotation for the player
    }

    private void TryGrabObject()
    {
        Ray ray = playerCamera.GetComponent<Camera>().ScreenPointToRay(Mouse.current.position.ReadValue());
        if (Physics.Raycast(ray, out RaycastHit hit, grabDistance))
        {
            if (hit.collider.CompareTag("Grabbable")) // Ensure object has Grabbable tag
            {
                grabbedObject = hit.collider.gameObject;
                grabbedObject.transform.SetParent(playerCamera); // Attach object to the camera
            }
        }
    }

    private void UpdateGrabbedObjectPosition()
    {
        if (grabbedObject != null)
        {
            grabbedObject.transform.position = playerCamera.position + playerCamera.forward * grabDistance;
        }
    }

    private void HandleEscape()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }

    private void ToggleMenu()
    {
        menuUI.SetActive(!menuUI.activeSelf); // Toggle the menu UI
        if (menuUI.activeSelf)
        {
            Cursor.lockState = CursorLockMode.None; // Unlock the cursor
            Cursor.visible = true; // Show the cursor
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked; // Lock the cursor
            Cursor.visible = false; // Hide the cursor
        }
    }
}
