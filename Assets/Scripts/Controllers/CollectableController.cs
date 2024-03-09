using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableCrop1 : MonoBehaviour
{
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
                Destroy(this.gameObject);
            }
        }
    }
}
