using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainManager : MonoBehaviour
{
    public static MainManager Instance;

    private int _gardenLevel = 1;
    private bool wonQuest = false;
    private int _fairyDust = 0;

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
}
