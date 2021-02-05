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

enum artifactUI
{   
    NONE = 3,
    MODEL = 2,
    TEXT = 1,
    IMAGE=0
}

public class GameManager : MonoBehaviour
{
    public MinigameController minigameController;

    artifactUI UiState = artifactUI.MODEL;

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
    public GameObject artifactUiModel;
    public GameObject artifactUiText;
    public GameObject artifactUiImage;

    public Object[] artifactModels;
    public Sprite[] artifactTexts;
    public Sprite[] artifactImages;

    //Artifacts
    public bool[] artifacts = new bool[5];

    public GameObject pauseMenu;

    public GameObject inventoryMenu;

    Quaternion oldRotationModel;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 5; i++)
        {
            artifacts[i] = false;
        }

        gameState = GameState.FREEWALK;

        SwitchLockedMouse();

        inventoryCamera.enabled = false;
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

        oldRotationModel = artifactUiModel.transform.rotation;
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

        if(gameState == GameState.INVENTORY)
        {
            if(Input.GetKeyDown("a"))
            {
                TurnArtifactUiLeft();
            }
            if (Input.GetKeyDown("d"))
            {
                TurnArtifactUiRight();
            }
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
        SwitchArtifactUi(0);
    }
    public void ShowCalculator()
    {
        SwitchArtifactUi(1);
    }
    public void ShowPlesiosaurus()
    {
        SwitchArtifactUi(2);
    }
    public void ShowQuartzElectrometer()
    {
        SwitchArtifactUi(3);
    }
    public void ShowTelescope()
    {
        SwitchArtifactUi(4);
    }

    void SwitchArtifactUi(int i)
    {
        if (artifacts[i])
        {
            foreach (Transform mod in artifactUiModel.transform)
            {
                Destroy(mod.gameObject);
            }
            Instantiate(artifactModels[i], artifactUiModel.transform.position, artifactUiModel.transform.rotation, artifactUiModel.transform);

            switch(i)
            {
                case 0:
                    artifactUiModel.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
                    artifactUiModel.transform.rotation = oldRotationModel;
                    break;
                case 1: //calculator
                    artifactUiModel.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
                    artifactUiModel.transform.Rotate(new Vector3(15f, 180f, 0f));
                    break;
                case 2: //dinosaurio
                    artifactUiModel.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                    artifactUiModel.transform.rotation = oldRotationModel;
                    break;
                case 3: //quartz
                    artifactUiModel.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
                    artifactUiModel.transform.rotation = oldRotationModel;
                    break;
                case 4: //telescope
                    artifactUiModel.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
                    artifactUiModel.transform.rotation = oldRotationModel;
                    break;
            }

            artifactUiText.GetComponent<SpriteRenderer>().sprite = artifactTexts[i];
            artifactUiImage.GetComponent<SpriteRenderer>().sprite = artifactImages[i];
        }
        SwitchUIArtifact();
        
    }

    void SwitchUIArtifact()
    {
        switch (UiState)
        {
            case artifactUI.MODEL:
                artifactUiModel.SetActive(true);
                artifactUiText.SetActive(false);
                artifactUiImage.SetActive(false);
                break;
            case artifactUI.IMAGE:
                artifactUiModel.SetActive(false);
                artifactUiText.SetActive(false);
                artifactUiImage.SetActive(true);
                break;
            case artifactUI.TEXT:
                artifactUiModel.SetActive(false);
                artifactUiText.SetActive(true);
                artifactUiImage.SetActive(false);
                break;
        }
    }

    public void TurnArtifactUiRight() //add
    {
        int i = (int)UiState;
        i++;
        UiState = (artifactUI)i;

        if (i == 3)
        {
            UiState = artifactUI.IMAGE;
        }
        SwitchUIArtifact();
    }

    public void TurnArtifactUiLeft() //substract
    {
        int i = (int)UiState;
        i--;
        UiState = (artifactUI)i;

        if (i == -1)
        {
            UiState = artifactUI.MODEL;
        }
        SwitchUIArtifact();
    }

    void UpdateArtifactUi()
    {

    }
}
