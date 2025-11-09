using System;
using System.Collections;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    //to config
    [SerializeField]
    private Building _buildingPrefab;
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

    private void BuildModeEnter(string id)
    {
        StartCoroutine(YieldEndFrame(CreateBuilding));
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

    private void CreateBuilding()
    {
        _currentBuilding = Instantiate(_buildingPrefab);
        _currentBuilding.Init();
        _currentBuilding.OnBuild += CurrentBuildingWasBuild;
    }

    private void CurrentBuildingWasBuild()
    {
        _currentBuilding.OnBuild -= CurrentBuildingWasBuild;
        _currentBuilding = null;
    }

    private IEnumerator YieldEndFrame(Action callback)
    {
        yield return new WaitForEndOfFrame();
        callback?.Invoke();
    }
}
