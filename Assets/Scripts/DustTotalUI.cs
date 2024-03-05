using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class DustTotalUI : MonoBehaviour
{
    private Text text;
    public GameObject timer;

    private void Awake()
    {
        text = GetComponent<Text>();
    }
    public void SetPoints(int points) {
        points = points + 10;
        string total = "" + points;
        text.text = total;
    }
}
