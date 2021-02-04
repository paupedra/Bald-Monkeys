using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    int artifactsCollected;

    public GameObject pauseMenu;

    public GameObject inventoryMenu;

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
            PauseMenu();
        }

        if(Input.GetKeyDown(KeyCode.Tab))
        {
            InventoryMenu();
        }

    }

    void PauseMenu()
    {
        if (pauseMenu.activeSelf)
        {
            pauseMenu.SetActive(false);
        }
        else
            pauseMenu.SetActive(true);
        
    }

    void InventoryMenu()
    {
        if (inventoryMenu.activeSelf)
        {
            inventoryMenu.SetActive(false);
        }
        else
            inventoryMenu.SetActive(true);
        
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
