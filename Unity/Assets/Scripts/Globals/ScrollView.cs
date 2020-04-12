using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CalorieCounter
{
    [RequireComponent(typeof(ScrollRect))]
    public class ScrollView : MonoBehaviour
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

        public Action RowChanged;

        public List<ScrollViewText> ScrollViewTexts { get; private set; } = new List<ScrollViewText>();

        public RectTransform RectTransform { get; private set; }
        public ScrollRect ScrollRect { get; private set; }
        public GridLayoutGroup Content { get; private set; }

        [SerializeField]
        private GameObject _scrollViewTextPrefab = default;

        // We need to manually keep track of content children because calling Destroy() takes a frame to update,
        // so using GetChild() won't return the expected results.
        protected List<GameObject> _contentChildren = new List<GameObject>();

        public virtual void DeleteRow(int rowIndex)
        {
            var childStartIndex = rowIndex * Content.constraintCount;
            for (var i = Content.constraintCount - 1; i >= 0; i--)
            {
                var childIndex = childStartIndex + i;
                var child = _contentChildren[childIndex];
                if (child.GetComponent<ScrollViewText>())
                {
                    var scrollViewText = child.GetComponent<ScrollViewText>();
                    ScrollViewTexts.Remove(scrollViewText);
                    TextModified?.Invoke(this, new TextModifiedEventArgs(TextModifiedType.Destroying, scrollViewText));
                }
                Destroy(child);
                _contentChildren.Remove(child);
            }
            InvokeRowChanged();
        }

        public GameObject InstantiateScrollViewText(int siblingIndex)
        {
            return InstantiateScrollViewText(_scrollViewTextPrefab, siblingIndex);
        }

        public GameObject InstantiateScrollViewText(GameObject scrollViewTextPrefab, int siblingIndex)
        {
            GameObject scrollViewTextGameObject = Instantiate(scrollViewTextPrefab);
            AddToScrollView(scrollViewTextGameObject.transform, siblingIndex);
            var scrollViewText = scrollViewTextGameObject.GetComponent<ScrollViewText>();
            ScrollViewTexts.Add(scrollViewText);
            TextModified?.Invoke(this, new TextModifiedEventArgs(TextModifiedType.Instantiated, scrollViewText));
            return scrollViewTextGameObject;
        }

        public void AddToScrollView(Transform transform)
        {
            transform.SetParent(Content.transform, false);
            _contentChildren.Add(transform.gameObject);
        }

        public void AddToScrollView(Transform transform, int siblingIndex)
        {
            transform.SetParent(Content.transform, false);
            transform.SetSiblingIndex(siblingIndex);
            _contentChildren.Insert(siblingIndex, transform.gameObject);
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

        protected void InvokeRowChanged()
        {
            RowChanged?.Invoke();
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
