using UnityEngine;

[CreateAssetMenu(fileName = "RemoveBuildingConfig", menuName = "Config/RemoveBuildingConfig")]
public class RemoveBuildingConfigSO : ScriptableObject
{
    [Header("Fall movement")]
    [SerializeField]
    private float _fallPosition;
    public float FallPosition => _fallPosition;
    [SerializeField]
    private float _fallDuration;
    public float FallDuration => _fallDuration;
    [Header("Fall rotate")]
    [SerializeField]
    private Vector2 _rotateDispersion;
    public Vector2 RotateDispersion => _rotateDispersion;
    [SerializeField]
    private float _rotateDuration;
    public float RotateDuration => _rotateDuration;
    [Header("Return")]
    [SerializeField]
    private float _timeToReturn;
    public float TimeToReturn => _timeToReturn;
}
