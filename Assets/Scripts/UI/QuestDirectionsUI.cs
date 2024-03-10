//author: Alina Polyudova
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestDirectionsUI : MonoBehaviour
{
    public GameObject questDirections;
    void Start()
    {
        if (MainManager.Instance.fairyDust >= 20) {
            questDirections.GetComponent<MenuToggle>().ShowMenu();
        } else {
            questDirections.GetComponent<MenuToggle>().HideMenu();
        }
    }

    public void CloseMenu()
    {
        questDirections.GetComponent<MenuToggle>().HideMenu();
    }
}
