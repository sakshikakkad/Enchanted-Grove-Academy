using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Authors: Sakshi
public class QuestManager : MonoBehaviour
{
    //UI Screen GameObjects
    public GameObject youDiedScreen;
    public GameObject startMenu;

    // Quest related vars
    public int spiderCount; // SET IN INSPECTOR
    public GameObject player; // SET IN INSPECTOR
    public GameObject spawnArea; // SET IN INSPECTOR
    private ArrayList spiders = new ArrayList();

    // Spider prefabs
    public GameObject blackWidow;
    public GameObject sandSpider;
    
    //Audio
    public AudioSource failAudioSource;
    public AudioClip failClip;

    // Initialize spiders
    void Start()
    {
        startMenu.GetComponent<MenuToggle>().ShowMenu();

        for (int i = 0; i < (spiderCount / 2); i++)
        {
            spiders.Add(Instantiate(blackWidow, randomPos(), Quaternion.identity));
            spiders.Add(Instantiate(sandSpider, randomPos(), Quaternion.identity));
        }
    }


    void Update()
    {
        // fail UI
        if (player.GetComponent<LifeController>().lifeCount <= 0) {
            failAudioSource.PlayOneShot(failClip);
            youDiedScreen.GetComponent<MenuToggle>().ShowMenu();
        } else {
            youDiedScreen.GetComponent<MenuToggle>().HideMenu();
        }
    }

    private Vector3 randomPos()
    {
        Vector3 spawnPos = spawnArea.transform.position;
        Vector3 spawnScale = spawnArea.transform.localScale;

        // calculate the bounds of where spiders can spawn
        float minX = spawnPos.x - spawnScale.x / 2;
        float maxX = spawnPos.x + spawnScale.x / 2;
        float minZ = spawnPos.z - spawnScale.z / 2;
        float maxZ = spawnPos.z + spawnScale.z / 2;

        // generate a random position within spawn bounds
        float pos_x = Random.Range(minX, maxX);
        float pos_z = Random.Range(minZ, maxZ);
        float pos_y = Terrain.activeTerrain.SampleHeight(new Vector3(pos_x, 0f, pos_z));
        return new Vector3(pos_x, pos_y, pos_z);
    }
}
