using SOs;
using UnityEngine;

namespace Player_Scripts
{
    public class Player : MonoBehaviour
    {
        public static Player Instance;

        [field: SerializeField] public PlayerDataSo PlayerData { get; private set; }

        private void Awake() => Instance = this;
    }
}