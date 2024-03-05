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
    void OnTriggerStay(Collider c)
    {
        if (c.attachedRigidbody != null)
        {
            if (Input.GetMouseButtonDown(0)) //left click
            {
                CropCollector cc = c.attachedRigidbody.gameObject.GetComponent<CropCollector>();
                if (cc != null) {
                    Destroy(this.gameObject);
                    cc.ReceiveCrop(id);
                }
            }
        }
    }
}
