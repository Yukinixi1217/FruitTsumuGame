using UnityEngine;

public class SafeAreaInitializer : MonoBehaviour
{
    [SerializeField] private string[] targetNames = new string[] { "BackgroundPanel", "SafeAreaRoot" }; // シーンにより異なる名前に対応

    void Start()
    {
        if (!Application.isPlaying) return;

        var rects = GameObject.FindObjectsOfType<RectTransform>(includeInactive: true);

        foreach (RectTransform rt in rects)
        {
            string lowerName = rt.name.ToLowerInvariant();
            bool isFullStretch = rt.anchorMin == Vector2.zero && rt.anchorMax == Vector2.one;
            bool nameMatches = false;

            foreach (var name in targetNames)
            {
                if (lowerName == name.ToLowerInvariant())
                {
                    nameMatches = true;
                    break;
                }
            }

            if (isFullStretch && nameMatches && rt.GetComponent<SafeAreaWithSwipeQuit>() == null)
            {
                rt.gameObject.AddComponent<SafeAreaWithSwipeQuit>();
                Debug.Log($"✅ SafeAreaWithSwipeQuit attached to: '{rt.name}' in Scene: {gameObject.scene.name}");
            }

            if (rt.GetComponent<SafeAreaWithSwipeQuit>() != null)
            {
                Debug.Log($"📌 Confirmed SafeAreaWithSwipeQuit present on: {rt.name}");
            }
        }
    }
}
