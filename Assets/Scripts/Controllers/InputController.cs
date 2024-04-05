using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Author: Sakshi Kakkad (entire script)
public class InputController : MonoBehaviour
{

    // input filters
    private float forwardFiltered = 0f;
    private float turnFiltered = 0f;
    private float flyFiltered = 0f;

    public float forwardFilter = 5f;
    public float turnFilter = 5f;
    public float flyFilter = 5f;

    // limits (CHANGE IN INSPECTOR)
    public float speedLimit = 10f;
    public float flySpeedLimit = 5f;

    // getters and setters
    public float Forward
    {
        get;
        private set;
    }

    public float Turn
    {
        get;
        private set;
    }

    public float Fly
    {
        get;
        private set;
    }

    public bool Click
    {
        get;
        private set;
    }

    // Update is called once per frame
    void Update()
    {
        // GetAxisRaw() for input smoothing/filtering for float outputs
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        float f = Input.GetAxisRaw("Jump");

        // filter for forward/turn/fly
        // forwardFiltered is the outputted forward value
        // v is either 0 or 1 (-1 if backwards but we don't have that movement)
        // forward filter affects the rate of interpolation
        forwardFiltered = Mathf.Clamp(Mathf.Lerp(forwardFiltered, v * speedLimit,
            Time.deltaTime * forwardFilter), 0f, speedLimit);

        turnFiltered = Mathf.Lerp(turnFiltered, h, Time.deltaTime * turnFilter);

        flyFiltered = Mathf.Clamp(Mathf.Lerp(flyFiltered, f * flySpeedLimit,
            Time.deltaTime * flyFilter), 0f, flySpeedLimit);

        // outputs
        Forward = forwardFiltered;
        Turn = turnFiltered;
        Fly = flyFiltered;
        Click = Input.GetButtonDown("Fire1");

        // Debug
        //Debug.Log("forward input: " + forwardFiltered);
    }
}
