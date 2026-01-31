using UnityEngine;
using DG.Tweening;

public class MenuController : MonoBehaviour
{
    [SerializeField]
    private MainMenuView _mainMenuView;
    [SerializeField]
    private SettingsView _settingsView;
    [SerializeField]
    private Camera _mainCamera;
    [Header("Parametrs")]
    [SerializeField]
    private float _heightCrane = 7.8f;
    [SerializeField]
    private float _timeToUp = 0.5f;
    [SerializeField]
    private float _cameraDistance = -3.4f;
    [SerializeField]
    private float _timeToZoom = 0.5f;

    private void Awake()
    {
        _mainMenuView.Init(this);
        _settingsView.Init(this);

        _mainMenuView.transform.position = Vector3.zero;
        if (!_mainCamera)
        {
            _mainCamera = Camera.main;
        }
        _settingsView.gameObject.SetActive(false);
    }

    private void Start()
    {
        _mainMenuView.transform.DOMoveY(_heightCrane, _timeToUp).OnComplete(() => {
            _mainCamera.transform.DOMoveZ(_cameraDistance, _timeToZoom).OnComplete(() => _settingsView.gameObject.SetActive(true));
            _mainMenuView.Show();
        });
    }

    public void OpenSettings()
    {
        _mainMenuView.Hide();
        _settingsView.Show();
    }

    public void OpenMenu()
    {
        _settingsView.Hide();
        _mainMenuView.Show();
    }
}
