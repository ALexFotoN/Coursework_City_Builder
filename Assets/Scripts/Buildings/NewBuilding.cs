using UnityEngine;
using System.Linq;
using System.Collections;
using DG.Tweening;

public class NewBuilding : Building
{
    [SerializeField]
    private Material[] _materials;
    [SerializeField] 
    private float _dissolveSpeed = 0.5f;
    [SerializeField]
    private float _rotationSpeed;

    private int _collisionCount;
    private float _currentDissolveAmount;

    public override void Init(BuildingData data)
    {
        base.Init(data);
        IsBuilt = false;
        _currentDissolveAmount = 1f;
        _buildingColider.enabled = true;
        _buildingColider.isTrigger = true;
        _collisionCount = 0;
        _buildingObject.DOMoveY(0,0);
        _buildingObject.DORotate(new Vector3(0, _buildingObject.transform.rotation.eulerAngles.y, 0), 0);
        if (_materials == null || _materials.Length == 0)
        {
            _materials = GetComponentsInChildren<MeshRenderer>().Select(x => x.material).ToArray();
        }
        foreach (var material in _materials)
        {
            material.SetFloat("_MinWorldHeight", data.MinWorldHeight);
            material.SetFloat("_MaxWorldHeight", data.MaxWorldHeight);
            material.SetFloat("_DissolveAmount", 0f);
        }
        foreach (var material in _materials)
        {
            material.SetColor("_BaseColor", Color.cyan);
        }
    }

    private void Update()
    {
        if (IsBuilt)
        {
            return;
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, _groundLayer))
        {
            transform.position = hit.point;
        }

        float scroll = Input.GetAxis("Mouse ScrollWheel");

        float rotationAmount = scroll * _rotationSpeed * Time.deltaTime;

        transform.Rotate(Vector3.up, rotationAmount, Space.World);

        if (Input.GetMouseButtonUp(0))
        {
            TryBuild();
        }
    }

    private void TryBuild()
    {
        if (_collisionCount > 0)
        {
            gameObject.SetActive(false);
        }
        else
        {
            IsBuilt = true;
            _buildingColider.isTrigger = false;
            StartCoroutine(Disolve());
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (IsBuilt)
        {
            return;
        }
        _collisionCount++;
        foreach (var material in _materials)
        {
            material.SetColor("_BaseColor", Color.red);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (IsBuilt)
        {
            return;
        }
        _collisionCount--;
        if(_collisionCount == 0)
        {
            foreach (var material in _materials)
            {
                material.SetColor("_BaseColor", Color.cyan);
            }
        }
    }

    private IEnumerator Disolve()
    {
        foreach (var material in _materials)
        {
            material.SetColor("_BaseColor", Color.white);
            material.SetFloat("_DissolveAmount", 1f);
        }
        while (_currentDissolveAmount > 0f)
        {
            _currentDissolveAmount -= _dissolveSpeed * Time.deltaTime;
            _currentDissolveAmount = Mathf.Clamp01(_currentDissolveAmount);
            foreach (var material in _materials)
            {
                material.SetFloat("_DissolveAmount", _currentDissolveAmount);
            }
            yield return null;
        }
    }
}
