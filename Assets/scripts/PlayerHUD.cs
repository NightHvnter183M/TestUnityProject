using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHUD : MonoBehaviour
{
    [SerializeField] private Player playerData;
    [SerializeField] private Image healthBarFill;
    [SerializeField] private Image DashBarFill;

    private void OnEnable()
    {
        if (playerData != null)
        {
            playerData.OnHealthChanged += RefreshUi;
        }
    }

    private void OnDisable()
    {
        if (playerData != null)
        {
            playerData.OnHealthChanged -= RefreshUi;
        }
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
            Console.WriteLine($"Dash Amount: {DashAmount}"); // Debug log to check the value of DashAmount
        }
    }
}