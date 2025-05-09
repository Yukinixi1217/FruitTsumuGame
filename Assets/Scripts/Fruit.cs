using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Fruit : MonoBehaviour
{
    public string ID;
    public int Level;
    public GameObject SelectSprite;
    public bool IsSelect { get; private set; }

    private Camera mainCamera;

    private InputAction touchPress;
    private InputAction touchPosition;

    private InputAction mousePress;
    private InputAction mousePosition;

    private LevelManager levelManager;

    //public Sprite idleSprite;
    //public Sprite smileSprite;

    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        mainCamera = Camera.main;

        // 入力の設定
        touchPress = new InputAction("TouchPress", binding: "<Touchscreen>/primaryTouch/press");
        touchPosition = new InputAction("TouchPosition", binding: "<Touchscreen>/primaryTouch/position");

        mousePress = new InputAction("MousePress", binding: "<Mouse>/leftButton");
        mousePosition = new InputAction("MousePosition", binding: "<Mouse>/position");

        touchPress.performed += OnPressDown;
        touchPress.canceled += OnPressUp;
        mousePress.performed += OnPressDown;
        mousePress.canceled += OnPressUp;

        touchPress.Enable();
        touchPosition.Enable();
        mousePress.Enable();
        mousePosition.Enable();

        // LevelManager をシーンから取得
        levelManager = FindFirstObjectByType<LevelManager>();

        if (levelManager == null)
        {
            Debug.LogError("LevelManager がシーンに存在しません！");
        }
    }
    // Fruit.cs の Awake または Start の末尾に追加：
    void Start()
    {
        var renderer = GetComponent<SpriteRenderer>();
        if (renderer != null)
        {
            renderer.sortingLayerName = "Default";   // または Fruit 用の Sorting Layer を新設して指定
            renderer.sortingOrder = 0;               // UI よりも手前にしたい場合は小さい値を設定
        }
    }

    private void OnDestroy()
    {
        touchPress.performed -= OnPressDown;
        touchPress.canceled -= OnPressUp;
        mousePress.performed -= OnPressDown;
        mousePress.canceled -= OnPressUp;
    }

    private void OnPressDown(InputAction.CallbackContext context)
    {
        Vector2 inputPos = GetInputPosition();
        RaycastHit2D hit = Physics2D.Raycast(mainCamera.ScreenToWorldPoint(inputPos), Vector2.zero);

        if (hit.collider != null && hit.collider.gameObject == gameObject && levelManager != null)
        {
            levelManager.FruitDown(this);
        }
    }

    private void OnPressUp(InputAction.CallbackContext context)
    {
        if (levelManager != null)
        {
            levelManager.FruitUp();
        }
    }

    private void OnMouseEnter()
    {
        if (levelManager != null)
        {
            levelManager.FruitEnter(this);
        }
    }

    private Vector2 GetInputPosition()
    {
        return (Touchscreen.current != null && Touchscreen.current.primaryTouch.press.isPressed)
            ? touchPosition.ReadValue<Vector2>()
            : mousePosition.ReadValue<Vector2>();
    }

    public void SetIsSelect(bool isSelect)
    {
        IsSelect = isSelect;

        if (SelectSprite != null)
        {
            SelectSprite.SetActive(isSelect);
        }

    }

}
