using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class MenuToggle : MonoBehaviour
{
    private CanvasGroup canvasGroup;
    void Awake() {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void HideMenu() {
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
        canvasGroup.alpha = 0f;
        Time.timeScale = 1f;
    }

    public void ShowMenu() {
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
        canvasGroup.alpha = 1f;
        Time.timeScale = 0f;
    }
}
