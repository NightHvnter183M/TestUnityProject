using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class GrenadeProjectile : MonoBehaviour
    {
        [SerializeField] private float damage = 90f;
        [SerializeField] private float lifetime = 5f;

        void Start()
        {
            Destroy(gameObject, lifetime);
        }
        
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.tag == "Enemy")
            {
                collision.collider.TryGetComponent<EnemyHealth>(out EnemyHealth enemy);
                enemy.TakeDamage(damage);
                Destroy(gameObject);
            }
        }
    }
}