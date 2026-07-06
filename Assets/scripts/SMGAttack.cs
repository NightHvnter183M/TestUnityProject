using System;
using UnityEngine;


namespace DefaultNamespace
{
    public class SMGAttack : MonoBehaviour
    {
        public static Boolean isShooting = false;
        private float nextFireTime;
        public WeaponType currentWeapon;
        [SerializeField] private float damage = 95f;
        [SerializeField] private float rate = 0.15f;
        [SerializeField] private Camera playerCamera;
        
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
            if (currentWeapon == WeaponType.SMG)
            {
                if (isShooting && Time.time >= nextFireTime)
                {
                    Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
                    RaycastHit hit;
                    if (Physics.Raycast(ray, out hit, 300f) &&
                        hit.collider.TryGetComponent<EnemyHealth>(out EnemyHealth enemy)) enemy.TakeDamage(damage);
                    nextFireTime = Time.time + rate;
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