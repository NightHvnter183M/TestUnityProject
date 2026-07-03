using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class RevolverAttack : MonoBehaviour
    {
        [SerializeField] private float Damage = 35;
        [SerializeField] private float Rate = 0.5f;
        [SerializeField] private Camera playerCamera;
        private static Boolean isShooting  = false;
        private float nextFireTime;

        public static void Shoot()
        {
            isShooting = true;
        }

        public static void StopShoot()
        {
            isShooting = false;
        }

        private void Update()
        {
            if (isShooting & Time.time >= nextFireTime)
            {
                Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, 300f) && hit.collider.TryGetComponent<EnemyHealth>(out EnemyHealth enemy)) enemy.TakeDamage(Damage);
                Console.WriteLine("Shooted");
                nextFireTime = Time.time + Rate;
            }
        }
    }
}