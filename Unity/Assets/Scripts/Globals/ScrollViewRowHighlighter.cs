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
            foreach (var scrollViewText in _scrollView.ScrollViewTexts)
            {
                scrollViewText.Highlighted += ScrollViewText_OnHighlighted;
                _scrollViewTexts.Add(scrollViewText);
            }
            _scrollView.TextModified += ScrollView_OnTextModified;
            GameManager.InputKeyManager.InputKeyPressed += InputKeyManager_OnInputKeyPressed;
        }

        private void OnDestroy()
        {
            foreach(var scrollViewText in _scrollViewTexts)
            {
                scrollViewText.Highlighted -= ScrollViewText_OnHighlighted;
            }
            GameManager.InputKeyManager.InputKeyPressed -= InputKeyManager_OnInputKeyPressed;
            _scrollView.TextModified -= ScrollView_OnTextModified;
        }

        private void ScrollView_OnTextModified(object sender, AbstractScrollView.TextModifiedEventArgs e)
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

        private void InputKeyManager_OnInputKeyPressed(object sender, InputKeyManager.InputKeyPressedEventArgs e)
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
            else if(e.InputKeyCode == InputKeyCode.AcceptSuggestion)
            {
                _scrollView.AcceptSuggestion(_highlightedRowIndex);
                FindObjectOfType<InteractableHandler>()?.Execute(gameObject);
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
