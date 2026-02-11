using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class HumanController : MonoBehaviour
{
    [SerializeField]
    private float _maxMoveDistance;
    [SerializeField]
    private float _maxWaitTime;
    [SerializeField]
    private Animator _animator;
    [SerializeField]
    private NavMeshAgent _agent;

    private static readonly string MOVE_NAME = "Move";

    public void Init(Vector3 startPosition)
    {
        _agent.enabled = true; 
        _agent.Warp(startPosition);
        StartCoroutine(LifeTime());
    }

    private IEnumerator LifeTime()
    {
        var radius = 1f;
        while (!_agent.isOnNavMesh)
        {
            if (NavMesh.SamplePosition(transform.position, out NavMeshHit hit, radius, NavMesh.AllAreas))
            {
                _agent.Warp(hit.position);
            }
            radius++;
            yield return null;
        }
        while (gameObject)
        {
            var angle = Random.Range(0f, 360f) * Mathf.Deg2Rad;
            var position = transform.position + new Vector3(Mathf.Sin(angle), 0, Mathf.Cos(angle)) * Random.Range(0, _maxMoveDistance);
            _agent.destination = position;
            while (Vector3.Distance(transform.position, _agent.destination) > 0.5f &&
                Vector3.Distance(transform.position, _agent.pathEndPosition) > 0.5f)
            {
                yield return new WaitForSeconds(0.2f);
            }
            yield return new WaitForSeconds(Random.Range(0, _maxWaitTime));
        }
    }

    private void Update()
    {
        if(_agent.velocity.magnitude != 0 && !_animator.GetBool(MOVE_NAME))
        {
            _animator.SetBool(MOVE_NAME, true);
        }
        else if (_agent.velocity.magnitude == 0 && _animator.GetBool(MOVE_NAME))
        {
            _animator.SetBool(MOVE_NAME, false);
        }
    }
}
