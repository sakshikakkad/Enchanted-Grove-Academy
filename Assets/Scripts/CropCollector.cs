using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CropCollector : MonoBehaviour
{
    public int numpercrop = 4;
    public bool[,] hasCrop = new bool[4, 4];

    public GameObject manager;
    private LevelManager levelManager;
    // indexed using crop id, # of crop

    void Awake()
    {
        levelManager = manager.GetComponent<LevelManager>();
    }

    public void ReceiveCrop(int id) {
        int i = 0;
        while (hasCrop[id, i] == true) {
            i++;
            levelManager.collect(id);
        }
        if (i < numpercrop) {
            hasCrop[id, i] = true;
        }

    }
}
