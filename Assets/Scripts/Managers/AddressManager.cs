using UnityEngine;

public class AddressManager : MonoBehaviour, IService
{
    [SerializeField]
    private Transform _roadContainer;
    public Transform RoadContainer => _roadContainer;
}
