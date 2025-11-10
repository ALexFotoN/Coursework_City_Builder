using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private EventButton _destructionButton;
    [SerializeField]
    private EventButton _buildButtonPrefab;
    [SerializeField]
    private Transform _buildButtonsContainer;
    [SerializeField]
    private BuildingsConfigSO _buildingsConfig;
    [SerializeField]
    private Button _exitButton;

    private List<EventButton> _buildButtons;

    private void Awake()
    {
        _buildButtons = new();

        for (int i = 0; i < _buildingsConfig.BuildingConfigs.Length; i++)
        {
            var button = Instantiate(_buildButtonPrefab, _buildButtonsContainer);
            var data = _buildingsConfig.BuildingConfigs[i].Data;
            button.SetIcon(data.Icon);
            button.OnPointerClickEvent += () => GameEvents.OnBuildModeEntered?.Invoke(data);
            _buildButtons.Add(button);
        }

        _destructionButton.OnPointerClickEvent += () => GameEvents.OnDestructionModeEntered?.Invoke();
        GameEvents.OnDestructionModeEntered += () => _destructionButton.SetColor(Color.yellow);
        GameEvents.OnBuildModeEntered += (x) => _destructionButton.SetColor(Color.white);

        _exitButton.onClick.AddListener(ExitGame);
    }

    private void ExitGame()
    {
        Application.Quit();
    }
}
