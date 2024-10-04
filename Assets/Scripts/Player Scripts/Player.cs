using Helper_Scripts;
using Sirenix.OdinInspector;
using SOs;
using UnityEngine;

namespace Player_Scripts
{
    public class Player : MonoBehaviour
    {
        public static Player Instance;

        [field: SerializeField] public PlayerDataSo PlayerData { get; private set; }
        [field: SerializeField] public CharacterController CharacterController { get; private set; }
        [field: SerializeField] public Animator Animator { get; private set; }

        [field: SerializeField] public Animator RigAnimator { get; private set; }
        [field: SerializeField] public WeaponAnimationEvents WeaponAnimationEvents { get; private set; }
        [field: SerializeField] public GameObject BodyPod { get; private set; }
        [field: SerializeField] public GameObject HandPod { get; private set; }
        
        [ReadOnly] public bool m_IsJumping;
        [ReadOnly] public bool m_IsFalling;
        [ReadOnly] public bool m_IsReloading;
        
        private void Awake() => Instance = this;
    }
}