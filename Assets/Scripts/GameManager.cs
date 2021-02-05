using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

enum GameState
{
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

    bool isMouseLocked = false;

    // Start is called before the first frame update
    void Start()
    {
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        UnityEngine.Cursor.visible = false;
        isMouseLocked = true;

        playerCamera.enabled = true;
        minigameCamera.enabled = false;

        gameState = GameState.FREEWALK;
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

            SwitchLockedMouse();

            if(gameState != GameState.PAUSED)
            {
                oldState = gameState;
                gameState = GameState.PAUSED;
            }
            else
            {
                gameState = oldState;
            }
            
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

    public void StartMinigame(int id)
    {
        playerCamera.enabled = false;
        minigameCamera.enabled = true;
        gameState = GameState.MINIGAME;
        Debug.Log("Started minigame: " + id.ToString());

        SwitchLockedMouse();

        //code to start overlay of minigame and unlock mouse and such
        minigameController.StartMinigame();

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
        if (isMouseLocked)
        {
            UnityEngine.Cursor.lockState = CursorLockMode.None;
            UnityEngine.Cursor.visible = true;
        }
        else
        {
            UnityEngine.Cursor.lockState = CursorLockMode.Locked;
            UnityEngine.Cursor.visible = false;
        }
        isMouseLocked = !isMouseLocked;
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
