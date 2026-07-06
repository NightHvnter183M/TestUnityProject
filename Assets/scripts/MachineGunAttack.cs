using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class MachineGunAttack : MonoBehaviour
    {
        public static Boolean isShooting = false;
        public WeaponType currentWeapon;
        [SerializeField] private float Damage = 15;
        [SerializeField] private float Rate = 0.2f;
        [SerializeField] private Camera playerCamera;
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
            if (currentWeapon == WeaponType.MachineGun)
            {
                if (isShooting && Time.time >= nextFireTime)
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