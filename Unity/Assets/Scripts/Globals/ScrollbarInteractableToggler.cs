using UnityEngine;
using UnityEngine.UI;

namespace CalorieCounter
{
    [RequireComponent(typeof(Scrollbar))]
    public class ScrollbarInteractableToggler : MonoBehaviour
	{
        [SerializeField]
        private AbstractScrollView _scrollView = default;

        private Scrollbar _scrollbar;

		private void Start()
		{
            _scrollbar = GetComponent<Scrollbar>();
            _scrollbar.interactable = false;

            _scrollView.TextModified += (object sender, AbstractScrollView.TextModifiedEventArgs e) => Refresh();

            Refresh();
        }

        private void OnDestroy()
        {
            _scrollView.TextModified -= (object sender, AbstractScrollView.TextModifiedEventArgs e) => Refresh();
        }

        private void Refresh()
        {
            if (_scrollView.ScrollViewTexts.Count <= 0)
            {
                _scrollbar.interactable = false;
            }
            else
            {
                var scrollViewTextsHeight = _scrollView.ScrollViewTexts.Count / _scrollView.Content.constraintCount * _scrollView.Content.cellSize.y;
                var scrollViewRectTransformHeight = _scrollView.RectTransform.sizeDelta.y;
                _scrollbar.interactable = scrollViewTextsHeight > scrollViewRectTransformHeight;
            }
        }
    }
}

