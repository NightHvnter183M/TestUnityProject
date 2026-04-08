using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHUD : MonoBehaviour
{
    [SerializeField] private Player playerData;
    [SerializeField] private Image healthBarFill;
    [SerializeField] private Image DashBarFill;
    [SerializeField] private TextMeshProUGUI weaponNameText;

    private void OnEnable()
    {
        if (playerData != null)
        {
            playerData.OnHealthChanged += RefreshUi;
        }
        WeaponChoosement.OnWeaponChanged += RefreshWeaponUi;
    }

    private void OnDisable()
    {
        if (playerData != null)
        {
            playerData.OnHealthChanged -= RefreshUi;
        }
        WeaponChoosement.OnWeaponChanged -= RefreshWeaponUi;
    }

    private void RefreshUi(float currentHP, float maxHP)
    {
        if (healthBarFill != null)
        {
            healthBarFill.fillAmount = playerData.CurrentHP / playerData.MaxHP;
        }
    }

    public void RefreshDashUi(byte DashAmount)
    {
        if (DashBarFill != null)
        {
            DashBarFill.fillAmount = (float)DashAmount / 3f; // Assuming max dash amount is 3
        }
    }
    private void RefreshWeaponUi(WeaponChoosement.WeaponType weaponType)
    {
        if (weaponNameText != null)
            weaponNameText.text = weaponType.ToString();
    }
}