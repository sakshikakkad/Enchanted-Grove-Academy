using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Authors: Sakshi
// Alina Polyudova - Update() and Fail() methods
public class QuestManager : MonoBehaviour
{
    //UI Screen GameObjects
    public GameObject youDiedScreen;

    // Quest related vars
    public Terrain spawnTerrain; // SET IN INSPECTOR
    public int spiderCount; // SET IN INSPECTOR
    public GameObject player; // SET IN INSPECTOR
    public GameObject end; // SET IN INSPECTOR
    private ArrayList spiders = new ArrayList();
    private int lives;

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
        
        for (int i = 0; i < spiderCount; i++) {
            AIController spider = ((GameObject)spiders[i]).GetComponent<AIController>();
            if (spider.aiState == AIController.AIState.Attack && spider.playerInAttackRange()) {
                StartCoroutine(PerformActionAfterAnimation(spider));
            }
        }
    }


    private Vector3 randomPos()
    {
        float pos_x = Random.Range(0f, spawnTerrain.terrainData.size.x);
        float pos_z = Random.Range(0f, spawnTerrain.terrainData.size.z);
        float pos_y = spawnTerrain.SampleHeight(new Vector3(pos_x, 0f, pos_z));

        return new Vector3(pos_x, pos_y, pos_z);
    }

     IEnumerator PerformActionAfterAnimation(AIController spider)
    {
        yield return new WaitForSeconds(spider.animator.GetCurrentAnimatorStateInfo(0).length);
        lives--;
        Debug.Log(lives);
    }
}
