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
        public event TextAddedEventHandler TextAddedEvent;

        protected abstract ScrollViewRowHighlighter ScrollViewRowHighlighter { get; }


        [SerializeField]
        private GameObject _scrollViewTextPrefab = default;

        protected ScrollRect _scrollRect;
        protected GridLayoutGroup _content;

        public GameObject InstantiateScrollViewText()
        {
            GameObject scrollViewText = Instantiate(_scrollViewTextPrefab);
            AddToScrollView(scrollViewText.transform);
            TextAddedEvent?.Invoke(this, new TextAddedEventArgs(scrollViewText.GetComponent<ScrollViewText>()));
            return scrollViewText;
        }

        public void AddToScrollView(Transform transform)
        {
            transform.SetParent(_content.transform);
        }

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

        public void ScrollToTop()
        {
            StartCoroutine(ScrollToPercentCoroutine(1));
        }

        public void ScrollToBottom()
        {
            StartCoroutine(ScrollToPercentCoroutine(0));
        }

        public void ScrollToPercent(float percent)
        {
            StartCoroutine(ScrollToPercentCoroutine(percent));
        }

        protected abstract void OnRowDestroyedEvent(object sender, ScrollViewRowHighlighter.RowDestroyedEventArgs e);

        private void Start()
        {
            _scrollRect = GetComponent<ScrollRect>();
            _content = _scrollRect.content.GetComponent<GridLayoutGroup>();
            ScrollViewRowHighlighter.RowDestroyedEvent += OnRowDestroyedEvent;
        }

        private void OnDestroy()
        {
            ScrollViewRowHighlighter.RowDestroyedEvent -= OnRowDestroyedEvent;
        }

        private IEnumerator ScrollToPercentCoroutine (float percent)
        {
            yield return new WaitForEndOfFrame();
            _scrollRect.verticalNormalizedPosition = Mathf.Clamp01(percent);
        }
    }
}
