using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;

public class EventButton : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    private Image _iconImage;
    [SerializeField]
    private Image _backImage;

    private BuildingData _data;
    public BuildingData Data => _data;

    public event Action OnPointerClickEvent;
    
    public void OnPointerClick(PointerEventData eventData)
    {
        OnPointerClickEvent?.Invoke();
    }

    public void SetData(BuildingData data)
    {
        _data = data;
        _iconImage.sprite = _data.Icon;
    }

    public void SetColor(EventButton button)
    {
        if(button == this)
            _backImage.color = Color.yellow;
        else
            _backImage.color = Color.white;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.DOScale(1.1f, 0.2f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.DOScale(1f, 0.2f);
    }
}
