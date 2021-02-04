using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectItem : MonoBehaviour
{
    public GameObject popUp;
    public bool isOnTrigger = false;
    public GameManager manager;
    public int minigameID=0;
    void Start()
    {
        popUp.SetActive(false);
    }

    void Update()
    {
        if (isOnTrigger)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                //Destroy(gameObject);
                popUp.SetActive(false);
                manager.StartMinigame(minigameID);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            popUp.SetActive(true);
            isOnTrigger = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            popUp.SetActive(false);
            isOnTrigger = false;
        }
    }

}
