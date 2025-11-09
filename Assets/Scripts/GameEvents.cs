using System;
using UnityEngine;

public static class GameEvents
{
    public static Action OnDestructionModeEntered;

    public static Action<BuildingData> OnBuildModeEntered;
}
