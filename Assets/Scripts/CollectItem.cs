using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectItem : MonoBehaviour
{
    public GameObject popUp;
    void Start()
    {
        popUp.SetActive(false);
    }

    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        popUp.SetActive(true);
    }

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Destroy(gameObject);
            popUp.SetActive(false);
        }
    }
    void OnTriggerExit(Collider other)
    {
        popUp.SetActive(false);
    }

}
