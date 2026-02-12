using UnityEngine;
using DG.Tweening;

public class MenuController : MonoBehaviour
{
    [SerializeField]
    private MainMenuView _mainMenuView;
    [SerializeField]
    private SettingsView _settingsView;

    private void Awake()
    {
        _mainMenuView.Init(this);
        _settingsView.Init(this);
    }

    private void Start()
    {
        OpenMenu();
    }

    public void OpenSettings()
    {
        _mainMenuView.Hide();
        _settingsView.Show();
    }

    public void OpenMenu()
    {
        _settingsView.Hide();
        _mainMenuView.Show();
    }
}
