using CalorieCounter.Managers;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace CalorieCounter.Audio
{
    [RequireComponent(typeof(Selectable))]
    public class ClickSoundEffectPlayer : MonoBehaviour, IPointerDownHandler
    {
        private Selectable _selectable;

        public void OnPointerDown(PointerEventData eventData)
        {
            if (_selectable.interactable)
            {
                GameManager.SoundEffectsManager.PlaySuccessfulClickSoundEffect();
            }
            else
            {
                GameManager.SoundEffectsManager.PlayFailedClickSoundEffect();
            }
        }

        private void Awake()
        {
            _selectable = GetComponent<Selectable>();
        }
    }
}
