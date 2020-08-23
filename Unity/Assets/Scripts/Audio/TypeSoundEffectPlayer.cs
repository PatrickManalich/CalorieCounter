using CalorieCounter.Managers;
using UnityEngine;
using UnityEngine.EventSystems;

namespace CalorieCounter.Audio
{
    public class TypeSoundEffectPlayer : MonoBehaviour
    {
        private void Update()
        {
            if (EventSystem.current.currentSelectedGameObject != null && EventSystem.current.currentSelectedGameObject == gameObject && Input.anyKeyDown)
            {
                GameManager.SoundEffectsManager.PlayTypeSoundEffect();
            }
        }
    }
}
