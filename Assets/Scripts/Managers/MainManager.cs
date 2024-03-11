//authors: 
//Alina Polyudova - implemented winScreen UI pop-up in Start() method
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainManager : MonoBehaviour
{
    public static MainManager Instance;

    public int fairyDustThreshold = 20; // SET IN INSPECTOR (amt when quest unlocks)
    private int _fairyDust = 0;
    public GameObject winScreen;

    // quest stuff
    public bool unlockedQuest = false;
    public bool wonQuest = false;
    private Coroutine unlockCoroutine;
    public GameObject questEntry;

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

        // Subscribe to the sceneLoaded event
        SceneManager.sceneLoaded += OnSceneLoaded;
        // if (Instance != null)
        // {
        //     Destroy(gameObject);
        //     return;
        // }
        // Instance = this;
        // DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        if (questEntry != null) // Check if questEntry exists
        {
            questEntry.GetComponent<Collider>().enabled = false;
            unlockCoroutine = StartCoroutine(CheckUnlockQuest());
        }
        if (wonQuest == true)
        {
            winScreen.GetComponent<MenuToggle>().ShowMenu();
        }
        else
        {
            winScreen.GetComponent<MenuToggle>().HideMenu();
        }
        // // check if we need to unlock the quest
        // questEntry.GetComponent<Collider>().enabled = false;
        // unlockCoroutine = StartCoroutine(CheckUnlockQuest());
        // if (wonQuest == true) {
        //     winScreen.GetComponent<MenuToggle>().ShowMenu();
        // } else {
        //     winScreen.GetComponent<MenuToggle>().HideMenu();
        // }
    }

    private IEnumerator CheckUnlockQuest()
    {
        while (!unlockedQuest && !wonQuest)
        {
            if (_fairyDust >= fairyDustThreshold && questEntry != null)
            {
                unlockedQuest = true;
                questEntry.GetComponent<Collider>().enabled = true;
            }
            yield return null; // wait for next frame
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if(scene.name == "Main") {
            // Attempt to find the questEntry and winScreen GameObjects in the new scene
            questEntry = GameObject.Find("QuestEntry"); // Replace "QuestEntryName" with the actual name of your GameObject in the scene
            winScreen = GameObject.Find("WonQuest"); // Replace "WinScreenName" with the actual name
        }
        // Additional initialization based on the newly assigned GameObjects
        if (questEntry != null)
        {
            // For example, re-check or reinitialize things specific to questEntry
            questEntry.GetComponent<Collider>().enabled = !unlockedQuest;
            if (unlockCoroutine == null && !unlockedQuest && !wonQuest)
            {
                unlockCoroutine = StartCoroutine(CheckUnlockQuest());
            }
        }

        if (winScreen != null && wonQuest)
        {
            winScreen.GetComponent<MenuToggle>().ShowMenu();
        }
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

}
