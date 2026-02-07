using TMPro;
using UnityEngine;
using System.Linq;
using System;

public class HappinesManager : MonoBehaviour, IService
{
    private int _totalHappy;
    private int _maxHappy;
    private UIManager _uiManager;

    public Action<int> OnChangeValue;

    private void Awake()
    {
        _uiManager = ServiceLocator.CurrentSericeLocator.GetServise<UIManager>();
    }

    private void Start()
    {
        var startBuildings = FindObjectsByType<Building>(FindObjectsSortMode.None);
        var startHappines = startBuildings.Sum(x => x.Data.Happy);

        ChangeValue(startHappines);
    }

    public void ChangeValue(int value)
    {
        _totalHappy += value;
        var percent = Mathf.Clamp((int)((_totalHappy * 100f) / _maxHappy ), 0, 100);
        _uiManager.ResourcesView.Happy = $"{percent}/100";
        OnChangeValue?.Invoke(_totalHappy);
    }

    public void SetMaxHappy(int value)
    {
        _maxHappy = value;
    }
}
