using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour, IService
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
    [SerializeField]
    private TitlePopUp _titlePopUp;
    public TitlePopUp TitlePopUp => _titlePopUp;

    private List<EventButton> _buildButtons;

    private void Awake()
    {
        var buildManager = ServiceLocator.CurrentSericeLocator.GetServise<BuildManager>();
        var destructionManager = ServiceLocator.CurrentSericeLocator.GetServise<DestructionManager>();

        _buildButtons = new();

        for (int i = 0; i < _buildingsConfig.BuildingConfigs.Length; i++)
        {
            var button = Instantiate(_buildButtonPrefab, _buildButtonsContainer);
            var data = _buildingsConfig.BuildingConfigs[i].Data;
            button.SetData(data);
            button.OnPointerClickEvent += () => buildManager.BuildModeEnter(data);
            button.OnPointerClickEvent += () => destructionManager.DestructionModeExit();
            _buildButtons.Add(button);
        }

        _destructionButton.OnPointerClickEvent += () => buildManager.BuildModeExit();
        _destructionButton.OnPointerClickEvent += () => destructionManager.DestructionModeEnter();

        _exitButton.onClick.AddListener(ExitGame);
    }

    private void ExitGame()
    {
        Application.Quit();
    }
}
