using UnityEngine;

namespace Constants
{
    public static class AnimatorHashes
    {
        public static readonly int InputX = Animator.StringToHash("InputX");
        public static readonly int InputY = Animator.StringToHash("InputY");
        public static readonly int IsJumping = Animator.StringToHash("isJumping");
        public static readonly int IsShooting = Animator.StringToHash("isShooting");
        public static readonly int IsReloading = Animator.StringToHash("isReloading");
    }
}
