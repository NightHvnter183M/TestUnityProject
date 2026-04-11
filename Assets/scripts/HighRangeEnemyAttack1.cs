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
    private NavMeshAgent agent;
    private Transform player;
    private float nextFireTime;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (player == null) return;
        float distance = Vector3.Distance(transform.position, player.position);
        if (distance < fleeRange)
        {
            Vector3 fleeDirection = (transform.position - player.position).normalized;
            Vector3 runTo = transform.position + fleeDirection * 5f;
            agent.SetDestination(runTo);
        }
        else if (distance <= attackRange)
        {
            agent.ResetPath();
            LookAtPlayer();
            if (Time.time >= nextFireTime)
            {
                Shoot();
                nextFireTime = Time.time + fireRate;
            }
        }
        else agent.SetDestination(player.position);
    }

    private void LookAtPlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;
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
