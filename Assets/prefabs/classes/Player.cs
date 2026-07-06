using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Player", menuName = "Scriptable Objects/Player")]
public class Player : ScriptableObject
{
    [SerializeField] public float MaxHP = 100f;
    [SerializeField] public float MaxMana = 200f;
    [SerializeField] public float CurrentHP;
    [SerializeField] public float CurrentMana;
    [SerializeField] public float ManaRestoreRate = 0.5f;
    public event Action<float, float> OnHealthChanged;

    private void OnEnable()
    {
        CurrentHP = MaxHP;
        CurrentMana = MaxMana;
        ;
    }

    public void ConsumeHP(float amount)
    {
        CurrentHP -= amount;
        CurrentHP = Mathf.Clamp(CurrentHP, 0f, MaxHP);
        OnHealthChanged?.Invoke(MaxHP, CurrentHP);
    }

    public void HealHP(float amount)
    {
        CurrentHP += amount;
        CurrentHP = Mathf.Clamp(CurrentHP, 0f, MaxHP);
        OnHealthChanged?.Invoke(MaxHP, CurrentHP);
    }
}