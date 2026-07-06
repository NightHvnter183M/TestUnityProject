using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class RevolverAttack : MonoBehaviour
    {
        [SerializeField] private float Damage = 35;
        [SerializeField] private float Rate = 0.5f;
        [SerializeField] private Camera playerCamera;
        public static Boolean isShooting  = false;
        public WeaponType currentWeapon;
        private float nextFireTime;
        
        private void OnEnable()
        {
            WeaponChoosement.OnWeaponChanged += UpdateCurrentWeapon;
        }

        private void OnDisable()
        {
            WeaponChoosement.OnWeaponChanged -= UpdateCurrentWeapon;
        }

        private void Update()
        {
            if (currentWeapon == WeaponType.Revolver)
            {
                if (isShooting & Time.time >= nextFireTime)
                {
                    Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
                    RaycastHit hit;
                    if (Physics.Raycast(ray, out hit, 300f) &&
                        hit.collider.TryGetComponent<EnemyHealth>(out EnemyHealth enemy)) enemy.TakeDamage(Damage);
                    nextFireTime = Time.time + Rate;
                }
            }
            else isShooting = false;
        }
        
        private void UpdateCurrentWeapon(WeaponChoosement.WeaponType newWeapon)
        {
            currentWeapon = (WeaponType)newWeapon;
        }
    }
}