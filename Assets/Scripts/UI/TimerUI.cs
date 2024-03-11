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
    private GardenManager gardenManager;

    private Text text;

    public void ResetTime() {
        time = 200;
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
            gardenManager.Fail();
        }
    }
    
    public void IncreaseTime() {
        time += 20;
    }

    void Awake()
    {
        text = GetComponent<Text>();
        gardenManager = manager.GetComponent<GardenManager>();
    }

}
