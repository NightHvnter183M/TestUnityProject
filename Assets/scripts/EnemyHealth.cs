using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private Player playerData;
    private float _currentHealth;

    void Start()
    {
        _currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        _currentHealth -= damage;
        if (_currentHealth <= 0) Die();

    }

    private void Die()
    {
        Destroy(gameObject);
        playerData.HealHP(10f);
    }
}