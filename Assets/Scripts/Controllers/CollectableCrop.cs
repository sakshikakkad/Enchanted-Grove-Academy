using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableCrop : MonoBehaviour
{
    public float collectableRange = 20f; // SET IN INSPECTOR
    public int id = 0;
    GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        collectableRange = 30f;
    }

    private void Update()
    {
        // check if player in range
        float dist = Vector3.Distance(this.transform.position, player.transform.position);
        if (dist < collectableRange && player.GetComponent<InputController>().Click)
        {
            CropCollector cc = player.gameObject.GetComponent<CropCollector>();
            if (cc != null)
            {
                Destroy(this.gameObject);
                cc.ReceiveCrop(id);
            }
        }
    }
}
