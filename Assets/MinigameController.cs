using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MinigameController : MonoBehaviour
{
    GameObject[,] grid = new GameObject[5, 5];

    public Camera minigameCamera;

    public Sprite rock;
    int tileWidth = 32;
    int tileHeight = 32;

    // Start is called before the first frame update
    void Start()
    {
        for (int x = 0; x < 5; x++)
        {
            for (int y = 0; y < 5; y++)
            {
                grid[x, y] = GameObject.Find("Grid" + (x + y).ToString());
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

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
            }

        }
    }

    public void StartMinigame()
    {
       
    }
}
