using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class TimerUI : MonoBehaviour
{
    public int time = 120;
    //time in seconds
    private int count = 0;

    public GameObject manager;
    private LevelManager levelManager;

    private Text text;

    public void ResetTime() {
        time = 120;
    }

    //FixedUpdate called 50 times every second
    void FixedUpdate() {
        count++;
        if (count >= 50) {
            count = 0;
            time--;
            text.text = "" + time;
        }
        if (time <= 0) {
            levelManager.Fail();
        }
    }
    
    public void IncreaseTime() {
        time += 20;
    }

    void Awake()
    {
        text = GetComponent<Text>();
        levelManager = manager.GetComponent<LevelManager>();
    }

}
