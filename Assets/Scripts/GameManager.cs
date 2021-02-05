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

    public Camera playerCamera;
    public Camera minigameCamera;
    public Camera inventoryCamera;

    //inventory UI
    public Button[] artifactButtons;
    public Texture[] artifactSprites;
    public Texture[] hiddenArtifactSprites;
    string[] artifactNames = new string[5];

    //rotating UI
    public GameObject rotatingObject;
    public GameObject model;
    public GameObject artifactText;
    public GameObject artifactImage;

    public Object[] artifactModels;
    public Object[] artifactTexts;
    public Object[] artifactImages;

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
            artifactButtons[i].GetComponentInChildren<RawImage>().texture = hiddenArtifactSprites[i]; //hiddenArtifactSprites[i]
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
            inventoryCamera.enabled = true;
            minigameCamera.enabled = false;
            playerCamera.enabled = false;
        }
        else
        {
            gameState = oldState;
            inventoryCamera.enabled = true;
            
            

            if(oldState == GameState.FREEWALK)
            {
                playerCamera.enabled = true;
                minigameCamera.enabled = false;
            }

            if(oldState == GameState.MINIGAME)
            {
                minigameCamera.enabled = true;
                playerCamera.enabled = false;
            }
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
        artifactButtons[id].GetComponentInChildren<RawImage>().texture = artifactSprites[id];
    }

    public void ShowAnalyticalEngine()
    {
        SwitchRotatingUi(0);
    }
    public void ShowCalculator()
    {
        SwitchRotatingUi(1);
    }
    public void ShowPlesiosaurus()
    {
        SwitchRotatingUi(2);
    }
    public void ShowQuartzElectrometer()
    {
        SwitchRotatingUi(3);
    }
    public void ShowTelescope()
    {
        SwitchRotatingUi(4);
    }

    void SwitchRotatingUi(int i)
    {
        if (artifacts[i])
        {
            ClearRotatingUi();

            Instantiate(artifactModels[i], model.transform.position, model.transform.rotation, model.transform);
            Instantiate(artifactTexts[i], artifactText.transform.position, artifactText.transform.rotation, artifactText.transform);
            Instantiate(artifactImages[i], artifactImage.transform.position, artifactImage.transform.rotation, artifactImage.transform);
        }
    }

    void ClearRotatingUi()
    {
        foreach (Transform mod in model.transform)
        {
            Destroy(mod.gameObject);
        }
        foreach (Transform mod in artifactText.transform)
        {
            Destroy(mod.gameObject);
        }
        foreach (Transform mod in artifactImage.transform)
        {
            Destroy(mod.gameObject);
        }
    }
}
