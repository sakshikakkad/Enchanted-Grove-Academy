using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class PauseMenuToggle : MonoBehaviour
{

    private CanvasGroup canvasGroup;
    void Awake() {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    void Update() {
        if (Input.GetKeyUp (KeyCode.Escape)) {
            if (canvasGroup.interactable) {
                GetComponent<MenuToggle>().HideMenu();
            } else {
                GetComponent<MenuToggle>().ShowMenu();
            }
        }
    }
}
