using SOs;
using UnityEngine;

namespace Player_Scripts
{
    public class Player : MonoBehaviour
    {
        public static Player Instance;

        [field: SerializeField] public PlayerDataSo PlayerData { get; private set; }
        [field: SerializeField] public CharacterController m_PlayerCharacterController;

        private void Awake() => Instance = this;
    }
}