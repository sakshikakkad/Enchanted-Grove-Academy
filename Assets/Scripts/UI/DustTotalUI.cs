using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DustTotalUI : MonoBehaviour
{
    // private Text pixieDustText;

    public GameObject fillBox;
    private Image pixieDustFillImage;
    public int maxPixieDust = 20;
    public int currentPixieDust = 0; 
    void Awake () 
    { 
        pixieDustFillImage = fillBox.GetComponent<Image>();
        pixieDustFillImage.type = Image.Type.Filled;
        pixieDustFillImage.fillMethod = Image.FillMethod.Horizontal;
        pixieDustFillImage.fillAmount = 0;
        currentPixieDust = MainManager.Instance.FairyDust; 
        UpdatePixieDustDisplay();
    }
    
    void Update() {
        pixieDustFillImage.fillAmount = (float)MainManager.Instance.FairyDust/(float)maxPixieDust;
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
        // pixieDustText.text = currentPixieDust.ToString();
        pixieDustFillImage.fillAmount = (float)currentPixieDust / (float)maxPixieDust;
        // pixieDustFillImage.rectTransform.localScale = new Vector3((float)currentPixieDust, 1, 1);
    }
}
