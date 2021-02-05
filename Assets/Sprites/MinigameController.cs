using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MinigameController : MonoBehaviour
{
    public GameObject[,] grid = new GameObject[5, 5];

    public GameManager manager;
    public ProtagonistController protagonist;
    public Camera minigameCamera;

    //artifact info
    int artifactId = 0;
    

    public Sprite rock;
    int tileWidth = 32;
    int tileHeight = 32;

    // Start is called before the first frame update
    void Start()
    {
        int i = 1;
        for (int x = 0; x < 5; x++)
        {
            for (int y = 0; y < 5; y++)
            {
                grid[x, y] = GameObject.Find("Grid" + i.ToString());
                i++;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (manager.GetGameState() != 1) //If not in minigame state
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            RaycastHit hit;
            Ray ray = minigameCamera.ScreenPointToRay(Input.mousePosition);
            Debug.Log("Raycasting");
            Debug.DrawRay(ray.origin,ray.direction*100,Color.red,1);

            if (Physics.Raycast(ray, out hit,1000.0f))
            {
                GameObject gridHit = hit.transform.gameObject;
                Debug.Log(gridHit.name + " Was Hit!");

                gridHit.GetComponent<SpriteRenderer>().sprite = null;
            }

        }

        //Debug end minigame
        if(Input.GetKeyDown("m"))
        {
            EndMinigame();
        }

    }

    public void StartMinigame(int id)
    {
        for (int x = 0; x < 5; x++)
        {
            for (int y = 0; y < 5; y++)
            {
                grid[x, y].GetComponent<SpriteRenderer>().sprite = rock;
            }
        }

        artifactId = id;
    }

    public void EndMinigame()
    {
        protagonist.AddArtifact(artifactId);
        manager.EndMinigame();
        Destroy(GameObject.Find("Artifact" + artifactId.ToString()));
    }
}
