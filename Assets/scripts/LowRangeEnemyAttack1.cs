using UnityEngine;
using UnityEngine.AI;

public class LowRangeEnemyAttack1 : MonoBehaviour
{
    [SerializeField] private Player playerData;
    [SerializeField] private float detectionRange = 10f;
    [SerializeField] private float attackRange = 3.5f;
    [SerializeField] private float attackCooldown = 1.5f;
    [SerializeField] private float damage = 10f;
    private Transform _playerTransform;
    private NavMeshAgent _agent;
    private float _nextAttackTime;

    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null) _playerTransform = playerObj.transform;
    }

    void Update()
    {
        if (_playerTransform == null) return;
        float distanceToPlayer = Vector3.Distance(transform.position, _playerTransform.position);
        if (distanceToPlayer <= detectionRange)
        {
            _agent.SetDestination(_playerTransform.position);
            if (distanceToPlayer <= attackRange) TryAttack();
        }
    }

    private void TryAttack()
    {
        if (Time.time >= _nextAttackTime)
        {
            _nextAttackTime = Time.time + attackCooldown;
            playerData.ConsumeHP(damage);
        }
    }
}
