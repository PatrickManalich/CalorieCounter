using CalorieCounter.Managers;
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
        private GridLayoutGroup _content = default;

        [SerializeField]
        private AbstractScrollView _scrollView = default;

        private List<ScrollViewText> _scrollViewTexts = new List<ScrollViewText>();

        private CanvasGroup _canvasGroup;
        private RectTransform _rectTransform;
        private RectTransform _contentRectTransform;
        private int _highlightedRowIndex;

        private void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
            _rectTransform = GetComponent<RectTransform>();
            _contentRectTransform = _content.GetComponent<RectTransform>();

            _rectTransform.sizeDelta = new Vector2(0, _content.cellSize.y);
            ExitHighlightRow();
        }

        private void Start()
        {
            _scrollView.TextAddedEvent += OnTextAddedEvent;
            GameManager.InputKeyManager.InputKeyPressedEvent += OnInputKeyPressed;
        }

        private void OnDestroy()
        {
            foreach(var scrollViewText in _scrollViewTexts)
            {
                scrollViewText.HighlightedEvent -= OnHighlightedEvent;
            }
            GameManager.InputKeyManager.InputKeyPressedEvent -= OnInputKeyPressed;
            _scrollView.TextAddedEvent -= OnTextAddedEvent;
        }

        private void OnTextAddedEvent(object sender, AbstractScrollView.TextAddedEventArgs e)
        {
            e.ScrollViewText.HighlightedEvent += OnHighlightedEvent;
            _scrollViewTexts.Add(e.ScrollViewText);
        }

        private void OnHighlightedEvent(object sender, ScrollViewText.HighlightedEventArgs e)
        {
            if (e.HighlightedEventType == HighlightedEventType.Enter)
            {
                EnterHighlightRow(e.SiblingIndex);
            } else if(e.HighlightedEventType == HighlightedEventType.Exit)
            {
                ExitHighlightRow();
            }
        }

        private void OnInputKeyPressed(object sender, InputKeyManager.InputKeyPressedEventArgs e)
        {
            if (e.InputKeyCode == InputKeyCode.DeleteRow)
            {
                if (_highlightedRowIndex != -1 && _contentRectTransform.childCount > 0)
                {
                    int childStartIndex = _highlightedRowIndex * _content.constraintCount;
                    for (int i = _content.constraintCount - 1; i >= 0; i--)
                    {
                        int childIndex = childStartIndex + i;
                        _scrollViewTexts.RemoveAt(childIndex);
                        Destroy(_contentRectTransform.GetChild(childIndex).gameObject);
                    }
                    FindObjectOfType<InteractableHandler>()?.Execute(gameObject);
                    RowDestroyedEvent?.Invoke(this, new RowDestroyedEventArgs(_highlightedRowIndex));

                    if (_scrollViewTexts.Count == 0 || _highlightedRowIndex >= _scrollViewTexts.Count / _content.constraintCount)
                    {
                        ExitHighlightRow();
                    }
                }
            }
        }

        private void EnterHighlightRow(int siblingIndex)
        {
            _highlightedRowIndex = siblingIndex / _content.constraintCount;
            _canvasGroup.alpha = 1;
            var contentOffset = _contentRectTransform.anchoredPosition.y;
            _rectTransform.anchoredPosition = new Vector2(0, (_highlightedRowIndex * _content.cellSize.y * -1) + contentOffset);
        }

        private void ExitHighlightRow()
        {
            _highlightedRowIndex = -1;
            _canvasGroup.alpha = 0;
        }

    }
}
