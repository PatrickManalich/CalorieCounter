using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CalorieCounter
{
    [RequireComponent(typeof(ScrollRect))]
    public class ScrollViewAssistant : MonoBehaviour
    {
        public class TextModifiedEventArgs : EventArgs
        {
            public TextModifiedType TextModifiedType { get; }

            public ScrollViewText ScrollViewText { get; }

            public TextModifiedEventArgs(TextModifiedType textModifiedType, ScrollViewText scrollViewText)
            {
                TextModifiedType = textModifiedType;
                ScrollViewText = scrollViewText;
            }
        }

        public event EventHandler<TextModifiedEventArgs> TextModified;

        public List<ScrollViewText> ScrollViewTexts { get; private set; } = new List<ScrollViewText>();

        public RectTransform RectTransform { get; private set; }
        public ScrollRect ScrollRect { get; private set; }
        public GridLayoutGroup Content { get; private set; }

        // We need to manually keep track of content children because calling Destroy() takes a frame to update,
        // so using GetChild() won't return the expected results.
        public List<GameObject> ContentChildren { get; private set; } = new List<GameObject>();

        [SerializeField]
        private GameObject _scrollViewTextPrefab = default;

        public void InstantiateScrollViewText(int siblingIndex, string text)
        {
            InstantiateScrollViewText(_scrollViewTextPrefab, siblingIndex, text);
        }

        public void InstantiateScrollViewText(GameObject scrollViewTextPrefab, int siblingIndex, string text)
        {
            var scrollViewTextTransform = Instantiate(scrollViewTextPrefab).transform;
            scrollViewTextTransform.SetParent(Content.transform, false);
            scrollViewTextTransform.SetSiblingIndex(siblingIndex);
            ContentChildren.Insert(siblingIndex, scrollViewTextTransform.gameObject);
            var scrollViewText = scrollViewTextTransform.GetComponent<ScrollViewText>();
            scrollViewText.Text.text = text;
            ScrollViewTexts.Add(scrollViewText);
            TextModified?.Invoke(this, new TextModifiedEventArgs(TextModifiedType.Instantiated, scrollViewText));
        }

        public void AddToScrollView(Transform transformToAdd)
        {
            transformToAdd.SetParent(Content.transform, false);
            ContentChildren.Add(transformToAdd.gameObject);
        }

        public void RemoveRow(int rowIndex)
        {
            var childStartIndex = rowIndex * Content.constraintCount;
            for (var i = Content.constraintCount - 1; i >= 0; i--)
            {
                var childIndex = childStartIndex + i;
                var child = ContentChildren[childIndex];
                if (child.GetComponent<ScrollViewText>())
                {
                    var scrollViewText = child.GetComponent<ScrollViewText>();
                    ScrollViewTexts.Remove(scrollViewText);
                    TextModified?.Invoke(this, new TextModifiedEventArgs(TextModifiedType.Destroying, scrollViewText));
                }
                Destroy(child);
                ContentChildren.Remove(child);
            }
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

        private void Awake()
        {
            RectTransform = GetComponent<RectTransform>();
            ScrollRect = GetComponent<ScrollRect>();
            Content = ScrollRect.content.GetComponent<GridLayoutGroup>();
        }

        private IEnumerator ScrollToPercentCoroutine (float percent)
        {
            yield return new WaitForEndOfFrame();
            ScrollRect.verticalNormalizedPosition = Mathf.Clamp01(percent);
        }
    }
}
