using Constants;
using Player_Scripts;

namespace State_Machine.States
{
    public class PlayerReloadState : PlayerBaseState
    {
        private PlayerStateManager _playerStateManager;
        public override void EnterState(PlayerStateManager playerStateManager)
        {
            _playerStateManager = playerStateManager;

            _playerStateManager.m_IsReloading = true;
            
            Player.Instance.WeaponAnimationEvents.WeaponAnimationEvent.AddListener(OnAnimationEvent);

            Reload();
        }
        public override void UpdateState() { }

        public override void FixedUpdateState() { }
        
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
                case "ReloadComplete":
                    ReloadComplete();
                    break;
            }
        }
        
        private void Reload()
        {
            Player.Instance.RigAnimator.SetTrigger(AnimatorHashes.IsReloading);
        }

        private void GrabPodAnimationEvent()
        {
            Player.Instance.BodyPod.SetActive(false);
            Player.Instance.HandPod.SetActive(true);
        }

        private void DetachPodAnimationEvent()
        {
            Player.Instance.HandPod.SetActive(false);
        }

        private void ReloadComplete()
        {
            _playerStateManager.m_IsReloading = false;
        }
    }
}
