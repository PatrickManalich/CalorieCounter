using UnityEngine;
using UnityEngine.UI;

namespace CalorieCounter
{
    [RequireComponent(typeof(Scrollbar))]
    public class ScrollbarInteractableToggler : MonoBehaviour
	{
        [SerializeField]
        private ScrollViewAssistant _scrollViewAssistant = default;

        private Scrollbar _scrollbar;

		private void Start()
		{
            _scrollbar = GetComponent<Scrollbar>();
            _scrollbar.interactable = false;

            _scrollViewAssistant.TextModified += (object sender, ScrollViewAssistant.TextModifiedEventArgs e) => Refresh();

            Refresh();
        }

        private void OnDestroy()
        {
            _scrollViewAssistant.TextModified -= (object sender, ScrollViewAssistant.TextModifiedEventArgs e) => Refresh();
        }

        private void Refresh()
        {
            if (_scrollViewAssistant.ScrollViewTexts.Count <= 0)
            {
                _scrollbar.interactable = false;
            }
            else
            {
                var scrollViewTextsHeight = _scrollViewAssistant.ScrollViewTexts.Count / _scrollViewAssistant.Content.constraintCount * _scrollViewAssistant.Content.cellSize.y;
                var scrollViewRectTransformHeight = _scrollViewAssistant.RectTransform.sizeDelta.y;
                _scrollbar.interactable = scrollViewTextsHeight > scrollViewRectTransformHeight;
            }
        }
    }
}

