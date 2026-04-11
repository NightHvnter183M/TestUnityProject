using UnityEngine;

public class HighRangeEnemyProjectile1 : MonoBehaviour
{
    [SerializeField] private float damage = 25f;
    [SerializeField] private float lifetime = 5f;
    [SerializeField] private Player playerData;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerData.ConsumeHP(damage);
            Destroy(gameObject);
        }
        else if (!collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
    }
}
