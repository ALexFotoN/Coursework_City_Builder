using TMPro;
using UnityEngine;
using System.Linq;

public class HappinesManager : MonoBehaviour, IService
{
    [SerializeField]
    private TMP_Text _totalHappyText;
    private int _totalHappy;

    private void Start()
    {
        var startBuildings = FindObjectsByType<Building>(FindObjectsSortMode.None);
        var startHappines = startBuildings.Sum(x => x.Data.Happy);

        ChangeValue(startHappines);
    }

    public void ChangeValue(int value)
    {
        _totalHappy += value;
        _totalHappyText.text = $"{_totalHappy}";
    }
}
