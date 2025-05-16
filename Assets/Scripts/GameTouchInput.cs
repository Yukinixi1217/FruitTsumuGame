using UnityEngine;
using UnityEngine.InputSystem;

public class GameTouchInput : MonoBehaviour
{
    private InputSystem_Actions inputActions;

    private void OnEnable()
    {
        inputActions = new InputSystem_Actions();
        inputActions.Player.Enable();
        Debug.Log("🟢 TouchInput_GameScene enabled");
    }

    private void OnDisable()
    {
        inputActions.Player.Disable();
        Debug.Log("🔴 TouchInput_GameScene disabled");
    }

    private void Update()
    {
#if UNITY_EDITOR || UNITY_ANDROID || UNITY_IOS
        if (inputActions == null) return;

        Vector2 touchPosition = inputActions.Player.TouchPosition.ReadValue<Vector2>();
        bool isPressed = inputActions.Player.TouchPress.ReadValue<float>() > 0.5f;

        if (isPressed)
        {
            Debug.Log($"👆 Touch detected at position: {touchPosition}");
            // タップに対する処理はここに追加
        }
#endif
    }
}
