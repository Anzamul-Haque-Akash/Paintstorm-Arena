using UnityEngine;

namespace Weapon_Scripts
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private float m_ProjectileSpeed;
        [SerializeField] private GameObject m_ProjectileHitDecal;
        private Rigidbody _rb;

        private void Start()
        {
            _rb = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            _rb.velocity = transform.forward * m_ProjectileSpeed;
        }

        private void OnCollisionEnter(Collision other)
        {
            if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, 10f))
            {
                Instantiate(m_ProjectileHitDecal, hit.point, Quaternion.LookRotation(hit.normal));
            }
            
            Destroy(gameObject);
        }
    }
}