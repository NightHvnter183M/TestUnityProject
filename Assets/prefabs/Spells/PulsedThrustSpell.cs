using UnityEngine;

namespace prefabs.Spells
{
    [CreateAssetMenu(fileName = "PulsedThrust", menuName = "Spells/Tier 1/Pulsed Thrust", order = 0)]
    public class PulsedThrustSpell : Spell
    {
        [SerializeField] private float force;
        [SerializeField] public float maxAngle;
        [SerializeField] public float finalRadius;
        
        
        
        public override void Cast(Transform castPoint, float finalDamage)
        {
            Collider[] colliders = Physics.OverlapSphere(castPoint.position, finalRadius);
            foreach (Collider col in colliders)
            {
                if (col.CompareTag("Player")) continue;
                Vector3 directionToEnemy = (col.transform.position - castPoint.position).normalized;
                float angle = Vector3.Angle(castPoint.forward, directionToEnemy);
                if (angle < maxAngle)
                {
                    if (col.TryGetComponent(out EnemyHealth enemyHealth))
                    {
                        enemyHealth.TakeDamage(finalDamage);
                    }
                    if (col.TryGetComponent(out UnityEngine.AI.NavMeshAgent agent))
                    {
                        // Временно отключаем агента, чтобы физика сработала, либо толкаем через метод
                        Vector3 pushDir = directionToEnemy;
                        pushDir.y = 0; // Толкаем строго по горизонтали
                    
                        // Самый простой способ для NavMesh — сдвинуть его позицию (Warp или Velocity)
                        agent.velocity = pushDir * force;;
                    }
                }
            }
        }
    }
}