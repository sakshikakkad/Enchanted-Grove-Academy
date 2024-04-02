//authors: 
//Alina Polyudova - implemented winScreen UI pop-up in Start() method
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainManager : MonoBehaviour
{
    public static MainManager Instance;

    public int fairyDustThreshold = 20; // SET IN INSPECTOR (amt when quest unlocks)
    private int _fairyDust = 0;

    // quest stuff
    public bool unlockedQuest = false;
    public bool wonQuest = false;
    private Coroutine unlockCoroutine;

    // garden stuff
    private int _gardenLevel = 1;

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
    }

    private void Start()
    {
        // check if we need to unlock the quest
        unlockCoroutine = StartCoroutine(CheckUnlockQuest());
    }

    private IEnumerator CheckUnlockQuest()
    {
        while (!unlockedQuest && !wonQuest)
        {
            if (_fairyDust >= fairyDustThreshold)
            {
                unlockedQuest = true;
                QuestLoader.Activate();
            }
            yield return null; // wait for next frame
        }
    }
}
