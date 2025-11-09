using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EventButton : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private Image _iconImage;
    [SerializeField]
    private Image _backImage;

    public event Action OnPointerClickEvent;

    public void OnPointerClick(PointerEventData eventData)
    {
        OnPointerClickEvent?.Invoke();
    }

    public void SetIcon(Sprite icon)
    {
        _iconImage.sprite = icon;
    }

    public void SetColor(Color color)
    {
        _backImage.color = color;
    }
}
