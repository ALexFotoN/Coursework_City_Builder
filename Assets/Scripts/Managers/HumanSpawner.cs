using Unity.AI.Navigation;
using UnityEngine;

public class HumanSpawner : MonoBehaviour
{
    [SerializeField]
    private float _distanceToSpawn;
    [SerializeField]
    private int _countToSpawn;
    [SerializeField]
    private HumanController _humanPrefab;

    private void Start()
    {
        for (int i = 0; i < _countToSpawn; i++)
        {
            var human = Instantiate(_humanPrefab);
            var angle = Random.Range(0f, 360f) * Mathf.Deg2Rad;
            var position = transform.position + new Vector3(Mathf.Sin(angle), 0, Mathf.Cos(angle)) * Random.Range(0, _distanceToSpawn);
            human.Init(position);
        }
    }
}
