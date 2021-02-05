using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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

    public Button[] artifactButtons;
    public Sprite[] artifactSprites;
    string[] artifactNames = new string[5];

    public Camera playerCamera;
    public Camera minigameCamera;

    int artifactsCollected;

    //Artifacts
    public bool[] artifacts = new bool[5];

    public GameObject pauseMenu;

    public GameObject inventoryMenu;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 5; i++)
        {
            artifacts[i] = false;
        }

        gameState = GameState.FREEWALK;

        SwitchLockedMouse();

        playerCamera.enabled = true;
        minigameCamera.enabled = false;

        inventoryMenu.SetActive(false);
        pauseMenu.SetActive(false);

        artifactNames[0] ="Analytical Engine";
        artifactNames[1] ="Calculator";
        artifactNames[2] ="Plesiosaurus";
        artifactNames[3] ="Quartz Electrometer";
        artifactNames[4] ="Telescope";

        for (int i = 0; i < 5; i++)
        {
            artifactButtons[i].GetComponentInChildren<Text>().text = "?";
        }
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

    public void AddArtifact(int id) //When an artifact is found the position in array is set to true
    {
        id--;
        artifacts[id] = true;
        artifactButtons[id].GetComponentInChildren<Text>().text = artifactNames[id];
    }
}
