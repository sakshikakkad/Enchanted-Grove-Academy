using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DustTotalUI : MonoBehaviour
{
    public TextMeshProUGUI pixieDustText;
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
        UpdatePixieDustDisplay();
    }

    public void UpdatePixieDust(int dustQuantity) {
        float fillAmount = (float)currentPixieDust + dustQuantity / maxPixieDust;
        MainManager.Instance.FairyDust = (int)fillAmount;
        UpdatePixieDustDisplay();
    }

    void UpdatePixieDustDisplay()
    {
        pixieDustText.text = currentPixieDust.ToString();
        pixieDustFillImage.rectTransform.localScale = new Vector3((float)currentPixieDust, 1, 1);
    }
}
