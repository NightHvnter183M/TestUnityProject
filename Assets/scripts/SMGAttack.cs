using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class SMGAttack : MonoBehaviour
    {
        private static Boolean isShooting = false;
        private float nextFireTime;
        [SerializeField] private float damage = 95f;
        [SerializeField] private float rate = 0.15f;
        [SerializeField] private Camera playerCamera;
        
        public static void StartShoot()
        {
            isShooting = true;
        }

        public static void StopShoot()
        {
            isShooting = false;
        }

        private void Update()
        {
            if (isShooting && Time.time >= nextFireTime)
            {
                Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, 300f) && hit.collider.TryGetComponent<EnemyHealth>(out EnemyHealth enemy)) enemy.TakeDamage(damage);
                nextFireTime = Time.time + rate;
            }
        }
    }
}