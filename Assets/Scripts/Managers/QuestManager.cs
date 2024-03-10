using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Author: Sakshi
public class QuestManager : MonoBehaviour
{
    // add UI Screen GameObjects here

    // Quest related vars
    public Terrain spawnTerrain; // SET IN INSPECTOR
    public int spiderCount; // SET IN INSPECTOR
    public GameObject player; // SET IN INSPECTOR
    public GameObject end; // SET IN INSPECTOR
    private ArrayList spiders = new ArrayList();

    // Spider prefabs
    public GameObject blackWidow;
    public GameObject sandSpider;

    // Initialize spiders
    void Start()
    { 
        for (int i = 0; i < (spiderCount / 2); i++)
        {
            spiders.Add(Instantiate(blackWidow, randomPos(), Quaternion.identity));
            spiders.Add(Instantiate(sandSpider, randomPos(), Quaternion.identity));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Implement Fail UI
    private void Fail() { }

    // Implement Win UI
    // TODO: set wonQuest in Main manager to true to prevent player from repeating quest
    private void Win() { }

    private Vector3 randomPos()
    {
        float pos_x = Random.Range(0f, spawnTerrain.terrainData.size.x);
        float pos_z = Random.Range(0f, spawnTerrain.terrainData.size.z);
        float pos_y = spawnTerrain.SampleHeight(new Vector3(pos_x, 0f, pos_z));

        return new Vector3(pos_x, pos_y, pos_z);
    }
}
