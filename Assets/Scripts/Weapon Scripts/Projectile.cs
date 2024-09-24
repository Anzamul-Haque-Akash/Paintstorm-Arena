using Shaders;
using UnityEngine;

namespace Weapon_Scripts
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private float m_ProjectileSpeed;
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
                other.gameObject.GetComponent<VertexPaintController>()?.Paint(hit);
            }
            
            Destroy(gameObject);
        }
    }
}