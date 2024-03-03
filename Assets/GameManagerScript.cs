using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour
{
    bool paused;
    bool gameStart;
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject tutorial;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0;
        gameStart = false;
        paused = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameStart)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                TogglePause();
            }
        }
    }

    public void StartGame()
    {
        gameStart = true;
        tutorial.SetActive(false);
        TogglePause();
    }

    public void TogglePause()
    {
        paused = !paused;
        if (paused)
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0;
        }

        if (!paused)
        {
            pauseMenu.SetActive(false);
            Time.timeScale = 1;
        }
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
