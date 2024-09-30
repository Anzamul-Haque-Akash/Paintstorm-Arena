using Helper_Scripts;
using UnityEngine;

namespace Controllers
{
    public class ReloadController : MonoBehaviour
    {
        [SerializeField] private Animator m_RigLayerAnimator;
        [SerializeField] private GameObject m_BodyPod;
        [SerializeField] private GameObject m_HandPod;
        [SerializeField] private WeaponAnimationEvents m_WeaponAnimationEvents;

        private static readonly int IsReloading = Animator.StringToHash("isReloading");

        private void Awake()
        {
            m_HandPod.SetActive(false);
        }

        private void Start()
        {
            m_WeaponAnimationEvents.WeaponAnimationEvent.AddListener(OnAnimationEvent);
        }

        private void OnAnimationEvent(string eventName)
        {
            switch (eventName)
            {
                case "GrabPodAnimationEvent":
                    GrabPodAnimationEvent();
                    break;
                case "DetachPodAnimationEvent":
                    DetachPodAnimationEvent();
                    break;
            }
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.R)) Reload();
        }

        private void Reload()
        {
            m_RigLayerAnimator.SetTrigger(IsReloading);
        }

        private void GrabPodAnimationEvent()
        {
            m_BodyPod.SetActive(false);
            m_HandPod.SetActive(true);
        }

        private void DetachPodAnimationEvent()
        {
            m_HandPod.SetActive(false);
        }
    }
}