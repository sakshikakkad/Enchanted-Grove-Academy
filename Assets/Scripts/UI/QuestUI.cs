using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MenuToggle))]
public class QuestUI : MonoBehaviour
{
    // Check if Quest UI needs to be activated
    void Start()
    {
        // Get value from Main Manager
        if (MainManager.Instance.unlockedQuest && !MainManager.Instance.wonQuest)
        {
            GetComponent<MenuToggle>().ShowMenu();
        } else
        {
            GetComponent<MenuToggle>().HideMenu();
        }
    }
}
