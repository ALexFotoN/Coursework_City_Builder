using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RoadBuilding : Building
{
    [SerializeField]
    private Material[] _materials;

    private string _buildAudio = "build";
    private string _cancelAudio = "click";

    private int _collisionCount;

    private List<Color> _baseColors;

    public override void Init(BuildingData data)
    {
        base.Init(data);
        IsBuilt = false;
        _buildingColider.enabled = true;
        _buildingColider.isTrigger = true;
        _collisionCount = 0;
        transform.parent = ServiceLocator.CurrentSericeLocator.GetServise<AddressManager>().RoadContainer;

        if (_materials == null || _materials.Length == 0)
        {
            _materials = GetComponentsInChildren<MeshRenderer>().Select(x => x.material).ToArray();
        }

        if (_baseColors == null)
        {
            _baseColors = new();

            foreach (var material in _materials)
            {
                _baseColors.Add(material.GetColor("_BaseColor"));
            }
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
            Vector3 localHitPoint = transform.parent.InverseTransformPoint(hit.point);

            int gridX = Mathf.RoundToInt(localHitPoint.x / 5f) * 5;
            int gridZ = Mathf.RoundToInt(localHitPoint.z / 5f) * 5;

            transform.localPosition = new Vector3(gridX, 0, gridZ);
        }

        float scroll = Input.GetAxis("Mouse ScrollWheel");

        float rotationAmount = 0;

        if(scroll > 0)
        {
            rotationAmount = 90;
        }
        else if (scroll < 0)
        {
            rotationAmount = 90;
        }

        transform.Rotate(Vector3.up, rotationAmount, Space.World);

        if (Input.GetMouseButtonUp(0))
        {
            TryBuild();
        }
        if (Input.GetMouseButtonUp(1))
        {
            gameObject.SetActive(false);
            AudioService.PlayAudio(_cancelAudio);
        }
    }

    private void TryBuild()
    {
        if (_collisionCount > 0 || !_moneyManager.TrySpend(_data.Cost))
        {
            gameObject.SetActive(false);
            AudioService.PlayAudio(_cancelAudio);
        }
        else
        {
            IsBuilt = true;
            _buildingColider.isTrigger = false;
            for (int i = 0; i < _materials.Length; i++)
            {
                _materials[i].SetColor("_BaseColor", _baseColors[i]);
            }
            AudioService.PlayAudio(_buildAudio);
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
        if (_collisionCount == 0)
        {
            foreach (var material in _materials)
            {
                material.SetColor("_BaseColor", Color.cyan);
            }
        }
    }
}
