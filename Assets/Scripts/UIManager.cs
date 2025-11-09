using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Linq;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private EventButton _destructionButton;
    [SerializeField]
    private EventButton _buildButtonPrefab;
    [SerializeField]
    private Transform _buildButtonsContainer;

    private List<EventButton> _buildButtons;

    private void Awake()
    {
        _buildButtons = new();

        for (int i = 0; i < 5; i++)
        {
            var button = Instantiate(_buildButtonPrefab, _buildButtonsContainer);
            button.OnPointerDownEvent += () => GameEvents.OnBuildModeEntered?.Invoke("");
            _buildButtons.Add(button);
        }

        _destructionButton.OnPointerDownEvent += () => GameEvents.OnDestructionModeEntered?.Invoke();
    }
}
