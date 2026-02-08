using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    [TextArea]
    [SerializeField]
    private string[] _messages;

    private int _currentMessage;

    private UIManager _uiManager;

    private void Awake()
    {
        _uiManager = ServiceLocator.CurrentSericeLocator.GetServise<UIManager>();
    }

    private void Start()
    {
        _uiManager.MessageView.OnEndShowMessage += ShowMessage;
        ShowMessage();
    }

    private void ShowMessage()
    {
        if(_currentMessage == _messages.Length)
        {
            _uiManager.MessageView.OnEndShowMessage -= ShowMessage;
            return;
        }
        _uiManager.MessageView.SetText(_messages[_currentMessage]);
        _currentMessage++;
    }
}
