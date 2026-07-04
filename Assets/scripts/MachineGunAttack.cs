using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class MachineGunAttack : MonoBehaviour
    {
        private static Boolean IsShooting = false;
        [SerializeField] private float Damage = 15;
        [SerializeField] private float Rate = 0.2f;
        [SerializeField] private Camera playerCamera;
        private float nextFireTime;
        
        public static void StartShoot()
        {
            IsShooting = true;
        }

        public static void StopShoot()
        {
            IsShooting = false;
        }

        private void Update()
        {
            if (IsShooting && Time.time >= nextFireTime)
            {
                Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, 300f) && hit.collider.TryGetComponent<EnemyHealth>(out EnemyHealth enemy)) enemy.TakeDamage(Damage);
                nextFireTime = Time.time + Rate;
            }
        }
    }
}