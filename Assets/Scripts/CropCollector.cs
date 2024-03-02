using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CropCollector : MonoBehaviour
{
    public int numpercrop = 4;
    public bool[,] hasCrop = new bool[4, 4];
    // indexed using crop id, # of crop

    public void ReceiveCrop(int id) {
        int i = 0;
        while (hasCrop[id, i] == true) {
            i++;
        }
        if (i < numpercrop) {
            hasCrop[id, i] = true;
        }

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
