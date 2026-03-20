using System.Collections.Generic;
using UnityEngine;

public class ActionButtonsView : MonoBehaviour
{
    [SerializeField]
    private EventButton _destructionButton;
    [SerializeField]
    private EventButton _buildButtonPrefab;
    [SerializeField]
    private BuildingsConfigSO _buildingsConfig;
    [SerializeField]
    private RectTransform _rect;
    public RectTransform Rect => _rect;

    private List<EventButton> _buildButtons;

    private BuildManager _buildManager;
    private DestructionManager _destructionManager;


    private void Awake()
    {
        _buildManager = ServiceLocator.CurrentSericeLocator.GetServise<BuildManager>();
        _destructionManager = ServiceLocator.CurrentSericeLocator.GetServise<DestructionManager>();

        _buildButtons = new();

        AddButtons(_buildingsConfig);
        _destructionButton.SetData(new BuildingData
        {
            Cost = _destructionManager.DestructionCost
        });
        _destructionButton.OnPointerClickEvent += () => _buildManager.BuildModeExit();
        _destructionButton.OnPointerClickEvent += () => _destructionManager.DestructionModeEnter();
    }

    public void AddButtons(BuildingsConfigSO buildingsConfig)
    {
        for (int i = 0; i < buildingsConfig.BuildingConfigs.Length; i++)
        {
            var button = Instantiate(_buildButtonPrefab, transform);
            var data = buildingsConfig.BuildingConfigs[i].Data;
            button.SetData(data);
            button.OnPointerClickEvent += () => _buildManager.BuildModeEnter(data);
            button.OnPointerClickEvent += () => _destructionManager.DestructionModeExit();
            _buildButtons.Add(button);
        }
    }

    public void UsedLimitedBuild(BuildingData data)
    {
        foreach (var button in _buildButtons)
        {
            if(button.Data == data)
            {
                button.Disable();
                return;
            }
        }
    }

    public void ReturnLimitedBuild(BuildingData data)
    {
        foreach (var button in _buildButtons)
        {
            if (button.Data == data)
            {
                button.Enable();
                return;
            }
        }
    }
}
