using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Authors: Sakshi
public class QuestManager : MonoBehaviour
{
    //UI Screen GameObjects
    public GameObject youDiedScreen;

    // Quest related vars
    public int spiderCount; // SET IN INSPECTOR
    public GameObject player; // SET IN INSPECTOR
    public GameObject spawnArea; // SET IN INSPECTOR
    private ArrayList spiders = new ArrayList();
    public int lives;

    // UI elements for each life bar
    public GameObject life1;
    public GameObject life2;
    public GameObject life3;

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
        lives = 3;
    }


    void Update()
    {
        //fail UI
        if (lives <= 0) {
            youDiedScreen.GetComponent<MenuToggle>().ShowMenu();
        } else {
            youDiedScreen.GetComponent<MenuToggle>().HideMenu();
        }

        
        // player lives calculations
        for (int i = 0; i < spiderCount; i++)
        {
            AIController spiderController = ((GameObject)spiders[i]).GetComponent<AIController>();
            if (spiderController.hitPlayer)
            {
                lives--;
                UpdateLifeUI();
                spiderController.hitPlayer = false;
            }
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
        Debug.Log("x = " + pos_x + " y = " + pos_y + " z = " + pos_z);
        return new Vector3(pos_x, pos_y, pos_z);
    }

     IEnumerator PerformActionAfterAnimation(AIController spider)
    {
        yield return new WaitForSeconds(spider.animator.GetCurrentAnimatorStateInfo(0).length);
        lives--;
        UpdateLifeUI();
    }
    
    private void UpdateLifeUI() {
        if (lives == 2) {
            life3.SetActive(false);
        } else if (lives == 1) {
            life3.SetActive(false);
            life2.SetActive(false);
        } else if (lives == 0) {
            life3.SetActive(false);
            life2.SetActive(false);
            life1.SetActive(false);
        }
    }
}
