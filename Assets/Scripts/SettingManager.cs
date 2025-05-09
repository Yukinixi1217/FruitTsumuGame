using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingManager : MonoBehaviour
{
    public GameObject HomeSettingCanvas;
    public CanvasGroup titleScreenGroup; // © ”wŒi‚Ì CanvasGroupiUI_TitleScreenPanelj

    public void CloseSettingPanel()
    {
        HomeSettingCanvas.SetActive(false);

        // ”wŒi‚ğ–¾‚é‚­‚·‚éiŒ³‚É–ß‚·j
        if (titleScreenGroup != null)
        {
            titleScreenGroup.alpha = 1;
            titleScreenGroup.blocksRaycasts = true;
            titleScreenGroup.interactable = true;
        }
    }
}
