using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponChoosement : MonoBehaviour
{
    public enum WeaponType {Revolver, Shotgun, MachineGun, GrenadeLaiuncher, RailGun, SMG};
    [SerializeField] private Player playerData;
    public WeaponType currentWeapon = WeaponType.Revolver;
    public static event Action<WeaponType> OnWeaponChanged;

    public void OnSelectWeapon(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            byte weaponIndex = (byte)Mathf.RoundToInt(context.ReadValue<float>());
            ChangeWeapon((byte)(weaponIndex - 1));
        }
    }

    private void ChangeWeapon(byte index)
    {
        if (index < 0 || index >= Enum.GetValues(typeof(WeaponType)).Length)
            return;
        if (index == (byte)currentWeapon)
            return;
        currentWeapon = (WeaponType)index;
        OnWeaponChanged?.Invoke(currentWeapon);
    }
}