using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Player", menuName = "Scriptable Objects/Player")]
public class Player : ScriptableObject
{
    [SerializeField] public float MaxHP = 100f;
    [SerializeField] public float CurrentHP;
    [SerializeField] private float UsedHP;

    public event Action<float, float> OnHealthChanged;

    private void OnEnable()
    {
        CurrentHP = MaxHP;
        UsedHP = 0f;
    }

    public void ConsumeHP(float amount)
    {
        CurrentHP -= amount;
        CurrentHP = Mathf.Clamp(CurrentHP, 0f, MaxHP);
        UsedHP += amount;
        OnHealthChanged?.Invoke(CurrentHP, UsedHP);
    }

    public void HealHP(float amount)
    {
        CurrentHP += amount;
        CurrentHP = Mathf.Clamp(CurrentHP, 0f, MaxHP);
        OnHealthChanged?.Invoke(CurrentHP, UsedHP);
    }
}