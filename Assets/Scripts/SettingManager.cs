using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingManager : MonoBehaviour
{
    public GameObject HomeSettingCanvas;
    public CanvasGroup titleScreenGroup; // �� �w�i�� CanvasGroup�iUI_TitleScreenPanel�j

    public void CloseSettingPanel()
    {
        HomeSettingCanvas.SetActive(false);

        // �w�i�𖾂邭����i���ɖ߂��j
        if (titleScreenGroup != null)
        {
            titleScreenGroup.alpha = 1;
            titleScreenGroup.blocksRaycasts = true;
            titleScreenGroup.interactable = true;
        }
    }
}
