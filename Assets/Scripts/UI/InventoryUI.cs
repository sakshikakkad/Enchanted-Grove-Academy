using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{

    public GameObject player;
    private Text text;
    private Canvas canvas;
    //names of the crops - could change to pictures later?
    private string[] cropNames = {"daikon", "beet", "radish", "parsnip"};

    private void Awake()
    {
        text = GetComponent<Text>();

        canvas = GetComponentInParent<Canvas>();
    }
    // Update is called once per frame
    void Update()
    {
        text.text = GetInventory();
    }

    private string GetInventory() {
        // List<string> inventoryList = new List<string>();
        string inventoryList = "";
        CropCollector cc = player.GetComponent<CropCollector>();
        for (int i = 0; i < cropNames.Length; i++) {
            inventoryList = inventoryList + cropNames[i] + ": " + countCrop(cc, i);
            if (i != cropNames.Length - 1) {
                inventoryList = inventoryList + "          "; //10 spaces to make spacing better
            }
        }
        return inventoryList;
    } //getInventory


    //counts the number of one crop the player has
    private int countCrop(CropCollector cc, int id) {
        int count = 0;
        for (int i = 0; i < cc.GetNumPerCrop(); i++) {
            if (cc.hasCrop[id, i] == false) {
                return count;
            }
            count++;
        }
        return count;
    } //countCrop
} //class