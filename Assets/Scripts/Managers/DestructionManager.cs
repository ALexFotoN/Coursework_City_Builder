using UnityEngine;

public class DestructionManager : MonoBehaviour, IService
{
    [SerializeField]
    private LayerMask _buildingsLayer;
    [SerializeField]
    private int _destructionCost;
    [SerializeField]
    private Texture2D _destructionCursor;

    private bool _destructionActive;

    private MoneyManager _moneyManager;

    private void Awake()
    {
        _moneyManager = ServiceLocator.CurrentSericeLocator.GetServise<MoneyManager>();
    }

    public void DestructionModeEnter()
    {
        _destructionActive = true;

        Cursor.SetCursor(_destructionCursor, new Vector2(511.5f, 511.5f), CursorMode.ForceSoftware);
    }

    public void DestructionModeExit()
    {
        _destructionActive = false;
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
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
        if (Input.GetMouseButtonUp(1))
        {
            DestructionModeExit();
        }
    }

    private void TryToDestroyBuilding()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, _buildingsLayer))
        {
            if (hit.collider.TryGetComponent(out IDestroyable destroyable))
            {
                if (!_moneyManager.TrySpend(_destructionCost))
                {
                    return;
                }
                destroyable.Remove();
            }
        }
    }
}
