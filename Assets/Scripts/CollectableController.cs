using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableController : MonoBehaviour
{
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
