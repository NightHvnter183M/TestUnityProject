using System;
using System.Collections.Concurrent;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.InputSystem;

public enum WeaponType { Revolver, Shotgun, MachineGun, GrenadeLauncher, RailGun, SMG };

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private Spell[] boundSpells = new Spell[6];
    [SerializeField] private Player playerData;
    [SerializeField] private Transform ShootPoint;
    public WeaponType currentWeapon;
    public static float overdriveModifier = 1f;
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

    private void Update()
    {
        if (playerData.CurrentMana < playerData.MaxMana)
        {
            playerData.CurrentMana += playerData.ManaRestoreRate * Time.deltaTime;
        }
    }
    
    public void OnCast(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            int weaponIndex = (int)currentWeapon;
            Spell activeSpell = boundSpells[weaponIndex];
            if (activeSpell == null)
            {
                Debug.LogWarning("No spell!");
                return;
            }
            CalculateWeaponModifiers(currentWeapon, out float costMultiplier);
            float finalCost = activeSpell.baseCost * costMultiplier * overdriveModifier;
            if (playerData.CurrentMana >= finalCost)
            {
                playerData.CurrentMana -= finalCost;
            }
            else
            {
                float missingMana = finalCost - playerData.CurrentMana;
                playerData.CurrentMana = 0f;
                float missingHP = missingMana * 0.8f;
                finalCost *= (1f + (missingHP / playerData.MaxHP) * overdriveModifier);
                playerData.ConsumeHP(missingHP);
            }
            activeSpell.Cast(ShootPoint, finalCost);
        }
    }
    private void OnShoot(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            switch (currentWeapon)
            {
                case WeaponType.Revolver:
                    RevolverAttack.isShooting = true;
                    break;
                case WeaponType.MachineGun:
                    MachineGunAttack.isShooting = true;
                    break;
                case WeaponType.SMG:
                    SMGAttack.isShooting = true;
                    break;
            }
        }

        if (context.canceled)
        {
            switch (currentWeapon)
            {
                case WeaponType.Revolver:
                    RevolverAttack.isShooting = false;
                    break;
                case WeaponType.MachineGun:
                    MachineGunAttack.isShooting = false;
                    break;
                case WeaponType.SMG:
                    SMGAttack.isShooting = false;
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

    private void CalculateWeaponModifiers(WeaponType weapon,  out float cost)
    {
        switch (weapon)
        {
            case WeaponType.Revolver:
            case WeaponType.Shotgun:
            case WeaponType.MachineGun:
            case WeaponType.GrenadeLauncher:
                cost = 0.6f;   
                break;

            case WeaponType.RailGun:
            case WeaponType.SMG:
                cost = 1.4f;   
                break;

            default:
                cost = 1f;
                break;
        }
    }
}
