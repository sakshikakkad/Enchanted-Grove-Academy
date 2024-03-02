using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CropCollector : MonoBehaviour
{
    public bool[] hasCrop1 = {false, false, false, false};
    public void ReceiveBall() {
        int i = 0;
        while (hasCrop1[0] == true) {
            i++;
        }
        if (i < hasCrop1.Length) {
            hasCrop1[i] = true;
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
