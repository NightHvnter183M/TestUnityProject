using System;
using UnityEngine;
using UnityEngine.InputSystem;

public enum WeaponType { Revolver, Shotgun, MachineGun, GrenadeLaiuncher, RailGun, SMG };

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private Player playerData;
    [SerializeField] private Camera playerCamera;
    public WeaponType currentWeapon;
    private float nextFireTime;
    private bool _isFiring;

    private void OnEnable()
    {
        WeaponChoosement.OnWeaponChanged += UpdateCurrentWeapon;
    }

    private void OnDisable()
    {
        WeaponChoosement.OnWeaponChanged -= UpdateCurrentWeapon;
    }

    public void OnShoot(InputAction.CallbackContext context)
    {
        if (context.started) _isFiring = true;
        if (context.canceled) _isFiring = false;
    }

    public void OnCast(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Cast();
        }
    }

    private void Update()
    {
        if (_isFiring && Time.time >= nextFireTime)
        {
            if (IsAutomatic(currentWeapon))
                Shoot();
            else Shoot();
        }
        else if (!IsAutomatic(currentWeapon))
        {
            _isFiring = false;
        }
    }

    private bool IsAutomatic(WeaponType weapon)
    {
        return weapon == WeaponType.MachineGun || weapon == WeaponType.SMG;
    }

    private void Shoot()
    {
        float damage;
        float firerate;
        switch (currentWeapon)
        {
            case WeaponType.Revolver:
                damage = 20f;
                firerate = 0.5f;
                break;
            case WeaponType.Shotgun:
                damage = 10f;
                firerate = 1f;
                break;
            case WeaponType.MachineGun:
                damage = 5f;
                firerate = 0.1f;
                break;
            case WeaponType.GrenadeLaiuncher:
                damage = 50f;
                firerate = 1.5f;
                break;
            case WeaponType.RailGun:
                damage = 40f;
                firerate = 0.8f;
                break;
            case WeaponType.SMG:
                damage = 7f;
                firerate = 0.2f;
                break;
            default:
                damage = 0f;
                firerate = 1f;
                break;
        }
        nextFireTime = Time.time + firerate;
        Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 300f))
        {
            if (hit.collider.CompareTag("Enemy"))
            {
                hit.collider.GetComponent<EnemyHealth>().TakeDamage(damage);
            }
        }
    }

    private void Cast()
    {
        switch (currentWeapon)
        {
            case WeaponType.Revolver:
                playerData.ConsumeHP(10f);
                break;
            case WeaponType.Shotgun:
                playerData.ConsumeHP(10f);
                break;
            case WeaponType.MachineGun:
                playerData.ConsumeHP(10f);
                break;
            case WeaponType.GrenadeLaiuncher:
                playerData.ConsumeHP(10f);
                break;
            case WeaponType.RailGun:
                playerData.ConsumeHP(30f);
                break;
            case WeaponType.SMG:
                playerData.ConsumeHP(30f);
                break;
        }
    }

    private void UpdateCurrentWeapon(WeaponChoosement.WeaponType newWeapon)
    {
        currentWeapon = (WeaponType)newWeapon;
    }
}
