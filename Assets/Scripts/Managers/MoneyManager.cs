using System;
using TMPro;
using UnityEngine;

public class MoneyManager : MonoBehaviour, IService
{
    [SerializeField]
    private TMP_Text _totalMoneyText;
    private int _totalMoney;

    public Action<int> OnChangeValue;

    private void Start()
    {
        ChangeValue(5000);
    }

    private void ChangeValue(int value)
    {
        _totalMoney += value;
        _totalMoneyText.text = $"{_totalMoney}";
        OnChangeValue?.Invoke(_totalMoney);
    }

    public bool TrySpend(int value)
    {
        if(_totalMoney - value < 0)
        {
            return false;
        }
        ChangeValue(-value);
        return true;
    }
}
