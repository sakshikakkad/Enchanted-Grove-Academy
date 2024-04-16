using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class CropsUI : MonoBehaviour
{
    private Text text;
    private Canvas canvas;

    private string[] cropNames = {"Pumpkin", "Carrot", "Turnip", "Eggplant", "Apple", "Pear"};

    // void Start()
    // {
    //     text = GetComponent<Text>();
    //     canvas = GetComponentInParent<Canvas>();
    // }

    public void UpdateText(int[] listPerCrop) { //cropIDList should be sorted already
        text = GetComponent<Text>();
        canvas = GetComponentInParent<Canvas>();
        string ret = "";

        for (int i = 0; i < cropNames.Length; i++) {
            if (listPerCrop[i] != 0) {
                ret = ret + cropNames[i] + ": " + listPerCrop[i] + "\n";
            }
        }
        text.text = ret;
    }
}
