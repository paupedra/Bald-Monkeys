using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public string url;

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown && !Input.GetMouseButtonDown(0))
            LoadGameScene();
    }

    public void LoadGameScene()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void OpenWebPage()
    {
        Application.OpenURL(url);
    }
}
