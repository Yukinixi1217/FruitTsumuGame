using UnityEngine;
using UnityEngine.InputSystem;

public class MenuTouchInput : MonoBehaviour
{
    private InputSystem_Actions controls;

    private bool isPressed = false;
    private Vector2 lastTouchPosition;

    public Vector2 TouchPosition => lastTouchPosition;
    public bool IsPressed => isPressed;

    private void Awake()
    {
        controls = new InputSystem_Actions();
    }

    private void OnEnable()
    {
        controls.Enable();

        controls.Player.TouchPress.started += ctx => isPressed = true;
        controls.Player.TouchPress.canceled += ctx => isPressed = false;

        controls.Player.TouchPosition.performed += ctx =>
        {
            lastTouchPosition = ctx.ReadValue<Vector2>();
        };
    }

    private void OnDisable()
    {
        controls.Player.TouchPress.started -= ctx => isPressed = true;
        controls.Player.TouchPress.canceled -= ctx => isPressed = false;
        controls.Player.TouchPosition.performed -= ctx =>
        {
            lastTouchPosition = ctx.ReadValue<Vector2>();
        };

        controls.Disable();
    }
}
