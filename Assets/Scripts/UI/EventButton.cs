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

    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.DOScale(1.1f, 0.2f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.DOScale(1f, 0.2f);
    }
}
