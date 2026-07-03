using UnityEngine;
using UnityEngine.AI;

public class HighRangeEnemyAttack1 : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private float attackRange = 15f;
    [SerializeField] private float fleeRange = 7f;
    [SerializeField] private float fireRate = 2f;
    [SerializeField] private float projectileSpeed = 20f;
    private NavMeshAgent _agent;
    private Transform _player;
    private float _nextFireTime;
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (_player == null) return;
        float distance = Vector3.Distance(transform.position, _player.position);
        if (distance < fleeRange)
        {
            Vector3 fleeDirection = (transform.position - _player.position).normalized;
            Vector3 runTo = transform.position + fleeDirection * 5f;
            _agent.SetDestination(runTo);
        }
        else if (distance <= attackRange)
        {
            _agent.ResetPath();
            LookAtPlayer();
            if (Time.time >= _nextFireTime)
            {
                Shoot();
                _nextFireTime = Time.time + fireRate;
            }
        }
        else _agent.SetDestination(_player.position);
    }

    private void LookAtPlayer()
    {
        Vector3 direction = (_player.position - transform.position).normalized;
        direction.y = 0;
        transform.rotation = Quaternion.LookRotation(direction);
    }

    private void Shoot()
    {
        GameObject projectile = Instantiate(projectilePrefab, shootPoint.position, shootPoint.rotation);
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.AddForce(shootPoint.forward * projectileSpeed, ForceMode.Impulse);
        }
    }
}
