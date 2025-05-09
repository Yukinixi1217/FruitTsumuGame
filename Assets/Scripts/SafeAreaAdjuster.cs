using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class SafeAreaAdjuster : MonoBehaviour
{
    private void Start()
    {
        RectTransform rt = GetComponent<RectTransform>();
        Debug.Log($"🟢 SafeAreaAdjuster START: {gameObject.name}");

        Rect safeArea = Screen.safeArea;
        Debug.Log($"🟡 SafeArea: position={safeArea.position}, size={safeArea.size}");
        Debug.Log($"🟡 Screen: width={Screen.width}, height={Screen.height}");

        Vector2 anchorMin = safeArea.position;
        Vector2 anchorMax = safeArea.position + safeArea.size;

        if (Screen.width == 0 || Screen.height == 0)
        {
            Debug.LogError("❌ Screen width or height is zero. Cannot calculate safe area anchors.");
            return;
        }

        anchorMin.x /= Screen.width;
        anchorMin.y /= Screen.height;
        anchorMax.x /= Screen.width;
        anchorMax.y /= Screen.height;

        Debug.Log($"🟢 Adjusted anchors on '{gameObject.name}' to Min={anchorMin}, Max={anchorMax}");

        rt.anchorMin = anchorMin;
        rt.anchorMax = anchorMax;
    }
}
