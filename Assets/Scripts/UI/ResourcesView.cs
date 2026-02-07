using TMPro;
using UnityEngine;

public class ResourcesView : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _moneyText;
    [SerializeField]
    private TMP_Text _happyText;
    [SerializeField]
    private RectTransform _rect;
    public RectTransform Rect => _rect;

    public string Money { 
        set
        {
            _moneyText.text = value;
        }
    }
    public string Happy
    {
        set
        {
            _happyText.text = value;
        }
    }
}
