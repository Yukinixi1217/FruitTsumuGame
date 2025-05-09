using UnityEngine;

public class SafeAreaInitializer : MonoBehaviour
{
    void Start()
    {
        if (!Application.isPlaying) return;

        var rects = GameObject.FindObjectsOfType<RectTransform>(includeInactive: true);

        foreach (RectTransform rt in rects)
        {
            string lowerName = rt.name.ToLowerInvariant();
            bool isFullStretch = rt.anchorMin == Vector2.zero && rt.anchorMax == Vector2.one;
            bool nameMatches = lowerName == "backgroundpanel"; // ← 完全一致！

            if (isFullStretch && nameMatches && rt.GetComponent<SafeAreaAdjuster>() == null)
            {
                rt.gameObject.AddComponent<SafeAreaAdjuster>();
                Debug.Log($"✅ SafeAreaAdjuster SUCCESSFULLY ATTACHED to GameObject: '{rt.name}' in Scene: {gameObject.scene.name}");
            }

            if (rt.GetComponent<SafeAreaAdjuster>() != null)
            {
                Debug.Log($"📌 Confirmed SafeAreaAdjuster present on: {rt.name}");
            }
        }
    }
}
