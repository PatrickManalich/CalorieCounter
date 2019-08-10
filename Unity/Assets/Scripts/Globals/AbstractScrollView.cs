using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CalorieCounter
{
    [RequireComponent(typeof(ScrollRect))]
    public abstract class AbstractScrollView : MonoBehaviour
    {
        public class TextAddedEventArgs : EventArgs
        {
            public ScrollViewText ScrollViewText { get; private set; }

            public TextAddedEventArgs(ScrollViewText scrollViewText)
            {
                ScrollViewText = scrollViewText;
            }
        }

        public delegate void TextAddedEventHandler(object sender, TextAddedEventArgs e);
        public abstract event TextAddedEventHandler TextAddedEvent;

        protected abstract ScrollViewRowHighlighter ScrollViewRowHighlighter { get; }

        protected ScrollRect _scrollRect;
        protected GridLayoutGroup _content;

        public bool HasInputFields()
        {
            foreach (Transform child in _content.transform)
            {
                if (child.GetComponent<TMP_InputField>() != null)
                {
                    return true;
                }
            }
            return false;
        }

        public void ScrollToBottom()
        {
            StartCoroutine(ScrollToBottomCoroutine());
        }

        public void AddToScrollView(Transform transform)
        {
            transform.SetParent(_content.transform);
        }

        protected abstract void OnRowDestroyedEvent(object sender, ScrollViewRowHighlighter.RowDestroyedEventArgs e);

        private void Awake()
        {
            ScrollViewRowHighlighter.RowDestroyedEvent += OnRowDestroyedEvent;
        }

        private void Start()
        {
            _scrollRect = GetComponent<ScrollRect>();
            _content = _scrollRect.content.GetComponent<GridLayoutGroup>();
        }

        private void OnDestroy()
        {
            ScrollViewRowHighlighter.RowDestroyedEvent -= OnRowDestroyedEvent;
        }

        private IEnumerator ScrollToBottomCoroutine()
        {
            yield return new WaitForEndOfFrame();
            _scrollRect.verticalNormalizedPosition = 0;
        }
    }
}
