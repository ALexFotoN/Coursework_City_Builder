using TMPro;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _totalMoneyText;
    private int _totalMoney;

    #region Singleton
    private static MoneyManager _instance;
    public static MoneyManager Instance => _instance;

    private void Awake()
    {
        if (_instance)
        {
            Destroy(gameObject);
            return;
        }
        _instance = this;
    }
    #endregion

    private void Start()
    {
        ChangeValue(5000);
    }

    private void ChangeValue(int value)
    {
        _totalMoney += value;
        _totalMoneyText.text = $"{_totalMoney}";
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
