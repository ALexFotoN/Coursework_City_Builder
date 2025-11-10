using System;
using System.Collections;
using UnityEngine;
using System.Linq;

public class BuildManager : MonoBehaviour
{
    [SerializeField]
    private BuildingPool _pool;

    private Building _currentBuilding;

    private void Awake()
    {
        GameEvents.OnBuildModeEntered += BuildModeEnter;
        GameEvents.OnDestructionModeEntered += BuildModeExit;
    }

    private void OnDestroy()
    {
        GameEvents.OnBuildModeEntered -= BuildModeEnter;
        GameEvents.OnDestructionModeEntered -= BuildModeExit;
    }

    private void BuildModeEnter(BuildingData data)
    {
        StartCoroutine(YieldEndFrame(CreateBuilding, data));
    }

    private void BuildModeExit()
    {
        if (!_currentBuilding)
        {
            return;
        }
        _currentBuilding.gameObject.SetActive(false);
        _currentBuilding = null;
    }

    private void CreateBuilding(BuildingData data)
    {
        if (data == null)
        {
            return;
        }
        if (_currentBuilding)
        {
            _currentBuilding.gameObject.SetActive(false);
        }
        _currentBuilding = _pool.GetBuilding(data);
        _currentBuilding.Init(data);
        _currentBuilding.OnBuild += CurrentBuildingWasBuild;
        _currentBuilding.gameObject.SetActive(true);
    }

    private void CurrentBuildingWasBuild()
    {
        if (!_currentBuilding)
        {
            return;
        }
        _currentBuilding.OnBuild -= CurrentBuildingWasBuild;
        _currentBuilding = null;
    }

    private IEnumerator YieldEndFrame(Action<BuildingData> callback, BuildingData data)
    {
        yield return new WaitForEndOfFrame();
        callback?.Invoke(data);
    }
}
