using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainManager : MonoBehaviour
{
    public static MainManager Instance;

    public int fairyDustThreshold = 20; // SET IN INSPECTOR (amt when quest unlocks)
    [SerializeField] private int _fairyDust = 0;

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
    }

    private void Start()
    {
        // check if we need to unlock the quest
        questEntry.GetComponent<Collider>().enabled = false;
        unlockCoroutine = StartCoroutine(CheckUnlockQuest());
    }

    private IEnumerator CheckUnlockQuest()
    {
        while (!unlockedQuest && !wonQuest)
        {
            if (_fairyDust >= fairyDustThreshold)
            {
                unlockedQuest = true;
                questEntry.GetComponent<Collider>().enabled = true;
            }
            yield return null; // wait for next frame
        }
    }
}
