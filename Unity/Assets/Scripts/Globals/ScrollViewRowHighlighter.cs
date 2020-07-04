﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CalorieCounter
{
    [RequireComponent(typeof(Image))]
    public class ScrollViewRowHighlighter : MonoBehaviour
    {
        public int HighlightedRowIndex { get; private set; }

        public bool IsRowHighlighted => HighlightedRowIndex != -1 && _contentRectTransform.childCount > 0;

        [SerializeField]
        private ScrollViewAssistant _scrollViewAssistant = default;

        private List<ScrollViewText> _scrollViewTexts = new List<ScrollViewText>();

        private Image _image;
        private RectTransform _rectTransform;
        private RectTransform _contentRectTransform;

        public void EnterHighlightRow(int siblingIndex)
        {
            HighlightedRowIndex = siblingIndex / _scrollViewAssistant.Content.constraintCount;
            _image.enabled = true;
            var contentOffset = _contentRectTransform.anchoredPosition.y;
            _rectTransform.anchoredPosition = new Vector2(0, (HighlightedRowIndex * _scrollViewAssistant.Content.cellSize.y * -1) + contentOffset);
        }

        public void ExitHighlightRow()
        {
            HighlightedRowIndex = -1;
            _image.enabled = false;
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
                EnterHighlightRow(e.SiblingIndex);
            } else if(e.HighlightedType == HighlightedType.Exited)
            {
                ExitHighlightRow();
            }
        }
    }
}
