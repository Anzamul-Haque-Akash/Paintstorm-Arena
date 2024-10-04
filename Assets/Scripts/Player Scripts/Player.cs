using Helper_Scripts;
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
        
        private void Awake() => Instance = this;
    }
}