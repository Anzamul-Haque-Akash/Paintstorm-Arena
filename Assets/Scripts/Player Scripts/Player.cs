using UnityEngine;

namespace Player_Scripts
{
    public class Player : MonoBehaviour
    {
        public static Player Instance;
        
        [SerializeField] private PlayerInput m_PlayerInput;
        [SerializeField] private PlayerMovement m_PlayerMovement;
        [SerializeField] private PlayerAiming m_PlayerAiming;

        private void Start() => Instance = this;
    }
}
