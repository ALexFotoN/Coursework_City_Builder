using UnityEngine;

[CreateAssetMenu(fileName = "BuildingsConfig", menuName = "Config/BuildingsConfig")]
public class BuildingsConfigSO : ScriptableObject
{
    [SerializeField]
    private BuildingDataConfigSO[] _buildingConfigs;
    public BuildingDataConfigSO[] BuildingConfigs => _buildingConfigs;
}
