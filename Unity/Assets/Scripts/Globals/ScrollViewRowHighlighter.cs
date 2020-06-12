using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CalorieCounter
{
    [RequireComponent(typeof(Image))]
    public class ScrollViewRowHighlighter : MonoBehaviour
    {
        public int HighlightedRowIndex { get; private set; }

        public bool RowHighlighted => HighlightedRowIndex != -1 && _contentRectTransform.childCount > 0;

        [SerializeField]
        private GridLayoutGroup _content = default;

        [SerializeField]
        private ScrollView _scrollView = default;

        private List<ScrollViewText> _scrollViewTexts = new List<ScrollViewText>();

        private Image _image;
        private RectTransform _rectTransform;
        private RectTransform _contentRectTransform;

        public void EnterHighlightRow(int siblingIndex)
        {
            HighlightedRowIndex = siblingIndex / _content.constraintCount;
            _image.enabled = true;
            var contentOffset = _contentRectTransform.anchoredPosition.y;
            _rectTransform.anchoredPosition = new Vector2(0, (HighlightedRowIndex * _content.cellSize.y * -1) + contentOffset);
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
            _contentRectTransform = _content.GetComponent<RectTransform>();

            _rectTransform.sizeDelta = new Vector2(0, _content.cellSize.y);
            ExitHighlightRow();
        }

        private void Start()
        {
            foreach (var scrollViewText in _scrollView.ScrollViewTexts)
            {
                scrollViewText.Highlighted += ScrollViewText_OnHighlighted;
                _scrollViewTexts.Add(scrollViewText);
            }
            _scrollView.TextModified += ScrollView_OnTextModified;
        }

        private void OnDestroy()
        {
            foreach(var scrollViewText in _scrollViewTexts)
            {
                scrollViewText.Highlighted -= ScrollViewText_OnHighlighted;
            }
            _scrollView.TextModified -= ScrollView_OnTextModified;
        }

        private void ScrollView_OnTextModified(object sender, ScrollView.TextModifiedEventArgs e)
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
