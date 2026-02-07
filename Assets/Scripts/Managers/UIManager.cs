using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour, IService
{
    [SerializeField]
    private float _timeToAnimation = 0.5f;
    [SerializeField]
    private Button _exitButton;
    [SerializeField]
    private ResourcesView _resourcesView;
    public ResourcesView ResourcesView => _resourcesView;
    [SerializeField]
    private TitlePopUp _titlePopUp;
    public TitlePopUp TitlePopUp => _titlePopUp;
    [SerializeField]
    private ActionButtonsView _actionButtonsView;
    public ActionButtonsView ActionButtonsView => _actionButtonsView;

    private float _resourcesViewY;
    private float _exitButtonY;
    private float _actionButtonsViewY;

    private void Awake()
    {
        _exitButton.onClick.AddListener(ExitGame);

        _resourcesViewY = _resourcesView.Rect.anchoredPosition.y;
        _exitButtonY = _exitButton.targetGraphic.rectTransform.anchoredPosition.y;
        _actionButtonsViewY = _actionButtonsView.Rect.anchoredPosition.y;

        HideUI(true);
    }

    private void ExitGame()
    {
        Application.Quit();
    }

    public void ShowUI()
    {
        _resourcesView.Rect.DOAnchorPosY(_resourcesViewY, _timeToAnimation);
        _exitButton.targetGraphic.rectTransform.DOAnchorPosY(_exitButtonY, _timeToAnimation);
        _actionButtonsView.Rect.DOAnchorPosY(_actionButtonsViewY, _timeToAnimation);
    }

    public void HideUI(bool force = false)
    {
        _resourcesView.Rect.DOAnchorPosY(200, force ? 0 : _timeToAnimation);
        _exitButton.targetGraphic.rectTransform.DOAnchorPosY(200, force ? 0 : _timeToAnimation);
        _actionButtonsView.Rect.DOAnchorPosY(-200, force ? 0 : _timeToAnimation);
    }
}
