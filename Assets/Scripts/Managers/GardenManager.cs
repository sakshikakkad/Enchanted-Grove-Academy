using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GardenManager : MonoBehaviour
{
    
    public int level = 1;
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
        level = MainManager.Instance.gardenLevel;
        cropIDList = new List<int>();
        for (int i = 0; i < level * 3; i++) {
            int gen = Random.Range(0, 4); // generates integer in [0, 3]
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
            MainManager.Instance.fairyDust += pointTotalText.GetComponent<DustTotalUI>().SetPoints(timer.time);
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
        MainManager.Instance.gardenLevel = level;
        // PixieDustSystem pixieDustSystem = pixieDustBar.GetComponent<PixieDustSystem>();
        // if (pixieDustSystem != null)
        // {
        //     pixieDustSystem.IncreasePixieDust();
        // }
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
