using UnityEngine;
using UnityEngine.SceneManagement;

public class TaskManager : MonoBehaviour
{
    [SerializeField]
    private int _targetHappines;

    private UIManager _uiManager;

    private void Awake()
    {
        _uiManager = ServiceLocator.CurrentSericeLocator.GetServise<UIManager>();

        ServiceLocator.CurrentSericeLocator.GetServise<HappinesManager>().OnChangeValue += CheckHappines;
        ServiceLocator.CurrentSericeLocator.GetServise<MoneyManager>().OnChangeValue += CheckMoney;
    }

    private void CheckHappines(int value)
    {
        if(value >= _targetHappines)
        {
            //Win
            _uiManager.TitlePopUp.ShowTitle("Win", 2, callback: () => SceneManager.LoadScene(0));
        }
    }

    private void CheckMoney(int value)
    {
        if(value <= 0)
        {
            //Lose
            _uiManager.TitlePopUp.ShowTitle("Lose", 2, callback: () => SceneManager.LoadScene(0));
        }
    }
}
