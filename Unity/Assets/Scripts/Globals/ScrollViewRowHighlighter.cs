using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CalorieCounter
{
    [RequireComponent(typeof(Image))]
    public class ScrollViewRowHighlighter : MonoBehaviour
    {
        public event EventHandler EnteredHighlightRow;

        public event EventHandler ExitedHighlightRow;

        public ScrollViewText HighlightedScrollViewText { get; private set; }

        public int HighlightedRowIndex { get; private set; }

        public bool IsRowHighlighted => HighlightedRowIndex != -1 && _contentRectTransform.childCount > 0;

        [SerializeField]
        private ScrollViewAssistant _scrollViewAssistant = default;

        private List<ScrollViewText> _scrollViewTexts = new List<ScrollViewText>();

        private Image _image;
        private RectTransform _rectTransform;
        private RectTransform _contentRectTransform;

        public void ExitHighlightRow()
        {
            HighlightedScrollViewText = null;
            HighlightedRowIndex = -1;
            _image.enabled = false;

            ExitedHighlightRow?.Invoke(this, EventArgs.Empty);
        }

        private void Awake()
        {
            _image = GetComponent<Image>();
            _rectTransform = GetComponent<RectTransform>();
        }

        private void Start()
        {
            foreach (var scrollViewText in _scrollViewAssistant.ScrollViewTexts)
            {
                scrollViewText.Highlighted += ScrollViewText_OnHighlighted;
                _scrollViewTexts.Add(scrollViewText);
            }
            _scrollViewAssistant.TextModified += ScrollView_OnTextModified;

            _contentRectTransform = _scrollViewAssistant.Content.GetComponent<RectTransform>();
            _rectTransform.sizeDelta = new Vector2(0, _scrollViewAssistant.Content.cellSize.y);
            ExitHighlightRow();
        }

        private void OnDestroy()
        {
            foreach(var scrollViewText in _scrollViewTexts)
            {
                scrollViewText.Highlighted -= ScrollViewText_OnHighlighted;
            }
            _scrollViewAssistant.TextModified -= ScrollView_OnTextModified;
        }

        private void ScrollView_OnTextModified(object sender, ScrollViewAssistant.TextModifiedEventArgs e)
        {
            if (e.TextModifiedType == TextModifiedType.Instantiated)
            {
                e.ScrollViewText.Highlighted += ScrollViewText_OnHighlighted;
                _scrollViewTexts.Add(e.ScrollViewText);
            }
            else if (e.TextModifiedType == TextModifiedType.Destroying)
            {
                e.ScrollViewText.Highlighted -= ScrollViewText_OnHighlighted;
                _scrollViewTexts.Remove(e.ScrollViewText);
            }
        }

        private void ScrollViewText_OnHighlighted(object sender, ScrollViewText.HighlightedEventArgs e)
        {
            if (e.HighlightedType == HighlightedType.Entered)
            {
                HighlightedScrollViewText = (ScrollViewText)sender;
                HighlightedRowIndex = e.SiblingIndex / _scrollViewAssistant.Content.constraintCount;
                _image.enabled = true;
                _rectTransform.anchoredPosition = new Vector2(0, (HighlightedRowIndex * _scrollViewAssistant.Content.cellSize.y * -1)
                    + _contentRectTransform.anchoredPosition.y);

                EnteredHighlightRow?.Invoke(this, EventArgs.Empty);
            }
            else if(e.HighlightedType == HighlightedType.Exited)
            {
                ExitHighlightRow();
            }
        }
    }
}
