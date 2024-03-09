using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GardenManager : MonoBehaviour
{
    public int level = 1;
    public GameObject failScreen;
    public GameObject winScreen;

    public List<int> cropIDList;

    public GameObject timerText;
    private TimerUI timer;

    public GameObject pointTotalText;
    // public GameObject pixieDustBar;
    
    
    // Start is called before the first frame update
    void Start()
    {
        cropIDList = new List<int>();
        Random r = new Random();
        for (int i = 0; i < level * 3; i++) {
            cropIDList.Add(r.Next(0, 4)); // generates integer in [0, 3]
        }
        timer = timerText.GetComponent<TimerUI>();
    }

    public void collect(int id) {
        int i = cropIDList.IndexOf(id);
        if (i >= 0) {
            cropIDList.Remove(id);
            timer.IncreaseTime();
        }
        if (cropIDList.Count == 0) {
            Win();
            pointTotalText.GetComponent<DustTotalUI>().SetPoints(timer.time);
        }
    }

    public void Fail() {
        timerText.SetActive(false);
        failScreen.GetComponent<MenuToggle>().ShowMenu();
    }

    public void Win() {
        timerText.SetActive(false);
        winScreen.GetComponent<MenuToggle>().ShowMenu();
        level++;
        // PixieDustSystem pixieDustSystem = pixieDustBar.GetComponent<PixieDustSystem>();
        // if (pixieDustSystem != null)
        // {
        //     pixieDustSystem.IncreasePixieDust();
        // }
    }
}
