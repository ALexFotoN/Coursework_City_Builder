using System;
using System.Collections;
using UnityEngine;
using System.Linq;

public class BuildManager : MonoBehaviour
{
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
        Destroy(_currentBuilding.gameObject);
        _currentBuilding = null;
    }

    private void CreateBuilding(BuildingData data)
    {
        if (data == null)
        {
            return;
        }
        _currentBuilding = Instantiate(data.Prefab);
        _currentBuilding.Init(data);
        _currentBuilding.OnBuild += CurrentBuildingWasBuild;
    }

    private void CurrentBuildingWasBuild()
    {
        _currentBuilding.OnBuild -= CurrentBuildingWasBuild;
        _currentBuilding = null;
    }

    private IEnumerator YieldEndFrame(Action<BuildingData> callback, BuildingData data)
    {
        yield return new WaitForEndOfFrame();
        callback?.Invoke(data);
    }
}
