using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuView : ViewBase
{
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void OpenSettings()
    {
        _viewManager.OpenSettings();
    }

    public void Exit()
    {
        Application.Quit();
    }
}
