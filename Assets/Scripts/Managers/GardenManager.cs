using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Author: Melissa Leng
//Author: Leila Baniassad

public class GardenManager : MonoBehaviour
{
    public GameObject failScreen;
    public GameObject winScreen;
    public GameObject startScreen;

    public int numPerCrop = 4;
    private int numTypeCrops = 4;
    public List<int> cropIDList;

    public GameObject timerText;
    private TimerUI timer;
    public GameObject cropsUI;

    public GameObject pointTotalText;

    public GameObject inventory;
    // public GameObject pixieDustBar;
    
    
    // Start is called before the first frame update
    void Start()
    {
        cropIDList = new List<int>();
        for (int i = 0; i < MainManager.Instance.GardenLevel * 3; i++) {
            int gen = Random.Range(0, 3); // generates integer in [0, 3]
            if (Count(cropIDList, gen) < numPerCrop) {
                cropIDList.Add(gen);
            } else {
                i--; //generate other crop
            }
        }
        cropIDList.Sort();
        int[] listPerCrop = SortCrops(cropIDList);
        cropsUI.GetComponent<CropsUI>().UpdateText(listPerCrop);
        inventory.GetComponent<InventoryUI>().SetCropList(listPerCrop);

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
            pointTotalText.GetComponent<Text>().text = "5";
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
        MainManager.Instance.FairyDust += 5;
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

    private int[] SortCrops(List<int> cropIDList) { //cropIDList should be sorted already
        int[] result = new int[numTypeCrops];
        for (int i = 0; i < numTypeCrops; i++) {
            result[i] = Count(cropIDList, i);
        }
        return result;
    }

}
