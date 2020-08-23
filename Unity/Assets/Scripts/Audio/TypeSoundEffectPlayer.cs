using CalorieCounter.Managers;
using TMPro;
using UnityEngine;

namespace CalorieCounter.Audio
{
    [RequireComponent(typeof(TMP_InputField))]
    public class TypeSoundEffectPlayer : MonoBehaviour
    {
        private TMP_InputField _inputField;

        private void Awake()
        {
            _inputField = GetComponent<TMP_InputField>();
        }

        private void Update()
        {
            if (_inputField.isFocused && Input.anyKeyDown)
            {
                GameManager.SoundEffectsManager.PlayTypeSoundEffect();
            }
        }
    }
}
