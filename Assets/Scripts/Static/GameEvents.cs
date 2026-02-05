using System;
using UnityEngine;

public static class GameEvents
{
    public static Action OnDestructionModeEntered;
    public static Action<BuildingData> OnBuildingWasDestroy;

    public static Action<BuildingData> OnBuildModeEntered;
    public static Action<BuildingData> OnBuildingWasBuild;

    public static Action<Vector2> OnMouseUp;
}
