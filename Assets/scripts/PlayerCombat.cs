using System;
using System.Collections.Concurrent;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.InputSystem;

public enum WeaponType { Revolver, Shotgun, MachineGun, GrenadeLauncher, RailGun, SMG };

public class PlayerCombat : MonoBehaviour
{
    public WeaponType currentWeapon;
    private ShotgunAttack shotgun;
    private GrenadeLauncherAttack grenadeLauncher;
    private RailGunAttack railgun;
    private float nextfireShotgun, nextfireGrenade, nextfireRailgun;

    private void Awake()
    {
        shotgun = GetComponent<ShotgunAttack>();
        grenadeLauncher = GetComponent<GrenadeLauncherAttack>();
        railgun = GetComponent<RailGunAttack>();
    }

    private void OnEnable()
    {
        WeaponChoosement.OnWeaponChanged += UpdateCurrentWeapon;
    }

    private void OnDisable()
    {
        WeaponChoosement.OnWeaponChanged -= UpdateCurrentWeapon;
    }
    public void OnCast(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
         
        }
    }
    private void OnShoot(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            switch (currentWeapon)
            {
                case WeaponType.Revolver:
                    RevolverAttack.StartShoot();
                    break;
                case WeaponType.MachineGun:
                    MachineGunAttack.StartShoot();
                    break;
                case WeaponType.SMG:
                    SMGAttack.StartShoot();
                    break;
            }
        }

        if (context.canceled)
        {
            switch (currentWeapon)
            {
                case WeaponType.Revolver:
                    RevolverAttack.StopShoot();
                    break;
                case WeaponType.MachineGun:
                    MachineGunAttack.StopShoot();
                    break;
                case WeaponType.SMG:
                    SMGAttack.StopShoot();
                    break;
            }
        }

        if (context.performed)
        {
            switch (currentWeapon)
            {
                case WeaponType.Shotgun:
                    if (Time.time >= nextfireShotgun)
                    {
                        shotgun.Shoot();
                        nextfireShotgun = Time.time + shotgun.rateShotgun;
                    }
                    break;
                case WeaponType.GrenadeLauncher:
                    if (Time.time >= nextfireGrenade)
                    {
                        grenadeLauncher.Shoot();
                        nextfireGrenade = Time.time + grenadeLauncher.grenadeRate;
                    }
                    break;
                case WeaponType.RailGun:
                    if (Time.time >= nextfireRailgun)
                    {
                        railgun.Shoot();
                        nextfireRailgun = Time.time + railgun.rate;
                    }
                    break;
            }
        }
    }

    private void UpdateCurrentWeapon(WeaponChoosement.WeaponType newWeapon)
    {
        currentWeapon = (WeaponType)newWeapon;
    }
}
