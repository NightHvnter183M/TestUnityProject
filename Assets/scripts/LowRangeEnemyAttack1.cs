using UnityEngine;
using UnityEngine.AI;

public class LowRangeEnemyAttack1 : MonoBehaviour
{
    [SerializeField] private Player playerData;
    [SerializeField] private float detectionRange = 10f;
    [SerializeField] private float attackRange = 3.5f;
    [SerializeField] private float attackCooldown = 1.5f;
    [SerializeField] private float damage = 10f;
    private Transform playerTransform;
    private NavMeshAgent agent;
    private float nextAttackTime;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null) playerTransform = playerObj.transform;
    }

    void Update()
    {
        if (playerTransform == null) return;
        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);
        if (distanceToPlayer <= detectionRange)
        {
            agent.SetDestination(playerTransform.position);
            if (distanceToPlayer <= attackRange) TryAttack();
        }
    }

    private void TryAttack()
    {
        if (Time.time >= nextAttackTime)
        {
            nextAttackTime = Time.time + attackCooldown;
            playerData.ConsumeHP(damage);
        }
    }
}
