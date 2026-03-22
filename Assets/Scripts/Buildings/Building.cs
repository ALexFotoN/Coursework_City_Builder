using UnityEngine;
using System;
using DG.Tweening;
using Random = UnityEngine.Random;
using System.Collections;
using UnityEngine.AI;

public class Building : MonoBehaviour, IDestroyable
{
    [SerializeField]
    protected Transform _buildingObject;
    [SerializeField]
    protected ParticleSystem _particle;
    [SerializeField]
    protected LayerMask _groundLayer;
    [SerializeField]
    protected Collider _buildingColider;
    [SerializeField]
    protected RemoveBuildingConfigSO _removeConfig;
    [SerializeField]
    protected BuildingDataConfigSO _defaultData;

    private string _destroyAudio = "destroy";
    private NavMeshObstacle _navMeshObstacle;

    protected BuildingData _data;
    public BuildingData Data => _data;
    public string BuildingId => _data.Id;

    private bool _isBuilt = true;
    protected bool IsBuilt
    {
        get
        {
            return _isBuilt;
        }
        set
        {
            if (value)
            {
                _happinesManager.ChangeValue(_data.Happy);
                _actionButtonsView.UsedLimitedBuild(_data);
                OnBuild?.Invoke();
            }
            _navMeshObstacle.enabled = value;
            _isBuilt = value;
        }
    }

    public event Action OnBuild;

    protected MoneyManager _moneyManager;
    private HappinesManager _happinesManager;
    private ActionButtonsView _actionButtonsView;

    private void Awake()
    {
        _moneyManager = ServiceLocator.CurrentSericeLocator.GetServise<MoneyManager>();
        _happinesManager = ServiceLocator.CurrentSericeLocator.GetServise<HappinesManager>();
        _actionButtonsView = ServiceLocator.CurrentSericeLocator.GetServise<UIManager>().ActionButtonsView;

        _navMeshObstacle = GetComponent<NavMeshObstacle>();
        _navMeshObstacle.enabled = false;

        if (_defaultData)
            _data = _defaultData.Data;
    }

    public virtual void Init(BuildingData data)
    {
        _data = data;
    }

    public void Remove()
    {
        AudioService.PlayAudio(_destroyAudio);
        _particle.Play(true);
        _buildingObject.DOMoveY(_removeConfig.FallPosition, _removeConfig.FallDuration).SetEase(Ease.InExpo);
        _buildingObject.DORotate(_buildingObject.transform.rotation.eulerAngles + 
            new Vector3(Random.Range(_removeConfig.RotateDispersion.x, _removeConfig.RotateDispersion.y), 0, 
            Random.Range(_removeConfig.RotateDispersion.x, _removeConfig.RotateDispersion.y)), _removeConfig.RotateDuration);
        _buildingColider.enabled = false;
        StartCoroutine(DelayToReturn());
        _happinesManager.ChangeValue(-_data.Happy);
        _actionButtonsView.ReturnLimitedBuild(_data);
    }

    private IEnumerator DelayToReturn()
    {
        yield return new WaitForSeconds(_removeConfig.TimeToReturn);
        gameObject.SetActive(false);
    }
}