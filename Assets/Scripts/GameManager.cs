using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    int artifactsCollected;

    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        //Open inGame menu when ESC is pressed
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            //Stop the player movement
            
            //Open inGame menu
            InGameMenu();
        }

    }



    void InGameMenu()
    {

    }
}
