using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingsView : ViewBase
{
    [SerializeField]
    private Slider _audioSlider;
    [SerializeField]
    private Toggle _fullScreenToggle;
    [SerializeField]
    private Toggle _vSyncToggle;
    [SerializeField]
    private TMP_Text _volumeText;

    private void Start()
    {
        _audioSlider.onValueChanged.AddListener(VolumeChanged);
        _fullScreenToggle.onValueChanged.AddListener(FullScreenChanged);
        _vSyncToggle.onValueChanged.AddListener(VSyncChanged);

        _audioSlider.SetValueWithoutNotify(AudioListener.volume);
        _volumeText.text = $"{(int)(AudioListener.volume * 100)}";
        _fullScreenToggle.SetIsOnWithoutNotify(Screen.fullScreen);
        _vSyncToggle.SetIsOnWithoutNotify(QualitySettings.vSyncCount == 1);
    }

    private void VolumeChanged(float value)
    {
        AudioListener.volume = value;
        _volumeText.text = $"{(int)(AudioListener.volume * 100)}";
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
