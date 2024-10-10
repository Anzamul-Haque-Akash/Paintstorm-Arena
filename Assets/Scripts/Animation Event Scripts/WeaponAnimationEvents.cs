using UnityEngine;
using UnityEngine.Events;

namespace Animation_Event_Scripts
{
    public class AnimationEvent : UnityEvent<string> { }
    
    public class WeaponAnimationEvents : MonoBehaviour
    {
        public readonly AnimationEvent WeaponAnimationEvent = new AnimationEvent();

        public void OnAnimationEvent(string eventName)
        {
            WeaponAnimationEvent?.Invoke(eventName);
        }
    }
}
