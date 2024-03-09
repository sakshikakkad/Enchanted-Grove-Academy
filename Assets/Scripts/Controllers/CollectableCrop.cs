using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableCrop : MonoBehaviour
{
    public int id = 0;
    //crops sorted by id, each manually set
    
    // // Start is called before the first frame update
    // void Start()
    // {
        
    // }

    // // Update is called once per frame
    // void Update()
    // {
        
    // }
    void OnCollisionStay(Collision c)
    {
        if (c.gameObject.CompareTag("Player"))
        {
            if (c.gameObject.GetComponent<InputController>().Click) //left click
            {
                Debug.Log("crop should delete");
                CropCollector cc = c.gameObject.GetComponent<CropCollector>();
                if (cc != null) {
                    Destroy(this.gameObject);
                    cc.ReceiveCrop(id);
                }
            }
        }
    }
}
