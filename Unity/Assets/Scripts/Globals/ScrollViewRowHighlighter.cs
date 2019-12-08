using CalorieCounter.Managers;
using CalorieCounter.Utilities;
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
            _scrollView.TextModified += OnTextModified;
            GameManager.InputKeyManager.InputKeyPressed += OnInputKeyPressed;
        }

        private void OnDestroy()
        {
            foreach(var scrollViewText in _scrollViewTexts)
            {
                scrollViewText.Highlighted -= OnHighlighted;
            }
            GameManager.InputKeyManager.InputKeyPressed -= OnInputKeyPressed;
            _scrollView.TextModified -= OnTextModified;
        }

        private void OnTextModified(object sender, AbstractScrollView.TextModifiedEventArgs e)
        {
            if (e.TextModifiedType == TextModifiedType.Instantiated)
            {
                e.ScrollViewText.Highlighted += OnHighlighted;
                _scrollViewTexts.Add(e.ScrollViewText);
            }
            else if (e.TextModifiedType == TextModifiedType.Destroying)
            {
                e.ScrollViewText.Highlighted -= OnHighlighted;
                _scrollViewTexts.Remove(e.ScrollViewText);
            }
        }

        private void OnHighlighted(object sender, ScrollViewText.HighlightedEventArgs e)
        {
            if (e.HighlightedType == HighlightedType.Enter)
            {
                EnterHighlightRow(e.SiblingIndex);
            } else if(e.HighlightedType == HighlightedType.Exit)
            {
                ExitHighlightRow();
            }
        }

        private void OnInputKeyPressed(object sender, InputKeyManager.InputKeyPressedEventArgs e)
        {
            if (_highlightedRowIndex == -1 || _contentRectTransform.childCount <= 0)
                return;

            if (e.InputKeyCode == InputKeyCode.DeleteRow)
            {
                _scrollView.DeleteRow(_highlightedRowIndex);
                FindObjectOfType<InteractableHandler>()?.Execute(gameObject);
                ExitHighlightRow();
            }
            else if (e.InputKeyCode == InputKeyCode.RenameRow)
            {
                _scrollView.ShowRenameField(_highlightedRowIndex);
                ExitHighlightRow();
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
