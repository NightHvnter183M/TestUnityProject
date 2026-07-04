using UnityEngine;

namespace DefaultNamespace
{
    public class ShotgunAttack : MonoBehaviour
    {
        [SerializeField] private float Damage = 80f;
        [SerializeField] private Camera playerCamera;
        [SerializeField] public float rateShotgun = 0.15f;
        public void Shoot()
        {
            Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 300f) && hit.collider.TryGetComponent<EnemyHealth>(out EnemyHealth enemy)) enemy.TakeDamage(Damage);
        }
    }
}