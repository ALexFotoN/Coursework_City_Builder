using System.Collections.Generic;
using UnityEngine;

public class BuildingPool : MonoBehaviour
{
    private List<Building> _pool = new();

    public Building GetBuilding(BuildingData buildingData)
    {
        foreach (var building in _pool)
        {
            if(!building.gameObject.activeInHierarchy && building.BuildingId == buildingData.Id)
            {
                return building;
            }
        }

        return CreateNewBuilding(buildingData);
    }

    private Building CreateNewBuilding(BuildingData data)
    {
        var building = Instantiate(data.Prefab);
        _pool.Add(building);
        return building;
    }
}