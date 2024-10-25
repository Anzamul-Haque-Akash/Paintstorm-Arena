using System;
using UnityEngine;

namespace Player_Scripts
{
    public class PlayerAnimationController : MonoBehaviour
    {
        private void Update()
        {
            SetAnimationLayerWeight();
        }

        private void SetAnimationLayerWeight()
        {
            Player.Instance.m_AnimatorWeight = Mathf.Lerp(Player.Instance.m_AnimatorWeight, Player.Instance.m_IsCrouching ? 1 : 0, Time.deltaTime * Player.Instance.PlayerData.m_CrouchSpeed);
            Player.Instance.Animator.SetLayerWeight(1, Player.Instance.m_AnimatorWeight);
        }
    }
}
