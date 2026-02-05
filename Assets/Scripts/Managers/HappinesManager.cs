using TMPro;
using UnityEngine;
using System.Linq;

public class HappinesManager : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _totalHappyText;
    private int _totalHappy;

    private void Start()
    {
        GameEvents.OnBuildingWasBuild += (x) => ChangeValue(x.Happy);
        GameEvents.OnBuildingWasDestroy += (x) => ChangeValue(-x.Happy);

        var startBuildings = FindObjectsByType<Building>(FindObjectsSortMode.None);
        var startHappines = startBuildings.Sum(x => x.Data.Happy);

        ChangeValue(startHappines);
    }

    private void ChangeValue(int value)
    {
        _totalHappy += value;
        _totalHappyText.text = $"{_totalHappy}";
    }
}
