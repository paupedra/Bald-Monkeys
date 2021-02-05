using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

enum GameState
{
    INVENTORY = 4,
    FREEWALK = 3,
    PAUSED = 2,
    MINIGAME = 1,
    NONE = 0
}

public class GameManager : MonoBehaviour
{
    public MinigameController minigameController;

    GameState gameState = GameState.NONE;
    GameState oldState = GameState.NONE;

    public Camera playerCamera;
    public Camera minigameCamera;

    int artifactsCollected;

    public GameObject pauseMenu;

    public GameObject inventoryMenu;

    // Start is called before the first frame update
    void Start()
    {
        gameState = GameState.FREEWALK;

        SwitchLockedMouse();

        playerCamera.enabled = true;
        minigameCamera.enabled = false;

        inventoryMenu.SetActive(false);
        pauseMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
        //Open inGame menu when ESC is pressed
        if(Input.GetKeyDown(KeyCode.Escape) && gameState != GameState.INVENTORY)
        {
            SwitchPauseGame();
        }


        if(Input.GetKeyDown(KeyCode.Tab) && gameState != GameState.PAUSED)
        {
            SwitchInventory();
        }

    }

    public void SwitchPauseGame() //Pause and unpause game
    {
        //Open inGame menu
        PauseMenu();

        if (gameState != GameState.PAUSED)
        {
            oldState = gameState;
            gameState = GameState.PAUSED;
        }
        else
        {
            gameState = oldState;
        }

        SwitchLockedMouse(); //Do after switching GameState
    }

    public void SwitchInventory()
    {
        InventoryMenu();

        if (gameState != GameState.INVENTORY)
        {
            oldState = gameState;
            gameState = GameState.INVENTORY;
        }
        else
        {
            gameState = oldState;
        }

        SwitchLockedMouse(); //Do after switching GameState
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

    public void StartMinigame(int id)
    {
        playerCamera.enabled = false;
        minigameCamera.enabled = true;
        gameState = GameState.MINIGAME;
        Debug.Log("Started minigame: " + id.ToString());

        SwitchLockedMouse();

        //code to start overlay of minigame and unlock mouse and such
        minigameController.StartMinigame(id);

    }

    public void EndMinigame()
    {
        playerCamera.enabled = true;
        minigameCamera.enabled = false;
        gameState = GameState.FREEWALK;
        SwitchLockedMouse();
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
    void SwitchLockedMouse()
    {
        if (gameState == GameState.FREEWALK)
        {
            UnityEngine.Cursor.lockState = CursorLockMode.Locked;
            UnityEngine.Cursor.visible = false;
        }
        else
        {
            UnityEngine.Cursor.lockState = CursorLockMode.None;
            UnityEngine.Cursor.visible = true;
        }
    }

    public int GetGameState()
    {
        return (int)gameState;

    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
