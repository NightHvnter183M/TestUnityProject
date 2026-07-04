using UnityEngine;

namespace DefaultNamespace
{
    public class GrenadeLauncherAttack : MonoBehaviour
    {
        [SerializeField] private float speed = 10f;
        [SerializeField] private Camera playerCamera;
        [SerializeField] private GameObject projectilePrefab;
        [SerializeField] private GameObject shootPoint;
        [SerializeField] public float grenadeRate = 1.5f;
        public void Shoot()
        {
            GameObject projectile = Instantiate(projectilePrefab, shootPoint.transform.position, playerCamera.transform.rotation);
            Rigidbody rb = projectile.GetComponent<Rigidbody>();
            if (rb != null) rb.AddForce(playerCamera.transform.forward * speed, ForceMode.Impulse);
        }
    }
}