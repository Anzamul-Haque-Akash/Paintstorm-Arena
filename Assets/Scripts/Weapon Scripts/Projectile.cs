using System;
using UnityEngine;

namespace Weapon_Scripts
{
    public class Projectile : MonoBehaviour
    {
        public float m_ProjectileSpeed;
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
            Debug.Log("Hit on " + other.transform.name);
        }
    }
}
