using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class MinigameController : MonoBehaviour
{
    struct Tile
    {
        public GameObject gridTile;
        public Vector2 pos;
        public bool isFilled;
    }

    Tile[] grid = new Tile[25];

    public Text artifactFoundText;

    public float endgameDelay=1;
    float endGameTimer = 0;

    public GameManager manager;
    public ProtagonistController protagonist;
    public Camera minigameCamera;

    //artifact info
    int artifactId = 0;
    int score = 0;

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
                grid[i - 1].gridTile = GameObject.Find("Grid" + i.ToString());
                grid[i - 1].pos = new Vector2(x, y);
                grid[i - 1].isFilled = false;
                i++;
            }
        }

        artifactFoundText.gameObject.SetActive(true);

        artifactFoundText.enabled = false;
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
            Debug.DrawRay(ray.origin, ray.direction * 100, Color.red, 1);

            if (Physics.Raycast(ray, out hit, 1000.0f))
            {
                GameObject gridHit = hit.transform.gameObject;
                //Debug.Log(gridHit.name + " Was Hit!");

                gridHit.GetComponent<SpriteRenderer>().sprite = null;


                for (int i = 0; i < 25; i++)
                {
                    if (grid[i].gridTile == gridHit)
                    {
                        if (grid[i].isFilled)
                        {
                            score++;
                            Debug.Log("Hit filled tile!");
                        }

                    }
                }
            }

        }

        //Debug end minigame
        //if(Input.GetKeyDown("m"))
        //{
        //    EndMinigame();
        //}

        if (score >= 4)
        {
            artifactFoundText.enabled = true;
            endGameTimer += Time.deltaTime;

            if (endGameTimer >= endgameDelay)
            {
                endGameTimer = 0;
                artifactFoundText.enabled = false;
                EndMinigame();
            }
        }

    }

    public void StartMinigame(int id)
    {
        for (int i = 0; i < 25; i++)
        {
            grid[i].gridTile.GetComponent<SpriteRenderer>().sprite = rock;
            grid[i].isFilled = false;
        }

        artifactId = id;

        ChooseTile();

        score = 0;

        artifactFoundText.enabled = false;
    }

    public void EndMinigame()
    {
        manager.AddArtifact(artifactId);
        manager.EndMinigame();
        Destroy(GameObject.Find("Artifact" + artifactId.ToString()));
    }

    void ChooseTile()
    {
        int r = Random.Range(0, 25);

        while (grid[r].pos.x == 4 || grid[r].pos.y == 4)
        {
            r = Random.Range(0, 25);
        }

        Debug.Log("Artifact tile is: " + grid[r].pos.x.ToString() + grid[r].pos.y.ToString());

        //find other digging tiles
        Vector2 tile = new Vector2(grid[r].pos.x, grid[r].pos.y);
        Vector2[] tiles = new Vector2[3];

        tiles[0] = new Vector2(tile.x + 1, tile.y);
        tiles[1] = new Vector2(tile.x, tile.y + 1);
        tiles[2] = new Vector2(tile.x + 1, tile.y + 1);

        int found = 0;
        for (int i = 0; i < 25; i++)
        {
            if (grid[i].pos == tile)
            {
                grid[i].isFilled = true;
                found++;
                continue;
            }

            for (int j = 0; j < 3; j++)
            {
                if (tiles[j] == grid[i].pos)
                {
                    grid[i].isFilled = true;
                    found++;
                    break;
                }
            }

            if (found >= 4)
            {
                break;
            }
        }
    }
}
