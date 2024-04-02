using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DustTotalUI : MonoBehaviour
{
    private Text pixieDustText;
    public Image pixieDustFillImage;
    public int maxPixieDust = 200;
    public int currentPixieDust; 
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Start () 
    { 
        currentPixieDust = MainManager.Instance.FairyDust; 
        pixieDustText = GetComponent<Text>();
        // pixieDustText.text = 0;
        UpdatePixieDustDisplay();
    }

    public void UpdatePixieDust(int dustQuantity) {
        MainManager.Instance.FairyDust += dustQuantity;
        //float fillAmount = (float)currentPixieDust + dustQuantity / maxPixieDust;
        // MainManager.Instance.FairyDust = (int)fillAmount;
        currentPixieDust = MainManager.Instance.FairyDust;
        UpdatePixieDustDisplay();
    }

    void UpdatePixieDustDisplay()
    {
        pixieDustText.text = currentPixieDust.ToString();
        // pixieDustFillImage.rectTransform.localScale = new Vector3((float)currentPixieDust, 1, 1);
    }
}
