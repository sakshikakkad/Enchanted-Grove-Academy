using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Author: Melissa Leng
//Author: Leila Baniassad

public class GardenManager : MonoBehaviour
{
    public GameObject failScreen;
    public GameObject winScreen;
    public GameObject startScreen;

    public int numPerCrop = 4;
    public List<int> cropIDList;

    public GameObject timerText;
    private TimerUI timer;
    public GameObject cropsUI;

    public GameObject pointTotalText;
    // public GameObject pixieDustBar;
    
    
    // Start is called before the first frame update
    void Start()
    {
        cropIDList = new List<int>();
        for (int i = 0; i < MainManager.Instance.GardenLevel * 3; i++) {
            int gen = Random.Range(0, 3); // generates integer in [0, 2] CHANGE BACK to (0,4) WHEN WE HAVE RESOLVED THE FLYING ISSUE
            if (Count(cropIDList, gen) < numPerCrop) {
                cropIDList.Add(gen);
            } else {
                i--; //generate other crop
            }
        }
        cropIDList.Sort();
        cropsUI.GetComponent<CropsUI>().UpdateText(cropIDList);
        startScreen.GetComponent<MenuToggle>().ShowMenu();
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
            //GetComponent<DustTotalUI>().UpdatePixieDust(10);
            MainManager.Instance.FairyDust += pointTotalText.GetComponent<DustTotalUI>().SetPoints(timer.time);
        }
    }

    public void Fail() {
        timerText.SetActive(false);
        failScreen.GetComponent<MenuToggle>().ShowMenu();
    }

    public void Win() {
        timerText.SetActive(false);
        winScreen.GetComponent<MenuToggle>().ShowMenu();
        MainManager.Instance.GardenLevel += 1;
    }

    int Count(List<int> list, int search) {
        int count = 0;
        foreach (int i in list) {
            if (i == search) {
                count++;
            }
        }
        return count;
    }
}
