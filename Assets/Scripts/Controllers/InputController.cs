using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;

// Author: Sakshi Kakkad (entire script)
public class InputController : MonoBehaviour
{

    // input filters
    private float forwardFiltered = 0f;
    private float turnFiltered = 0f;
    private float flyFiltered = 0f;

    public float forwardFilter = 5f;
    public float turnFilter = 2f;
    public float flyFilter = 5f;

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
        float f = System.Convert.ToSingle(Input.GetKey(KeyCode.Space));

        // filter for forward/turn/fly
        // forwardFiltered is the outputted forward value
        // v is either 0 or 1 (-1 if backwards but we don't have that movement)
        // forward filter affects the rate of interpolation
        forwardFiltered = Mathf.Lerp(forwardFiltered, v * forwardFilter,
            Time.deltaTime * forwardFilter);

        turnFiltered = Mathf.Lerp(turnFiltered, h * turnFilter, Time.deltaTime * turnFilter);

        flyFiltered = Mathf.Lerp(flyFiltered, f * flyFilter,
            Time.deltaTime * flyFilter);

        // outputs
        Forward = forwardFiltered;
        Turn = turnFiltered;
        Fly = flyFiltered;
        Click = Input.GetButtonDown("Fire1");

        // Debug
        //Debug.Log("forward input: " + Forward);
        //Debug.Log("fly input: " + Fly);
    }
}
