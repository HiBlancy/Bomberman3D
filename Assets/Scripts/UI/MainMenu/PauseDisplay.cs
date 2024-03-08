using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseDisplay : MonoBehaviour
{
    [SerializeField] GameObject PanelPause;
    static bool GamePaused = false;

    [SerializeField] GameObject PanelOptions;

    [SerializeField] GameObject GameOverPanel;

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
        GameObject.Find("First Person Camera").GetComponent<FirstPersonLook>().enabled = true; //is this correct?
        Cursor.lockState = CursorLockMode.Locked;
        PanelPause.SetActive(false);
        GamePaused = false;
        Cursor.visible = false;
    }

    public void BackOnMainPause()
    {
        PanelPause.SetActive(true);
        PanelOptions.SetActive(false);
    }

    public void OptionsPanel()
    {
        PanelPause.SetActive(false);
        PanelOptions.SetActive(true);
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        GameObject.Find("First Person Camera").GetComponent<FirstPersonLook>().enabled = false;
        Cursor.lockState = CursorLockMode.Confined;
        PanelPause.SetActive(true);
        GamePaused = true;
        Cursor.visible = true;
        PanelOptions.SetActive(false);
    }

    public void ExitToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenuScene");
    }

    public void RetryGame()
    {
        Time.timeScale = 1f;
        GameObject.Find("First Person Camera").GetComponent<FirstPersonLook>().enabled = true; //is this correct?
        Cursor.lockState = CursorLockMode.Locked;
        GameOverPanel.SetActive(false);
        Cursor.visible = false;

        SceneManager.LoadScene("GameScene");
    }
}