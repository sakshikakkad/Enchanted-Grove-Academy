using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameWinControl : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject winScreen;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            winScreen.GetComponent<MenuToggle>().ShowMenu();
            MainManager.Instance.wonQuest = true;
        }
    }
}
