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

    private string[] cropNames = {"yellow", "grey", "orange", "purple"};

    // void Start()
    // {
    //     text = GetComponent<Text>();
    //     canvas = GetComponentInParent<Canvas>();
    // }

    public void UpdateText(List<int> cropIDList) { //cropIDList should be sorted already
        text = GetComponent<Text>();
        canvas = GetComponentInParent<Canvas>();
        int curr = 0;
        int count = 0;
        string ret = "";
        for (int i = 0; i < cropIDList.Count; i++) {
            if (cropIDList[i] != curr) {
                if (count != 0) {
                    ret = ret + cropNames[curr] + ": " + count + "\n";
                } //only add to list if nonzero
                curr = cropIDList[i];
                count = 1;
            } else {
                count++;
            }
        }
        ret = ret + cropNames[curr] + ": " + count + "\n";
        text.text = ret;
    }
}
