using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseDisplay : MonoBehaviour
{
    [SerializeField] GameObject PanelOptions;
    static bool GamePaused = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GamePaused)
            {
                BackToGame();
            }
            else
            {
                Pause();
            }
        }
    }

    public void BackToGame()
    {
        Time.timeScale = 1f;
        PanelOptions.SetActive(false);
        GamePaused = false;
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        PanelOptions.SetActive(true);
        GamePaused = true;
    }
}