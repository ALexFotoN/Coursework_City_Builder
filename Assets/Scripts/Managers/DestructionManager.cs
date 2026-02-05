using UnityEngine;

public class DestructionManager : MonoBehaviour
{
    [SerializeField]
    private LayerMask _buildingsLayer;
    [SerializeField]
    private int _destructionCost;

    private bool _destructionActive;

    private void Awake()
    {
        GameEvents.OnDestructionModeEntered += DestructionModeEnter;
        GameEvents.OnBuildModeEntered += DestructionModeExit;
    }

    private void OnDestroy()
    {
        GameEvents.OnDestructionModeEntered -= DestructionModeEnter;
        GameEvents.OnBuildModeEntered -= DestructionModeExit;
    }

    private void DestructionModeEnter()
    {
        _destructionActive = true;
    }

    private void DestructionModeExit(BuildingData data)
    {
        _destructionActive = false;
    }

    private void Update()
    {
        if (!_destructionActive)
        {
            return;
        }

        if (Input.GetMouseButtonUp(0))
        {
            TryToDestroyBuilding();
        }
    }

    private void TryToDestroyBuilding()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, _buildingsLayer))
        {
            if (hit.collider.TryGetComponent(out IDestroyable destroyable))
            {
                if (!MoneyManager.Instance.TrySpend(_destructionCost))
                {
                    return;
                }
                destroyable.Remove();
            }
        }
    }
}
