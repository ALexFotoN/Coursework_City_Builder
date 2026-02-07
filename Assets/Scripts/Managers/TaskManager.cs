using UnityEngine;
using UnityEngine.SceneManagement;

public class TaskManager : MonoBehaviour
{
    [SerializeField]
    private int _targetHappines;
    [SerializeField]
    private int _startMoney;

    private UIManager _uiManager;
    private MoneyManager _moneyManager;
    private HappinesManager _happinesManager;

    private void Awake()
    {
        _uiManager = ServiceLocator.CurrentSericeLocator.GetServise<UIManager>();
        _moneyManager = ServiceLocator.CurrentSericeLocator.GetServise<MoneyManager>();
        _happinesManager = ServiceLocator.CurrentSericeLocator.GetServise<HappinesManager>();

        _moneyManager.ChangeValue(_startMoney);
        _happinesManager.SetMaxHappy(_targetHappines);

        _happinesManager.OnChangeValue += CheckHappines;
        _moneyManager.OnChangeValue += CheckMoney;
    }

    private void Start()
    {
        _uiManager.ShowUI();
    }

    private void CheckHappines(int value)
    {
        if(value >= _targetHappines)
        {
            //Win
            _happinesManager.OnChangeValue -= CheckHappines;
            _moneyManager.OnChangeValue -= CheckMoney;
            _uiManager.TitlePopUp.ShowTitle("Win", 2, callback: () => SceneManager.LoadScene(0));
            _uiManager.HideUI();
        }
    }

    private void CheckMoney(int value)
    {
        if(value <= 0)
        {
            //Lose
            _happinesManager.OnChangeValue -= CheckHappines;
            _moneyManager.OnChangeValue -= CheckMoney;
            _uiManager.TitlePopUp.ShowTitle("Lose", 2, callback: () => SceneManager.LoadScene(0));
            _uiManager.HideUI();
        }
    }
}
