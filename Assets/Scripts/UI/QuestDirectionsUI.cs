//author: Alina Polyudova
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestDirectionsUI : MonoBehaviour
{
    void Start()
    {
        if (MainManager.Instance.unlockedQuest && !MainManager.Instance.wonQuest) {
            GetComponent<MenuToggle>().ShowMenu();
        } else {
            GetComponent<MenuToggle>().HideMenu();
        }
    }

    void CloseMenu()
    {
        GetComponent<MenuToggle>().HideMenu();
    }
}
