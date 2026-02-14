using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingsView : ViewBase
{
    [SerializeField]
    private Toggle _fullScreenToggle;
    [SerializeField]
    private Toggle _vSyncToggle;

    private void Start()
    {
        _fullScreenToggle.onValueChanged.AddListener(FullScreenChanged);
        _vSyncToggle.onValueChanged.AddListener(VSyncChanged);

        _fullScreenToggle.SetIsOnWithoutNotify(Screen.fullScreen);
        _vSyncToggle.SetIsOnWithoutNotify(QualitySettings.vSyncCount == 1);
    }

    private void FullScreenChanged(bool value)
    {
        _fullScreenToggle.isOn = value;
    }

    private void VSyncChanged(bool value)
    {
        _vSyncToggle.isOn = value;
    }

    public void BackToMenu()
    {
        _viewManager.OpenMenu();
    }
}
