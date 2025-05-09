using UnityEngine;
using UnityEngine.InputSystem;

public class TouchInput : MonoBehaviour
{
    private InputSystem_Actions controls;  // Input Action の参照

    void Awake()
    {
        controls = new InputSystem_Actions(); // Input Action を生成
    }

    void OnEnable()
    {
        controls.Enable(); // アクションを有効化
        controls.Player.TouchPress.performed += ctx => OnTouchPress(ctx);
    }

    void OnDisable()
    {
        controls.Disable(); // アクションを無効化
        controls.Player.TouchPress.performed -= ctx => OnTouchPress(ctx);
    }

    void Update()
    {
        if (controls.Player.TouchPress.WasPressedThisFrame()) // タッチ検出
        {
            //Debug.Log("タッチされた！");
        }
    }
    private void OnTouchPress(InputAction.CallbackContext context)
    {
        //Debug.Log("タッチ入力またはクリックを検知");
        Vector2 position = controls.Player.TouchPosition.ReadValue<Vector2>();
        //Debug.Log($"タッチ位置: {position}");
    }
}
