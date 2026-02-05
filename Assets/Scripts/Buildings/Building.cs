using UnityEngine;
using System;
using DG.Tweening;
using Random = UnityEngine.Random;
using System.Collections;

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
                GameEvents.OnBuildingWasBuild?.Invoke(_data);
                OnBuild?.Invoke();
            }
            _isBuilt = value;
        }
    }

    public event Action OnBuild;

    private void Awake()
    {
        if(_defaultData)
            _data = _defaultData.Data;
    }

    public virtual void Init(BuildingData data)
    {
        _data = data;
    }

    public void Remove()
    {
        _particle.Play(true);
        _buildingObject.DOMoveY(_removeConfig.FallPosition, _removeConfig.FallDuration).SetEase(Ease.InExpo);
        _buildingObject.DORotate(_buildingObject.transform.rotation.eulerAngles + 
            new Vector3(Random.Range(_removeConfig.RotateDispersion.x, _removeConfig.RotateDispersion.y), 0, 
            Random.Range(_removeConfig.RotateDispersion.x, _removeConfig.RotateDispersion.y)), _removeConfig.RotateDuration);
        _buildingColider.enabled = false;
        StartCoroutine(DelayToReturn());
        GameEvents.OnBuildingWasDestroy?.Invoke(_data);
    }

    private IEnumerator DelayToReturn()
    {
        yield return new WaitForSeconds(_removeConfig.TimeToReturn);
        gameObject.SetActive(false);
    }
}