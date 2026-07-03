using System;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.InputSystem;

public enum WeaponType { Revolver, Shotgun, MachineGun, GrenadeLauncher, RailGun, SMG };

public class PlayerCombat : MonoBehaviour
{
    public WeaponType currentWeapon;

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
                    RevolverAttack.Shoot();
                    break;
                case WeaponType.MachineGun:
                    break;
                default:
                    Console.WriteLine("Default result");
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
            }
        }
    }

    private void UpdateCurrentWeapon(WeaponChoosement.WeaponType newWeapon)
    {
        currentWeapon = (WeaponType)newWeapon;
    }
}
