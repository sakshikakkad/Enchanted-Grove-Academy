using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CropCollector : MonoBehaviour {

    public bool[,] hasCrop;

    public GameObject manager;
    private GardenManager gardenManager;
    // indexed using crop id, # of crop

    void Start() {
        gardenManager = manager.GetComponent<GardenManager>();
        // indexing: # of different crops, # of that crop
        hasCrop = new bool[4, gardenManager.numPerCrop];
    }

    public void ReceiveCrop(int id) {
        int i = 0;
        while (hasCrop[id, i] == true) {
            i++;
        }
        gardenManager.collect(id);
        if (i < gardenManager.numPerCrop) {
            hasCrop[id, i] = true;
        }

    }

    public int GetNumPerCrop() {
        return gardenManager.numPerCrop;
    }
}
