using UnityEngine;
using System;
using DG.Tweening;
using Random = UnityEngine.Random;

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
                OnBuild?.Invoke();
            }
            _isBuilt = value;
        }
    }

    public event Action OnBuild;

    public virtual void Init(BuildingData data)
    {

    }

    public void Remove()
    {
        _particle.Play(true);
        _buildingObject.DOMoveY(_removeConfig.FallPosition, _removeConfig.FallDuration).SetEase(Ease.InExpo);
        _buildingObject.DORotate(_buildingObject.transform.rotation.eulerAngles + 
            new Vector3(Random.Range(_removeConfig.RotateDispersion.x, _removeConfig.RotateDispersion.y), 0, 
            Random.Range(_removeConfig.RotateDispersion.x, _removeConfig.RotateDispersion.y)), _removeConfig.RotateDuration);
        _buildingColider.enabled = false;
        Destroy(gameObject, _removeConfig.TimeToReturn);
    }
}
