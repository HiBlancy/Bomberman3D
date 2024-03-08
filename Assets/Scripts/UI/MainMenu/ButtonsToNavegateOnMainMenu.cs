using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonsToNavegateOnMainMenu : MonoBehaviour
{
    [SerializeField] GameObject QuitPanel;
    [SerializeField] GameObject MainMenuPanel;
    [SerializeField] GameObject OptionsPanel;

    public void StartButton()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("GameScene");
    }

    public void OptionsMenu()
    {
        OptionsPanel.SetActive(true);
        MainMenuPanel.SetActive(false);
    }

    public void ExitButton()
    {
        QuitPanel.SetActive(true);
        MainMenuPanel.SetActive(false);
    }
    public void BackMenu()
    {
        OptionsPanel.SetActive(false);
        QuitPanel.SetActive(false);
        MainMenuPanel.SetActive(true);
    }

    public void QuitButton()
    {
        Application.Quit();
    }

    public void BackToMain()
    {
        SceneManager.LoadScene("MainMenuScene");
    }
}
