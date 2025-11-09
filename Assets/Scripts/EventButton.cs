using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EventButton : MonoBehaviour, IPointerDownHandler
{
    [SerializeField]
    private Image _iconImage;
    [SerializeField]
    private Image _backImage;

    public event Action OnPointerDownEvent;

    public void OnPointerDown(PointerEventData eventData)
    {
        OnPointerDownEvent?.Invoke();
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
