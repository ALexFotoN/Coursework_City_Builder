using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class EventButton : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    private Image _iconImage;
    [SerializeField]
    private Image _backImage;
    [SerializeField]
    private ResourcesInfoData _moneyInfo;
    [SerializeField]
    private ResourcesInfoData _happyInfo;

    private string _audioId = "click";

    [Serializable]
    private struct ResourcesInfoData
    {
        public GameObject Container;
        public TMP_Text Text;
    }

    private BuildingData _data;
    public BuildingData Data => _data;

    public event Action OnPointerClickEvent;
    
    public void OnPointerClick(PointerEventData eventData)
    {
        AudioService.PlayAudio(_audioId);
        OnPointerClickEvent?.Invoke();
    }

    public void SetData(BuildingData data)
    {
        _data = data;
        if(_data.Icon)
            _iconImage.sprite = _data.Icon;
        _moneyInfo.Text.text = $"{_data.Cost}";
        _happyInfo.Text.text = $"{_data.Happy}";
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

        if(_data.Cost > 0)
            _moneyInfo.Container.SetActive(true);
        if (_data.Happy > 0)
            _happyInfo.Container.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.DOScale(1f, 0.2f);
        _moneyInfo.Container.SetActive(false);
        _happyInfo.Container.SetActive(false);
    }

    private void Awake()
    {
        _moneyInfo.Container.SetActive(false);
        _happyInfo.Container.SetActive(false);
    }
}
