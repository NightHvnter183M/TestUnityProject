using UnityEngine;

namespace DefaultNamespace
{
    public class RailGunAttack : MonoBehaviour
    {
        [SerializeField] public float rate = 10f;
        [SerializeField] private float damage = 130f;
        [SerializeField] private Camera playerCamera;
        public void Shoot()
        {
            Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 300f) && hit.collider.TryGetComponent<EnemyHealth>(out EnemyHealth enemy)) enemy.TakeDamage(damage);
        }
    }
}