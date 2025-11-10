using UnityEngine;
using System;

[Serializable]
public class BuildingData
{
    public string Id;
    public Sprite Icon;
    public Building Prefab;

    [Header("Disolve Shader")]
    public float MinWorldHeight = -1f;
    public float MaxWorldHeight = 4f;
}
