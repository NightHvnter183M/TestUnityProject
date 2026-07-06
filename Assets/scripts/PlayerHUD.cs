using System;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHUD : MonoBehaviour
{
    [SerializeField] private Player playerData;
    [SerializeField] private RectTransform maskRectTransform;
    [SerializeField] private RectTransform maskRectTransformDash;
    [SerializeField] private TextMeshProUGUI weaponNameText;
    [SerializeField] private float visibleHealth;
    [SerializeField] private float visibleDash;
    private float maxHealthWidth;
    private float maxDashWidth;
    private RectMask2D MaskHealth;
    private RectMask2D MaskDash;

    private void OnEnable()
    {
        if (playerData != null)
        {
            playerData.OnHealthChanged += RefreshHealthUi;
        }
        WeaponChoosement.OnWeaponChanged += RefreshWeaponUi;
    }

    private void OnDisable()
    {
        if (playerData != null)
        {
            playerData.OnHealthChanged -= RefreshHealthUi;
        }
        WeaponChoosement.OnWeaponChanged -= RefreshWeaponUi;
    }

    private void Awake()
    {
        maxHealthWidth = maskRectTransform.rect.width;
        maxDashWidth = maskRectTransformDash.rect.width;
        MaskHealth = maskRectTransform.GetComponent<RectMask2D>();
        MaskDash = maskRectTransformDash.GetComponent<RectMask2D>();
    }

    private void RefreshHealthUi(float maxHP, float currentHP)
    {
        float fillPercentage = currentHP / maxHP;
        visibleHealth = Math.Abs(maxHealthWidth - (maxHealthWidth * fillPercentage));
        MaskHealth.padding = new Vector4(0, 0, visibleHealth, 0);
    }

    public void RefreshDashUi(byte DashAmount)
    {
        float fillPercentage = DashAmount / 3f;
        visibleDash = maxDashWidth - (fillPercentage * maxDashWidth);
        MaskDash.padding = new Vector4(0, 0, visibleDash, 0);
    }
    private void RefreshWeaponUi(WeaponChoosement.WeaponType weaponType)
    {
        if (weaponNameText != null)
            weaponNameText.text = weaponType.ToString();
    }
}