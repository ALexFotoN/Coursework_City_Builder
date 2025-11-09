using UnityEngine;

[CreateAssetMenu(fileName = "BuildingConfig", menuName = "Config/BuildingConfig")]
public class BuildingDataConfigSO : ScriptableObject
{
    [SerializeField]
    private BuildingData _data;
    public BuildingData Data => _data;
}
