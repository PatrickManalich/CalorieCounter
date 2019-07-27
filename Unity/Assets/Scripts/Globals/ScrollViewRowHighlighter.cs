using CalorieCounter.Utilities;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CalorieCounter
{
    [RequireComponent(typeof(CanvasGroup))]
    public class ScrollViewRowHighlighter : MonoBehaviour
    {
        public class RowDestroyedEventArgs : EventArgs
        {
            public int DestroyedRowIndex { get; private set; }

            public RowDestroyedEventArgs(int destroyedRowIndex)
            {
                DestroyedRowIndex = destroyedRowIndex;
            }
        }

        public delegate void RowDestroyedEventHandler(object sender, RowDestroyedEventArgs e);
        public event RowDestroyedEventHandler RowDestroyedEvent;

        [SerializeField]
        private KeyCode _deleteKeyCode = default;

        [SerializeField]
        private GridLayoutGroup _content = default;

        private List<ScrollViewText> _scrollViewTexts = new List<ScrollViewText>();

        private CanvasGroup _canvasGroup;
        private RectTransform _rectTransform;
        private int _highlightedRowIndex = -1;

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

        private void Update()
        {
            if (Input.GetKeyDown(_deleteKeyCode) && _highlightedRowIndex != -1)
            {
                int childStartIndex = _highlightedRowIndex * _content.constraintCount;
                for (int i = _content.constraintCount - 1; i >= 0 ; i--)
                {
                    int childIndex = childStartIndex + i;
                    _scrollViewTexts.RemoveAt(childIndex);
                    Destroy(_content.transform.GetChild(childIndex).gameObject);
                }
                FindObjectOfType<InteractableHandler>()?.Execute(gameObject);
                RowDestroyedEvent?.Invoke(this, new RowDestroyedEventArgs(_highlightedRowIndex));
            }
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
            if (e.HighlightEventType == HighlightEventType.Enter)
            {
                _highlightedRowIndex = e.SiblingIndex / _content.constraintCount;
                _canvasGroup.alpha = 1;
                var contentOffset = _content.GetComponent<RectTransform>().anchoredPosition.y;
                _rectTransform.anchoredPosition = new Vector2(0, (_highlightedRowIndex * _content.cellSize.y * -1) + contentOffset);
            } else if(e.HighlightEventType == HighlightEventType.Exit)
            {
                _highlightedRowIndex = -1;
                _canvasGroup.alpha = 0;
            }
        }
}
}
