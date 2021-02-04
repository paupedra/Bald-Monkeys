using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectItem : MonoBehaviour
{
    public GameObject popUp;
    public bool isOnTrigger = false;
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
                Destroy(gameObject);
                popUp.SetActive(false);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        popUp.SetActive(true);
        isOnTrigger = true;
    }

    void OnTriggerExit(Collider other)
    {
        popUp.SetActive(false);
        isOnTrigger = false;
    }

}
