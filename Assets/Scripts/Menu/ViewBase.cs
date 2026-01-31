using UnityEngine;
using DG.Tweening;

public abstract class ViewBase : MonoBehaviour
{
    [SerializeField]
    protected CanvasGroup _containerGroup;
    [SerializeField]
    private float _timeToShow = 0.5f;
    [SerializeField]
    private float _moveToShow = 0f;
    [SerializeField]
    private float _moveToHide = 0f;

    protected MenuController _viewManager;

    public void Init(MenuController manager)
    {
        _viewManager = manager;
    }

    private void Awake()
    {
        _containerGroup.alpha = 0;
    }

    public virtual void Show()
    {
        transform.DOMoveX(_moveToShow, _timeToShow);
        _containerGroup.DOFade(1, _timeToShow);
    }

    public virtual void Hide()
    {
        transform.DOMoveX(_moveToHide, _timeToShow);
        _containerGroup.DOFade(0, _timeToShow);
    }
}
