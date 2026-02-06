using UnityEngine;

public class ServiceLocatorLoader : MonoBehaviour
{
    [SerializeField]
    private BuildManager _buildManager;
    public BuildManager BuildManager => _buildManager;
    [SerializeField]
    private DestructionManager _destructionManager;
    public DestructionManager DestructionManager => _destructionManager;
    [SerializeField]
    private HappinesManager _happinesManager;
    public HappinesManager HappinesManager => _happinesManager;
    [SerializeField]
    private MoneyManager _moneyManager;
    public MoneyManager MoneyManager => _moneyManager;

    private void Awake()
    {
        ServiceLocator.Initialize();
        ServiceLocator.CurrentSericeLocator.RegisterService<BuildManager>(_buildManager);
        ServiceLocator.CurrentSericeLocator.RegisterService<DestructionManager>(_destructionManager);
        ServiceLocator.CurrentSericeLocator.RegisterService<HappinesManager>(_happinesManager);
        ServiceLocator.CurrentSericeLocator.RegisterService<MoneyManager>(_moneyManager);
    }
}
