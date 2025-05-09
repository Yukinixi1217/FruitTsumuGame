using UnityEngine;
using UnityEngine.InputSystem;

public class TouchInput : MonoBehaviour
{
    private InputSystem_Actions controls;  // Input Action �̎Q��

    void Awake()
    {
        controls = new InputSystem_Actions(); // Input Action �𐶐�
    }

    void OnEnable()
    {
        controls.Enable(); // �A�N�V������L����
        controls.Player.TouchPress.performed += ctx => OnTouchPress(ctx);
    }

    void OnDisable()
    {
        controls.Disable(); // �A�N�V�����𖳌���
        controls.Player.TouchPress.performed -= ctx => OnTouchPress(ctx);
    }

    void Update()
    {
        if (controls.Player.TouchPress.WasPressedThisFrame()) // �^�b�`���o
        {
            //Debug.Log("�^�b�`���ꂽ�I");
        }
    }
    private void OnTouchPress(InputAction.CallbackContext context)
    {
        //Debug.Log("�^�b�`���͂܂��̓N���b�N�����m");
        Vector2 position = controls.Player.TouchPosition.ReadValue<Vector2>();
        //Debug.Log($"�^�b�`�ʒu: {position}");
    }
}
