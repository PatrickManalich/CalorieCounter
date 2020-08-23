using CalorieCounter.Managers;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace CalorieCounter.Audio
{
    [RequireComponent(typeof(Scrollbar))]
    public class ScrollSoundEffectPlayer : MonoBehaviour, IDragHandler
    {
        private Scrollbar _scrollbar = default;

        private float _topThreshold;
        private float _bottomThreshold;

        public void OnDrag(PointerEventData eventData)
        {
            if(_scrollbar.value > _topThreshold)
            {
                _bottomThreshold = _topThreshold;
                _topThreshold = Mathf.Min(_topThreshold + _scrollbar.size, 1);
                GameManager.SoundEffectsManager.PlayScrollSoundEffect();
            }
            else if(_scrollbar.value < _bottomThreshold)
            {
                _topThreshold = _bottomThreshold;
                _bottomThreshold = Mathf.Max(_bottomThreshold - _scrollbar.size, 0);
                GameManager.SoundEffectsManager.PlayScrollSoundEffect();
            }
        }

        private void Awake()
        {
            _scrollbar = GetComponent<Scrollbar>();
        }

        private IEnumerator Start()
        {
            // Wait one frame so scrollbar size can be set
            yield return null;
            _topThreshold = 1;
            _bottomThreshold = Mathf.Max(1 - _scrollbar.size, 0);
        }
    }
}
