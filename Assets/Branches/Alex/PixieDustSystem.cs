using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PixieDustSystem : MonoBehaviour
{
    public static PixieDustSystem Instance;
    public int maxPixieDust;
    public float updatedPixieDust;
    public float pixieDustIncrement = 10f;

    public Image pixieDustBar;
    // Start is called before the first frame update
    void Awake()
    {
        maxPixieDust = 50;
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        pixieDustBar.fillAmount = updatedPixieDust / maxPixieDust;
    }

    // void Awake()
    // {
    //     Instance = this;
    //     DontDestroyOnLoad(gameObject);
    // }

    public void IncreasePixieDust()
    {
        updatedPixieDust += pixieDustIncrement;
    }
}
