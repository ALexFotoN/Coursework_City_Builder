using System;
using TMPro;
using UnityEngine;

public class MoneyManager : MonoBehaviour, IService
{
    private int _totalMoney;
    private UIManager _uiManager;

    public Action<int> OnChangeValue;

    private void Awake()
    {
        _uiManager = ServiceLocator.CurrentSericeLocator.GetServise<UIManager>();
    }

    public void ChangeValue(int value)
    {
        _totalMoney += value;
        _uiManager.ResourcesView.Money = $"{_totalMoney}";
        OnChangeValue?.Invoke(_totalMoney);
    }

    public bool TrySpend(int value)
    {
        if(_totalMoney - value < 0)
        {
            OnChangeValue?.Invoke(0);
            return false;
        }
        ChangeValue(-value);
        return true;
    }
}
