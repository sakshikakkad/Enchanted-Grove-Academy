//authors: 
// Sakshi Kakkad 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MainManager : MonoBehaviour
{
    public static MainManager Instance;

    public int fairyDustThreshold = 100; // SET IN INSPECTOR (amt when quest unlocks)
    private int _fairyDust = 0;

    // quest stuff
    public bool unlockedQuest = false;
    public bool wonQuest = false;
    private Coroutine unlockQuestCoroutine;

    // garden stuff
    private int _gardenLevel = 1;

    // ui stuff
    public GameObject questPanel;
    public GameObject introPanel;

    public int GardenLevel
    {
        get { return _gardenLevel; }
        set { _gardenLevel = value; }
    }

    public int FairyDust { 
        get { return _fairyDust; } 
        set { _fairyDust = value; }
    }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        introPanel.SetActive(true);
    }

    private void Start()
    {
        // check if we need to unlock the quest
        unlockQuestCoroutine = StartCoroutine(CheckUnlockQuest());
    }

    private IEnumerator CheckUnlockQuest()
    {
        while (!unlockedQuest && !wonQuest)
        {
            if (_fairyDust >= fairyDustThreshold)
            {
                unlockedQuest = true;
                questPanel.SetActive(true);
                QuestLoader.Activate();
            }
            yield return null; // wait for next frame
        }
    }
}
