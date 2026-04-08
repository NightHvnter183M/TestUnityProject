using UnityEngine;

public class BasicEnemy : MonoBehaviour
{
    [SerializeField] private Player playerData;
    [SerializeField] private float damagePerHit = 10f;
    [SerializeField] private float damageCooldown = 1f;
    private float nextDamageTime;

    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Player") && Time.time >= nextDamageTime)
        {
            ApplyDamage();
            nextDamageTime = Time.time + damageCooldown;
        }
    }

    private void ApplyDamage()
    {
        if (playerData != null)
        {
            playerData.ConsumeHP(damagePerHit);
        }
    }
}
