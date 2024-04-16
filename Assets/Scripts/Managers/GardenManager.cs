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

    public int numPerCrop = 5;
    public int numTypeCrops = 6;
    public List<int> cropIDList;

    public GameObject timerText;
    private TimerUI timer;
    public GameObject cropsUI;

    public GameObject pointTotalText;

    public GameObject inventory;
    public AudioSource collectSoundAudioSource;
    public AudioClip collectSoundClip;
    public AudioSource victoryAudioSource;
    public AudioClip victoryClip;
    
    public AudioSource failAudioSource;
    public AudioClip failClip;
    // public GameObject pixieDustBar;
    
    
    // Start is called before the first frame update
    void Start()
    {
        cropIDList = new List<int>();
        int maxID = 3;
        if (MainManager.Instance.GardenLevel >= 2) { // only make player get tree fruit after first level
            maxID = 5;
        }
        for (int i = 0; i < MainManager.Instance.GardenLevel * 3; i++) {
            int gen = Random.Range(0, maxID + 1); // generates integer in [0, maxID]
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
        collectSoundAudioSource.PlayOneShot(collectSoundClip);
        int i = cropIDList.IndexOf(id);
        if (i >= 0) {
            cropIDList.Remove(id);
            timer.IncreaseTime();
        }
        if (cropIDList.Count == 0) {
            Win();
        }
    }

    public void Fail() {
        failAudioSource.PlayOneShot(failClip);
        timerText.SetActive(false);
        failScreen.GetComponent<MenuToggle>().ShowMenu();
    }

    public void Win() {
        victoryAudioSource.PlayOneShot(victoryClip);
        timerText.SetActive(false);
        int earnedPoints = CalculatePoints(timer.time);
        pointTotalText.GetComponent<Text>().text = earnedPoints.ToString();
        winScreen.GetComponent<MenuToggle>().ShowMenu();
        MainManager.Instance.GardenLevel += 1;
        MainManager.Instance.FairyDust += earnedPoints;
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

    private int CalculatePoints(int timeLeft) {
        float result = Random.Range(.9f, 1.1f) * timeLeft;
        result = Mathf.Clamp(result, 25, 34); //require around 3/4 tries in the garden
        int randomOffset = Random.Range(-4, 5); //generate integer between[-4, 4]
        result = result + randomOffset; //minimum 3 tries in the garden, max 5
        return (int)result;
    }

}
