using CalorieCounter.Managers;
using UnityEngine;
using UnityEngine.EventSystems;

namespace CalorieCounter.Audio
{
    public class PressInputKeySoundEffectPlayer : MonoBehaviour
    {
        private void Start()
        {
            GameManager.InputKeyManager.InputKeyPressed += InputKeyManager_OnInputKeyPressed;
        }

        private void OnDestroy()
        {
            GameManager.InputKeyManager.InputKeyPressed -= InputKeyManager_OnInputKeyPressed;
        }

        private void InputKeyManager_OnInputKeyPressed(object sender, InputKeyManager.InputKeyPressedEventArgs e)
        {
            if (EventSystem.current.currentSelectedGameObject == null)
            {
                GameManager.SoundEffectsManager.PlayPressInputKeySoundEffect();
            }
        }
    }
}
