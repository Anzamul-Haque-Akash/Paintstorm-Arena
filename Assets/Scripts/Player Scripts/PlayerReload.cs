using System;
using UnityEngine;

namespace Player_Scripts
{
    public class PlayerReload : MonoBehaviour
    {
        [SerializeField] private Animator m_RigLayerAnimator;
        [SerializeField] private GameObject m_BodyPod;
        [SerializeField] private GameObject m_HandPod;
        
        private static readonly int IsReloading = Animator.StringToHash("isReloading");

        private void Awake()
        {
            m_HandPod.SetActive(false);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.R)) Reload();
        }

        private void Reload()
        {
            m_RigLayerAnimator.SetTrigger(IsReloading);
        }

        public void SetPodParentAnimationEvent()
        {
            m_BodyPod.SetActive(false);
            m_HandPod.SetActive(true);
        }
        
        public void DetachPodAnimationEvent()
        {
            m_BodyPod.SetActive(true);
            m_HandPod.SetActive(false);
        }

    }
}
