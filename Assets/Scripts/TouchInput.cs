using UnityEngine;

public class TouchInput : MonoBehaviour
{
    private InputSystem_Actions controls;

    public Vector2 TouchPosition { get; private set; }
    public bool IsPressed { get; private set; }

    private void Awake()
    {
        controls = new InputSystem_Actions();
    }

    private void OnEnable()
    {
        controls.Enable();
        controls.Player.TouchPress.started += _ => IsPressed = true;
        controls.Player.TouchPress.canceled += _ => IsPressed = false;
        controls.Player.TouchPosition.performed += ctx => TouchPosition = ctx.ReadValue<Vector2>();
    }

    private void OnDisable()
    {
        controls.Player.TouchPress.started -= _ => IsPressed = true;
        controls.Player.TouchPress.canceled -= _ => IsPressed = false;
        controls.Player.TouchPosition.performed -= ctx => TouchPosition = ctx.ReadValue<Vector2>();
        controls.Disable();
    }
}
