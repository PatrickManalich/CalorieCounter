using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CalorieCounter
{
    [RequireComponent(typeof(CanvasGroup))]
    public class ScrollViewRowHighlighter : MonoBehaviour
    {

        [SerializeField]
        private GridLayoutGroup _content = default;

        private List<ScrollViewText> _scrollViewTexts = new List<ScrollViewText>();

        private CanvasGroup _canvasGroup;
        private RectTransform _rectTransform;

        public void AddScrollViewText(ScrollViewText scrollViewText)
        {
            scrollViewText.HighlightEvent += OnHighlightEvent;
            _scrollViewTexts.Add(scrollViewText);
        }

        private void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
            _rectTransform = GetComponent<RectTransform>();
            _canvasGroup.alpha = 0;
        }

        private void OnDestroy()
        {
            foreach(var scrollViewText in _scrollViewTexts)
            {
                scrollViewText.HighlightEvent -= OnHighlightEvent;
            }
        }


        private void OnHighlightEvent(object sender, ScrollViewText.HighlightEventArgs e)
        {
            int rowIndex = e.SiblingIndex / _content.constraintCount;
            if (e.HighlightEventType == HighlightEventType.Enter)
            {
                _canvasGroup.alpha = 1;
                _rectTransform.anchoredPosition = new Vector2(0, rowIndex * _content.cellSize.y * -1);
            } else if(e.HighlightEventType == HighlightEventType.Exit)
            {
                _canvasGroup.alpha = 0;
            }
        }
}
}
