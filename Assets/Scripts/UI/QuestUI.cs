using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestUI : MonoBehaviour
{
    public MainManager MainManager;
    private bool questUIdisplayed = false;

    private void Start()
    {
        MainManager = GameObject.Find("MainManager").GetComponent<MainManager>();
    }

    // Check if Quest UI needs to be activated
    void Update()
    {
        // Get value from Main Manager
        if (MainManager.unlockedQuest == true && !questUIdisplayed)
        {

        }
    }
}
