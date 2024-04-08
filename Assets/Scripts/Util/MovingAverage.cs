using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class MovingAverage : MonoBehaviour
{
    public int length = 30;
    private int count;

    private float average
    {
        get;
        set;
    }

    // Start is called before the first frame update
    void Start()
    {
        count = 0;
    }

    // Update is called once per frame
    public float calcMovingAvg(float newVal)
    {
        count++;

        // calculate average
        if (count > length)
        {
            average = average + (newVal - average) / (length + 1);
        }
        // calculate first value of average
        else
        {
            average += newVal;
            if (count == length)
            {
                average = average / length;
            }
        }

        return average;
    }
}
