using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class EventButton : MonoBehaviour, IPointerDownHandler
{
    public event Action OnPointerDownEvent;

    public void OnPointerDown(PointerEventData eventData)
    {
        OnPointerDownEvent?.Invoke();
    }
}
