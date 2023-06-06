using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    public static bool isPaused = false;

    public GameObject pausedGame, openSettingsInGame;


    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                PausedFunction();
            }
        }
    }

    public void Resume()
    {
  
        pausedGame.SetActive(false);
        openSettingsInGame.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void PausedFunction()
    {
        pausedGame.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }
}
