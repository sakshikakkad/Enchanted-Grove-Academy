using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinManager : MonoBehaviour
{
    public GameObject Intro;
    void Start()
    {
        Intro.GetComponent<MenuToggle>().ShowMenu();
    }

}
