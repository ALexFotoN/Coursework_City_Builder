using TMPro;
using UnityEngine;

public class HappinesManager : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _totalHappyText;
    private int _totalHappy;

    private void Start()
    {
        ChangeValue(20);
    }

    private void ChangeValue(int value)
    {
        _totalHappy += value;
        _totalHappyText.text = $"{_totalHappy}";
    }
}
