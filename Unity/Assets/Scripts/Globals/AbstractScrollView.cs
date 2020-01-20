using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CalorieCounter
{
    [RequireComponent(typeof(ScrollRect))]
    public abstract class AbstractScrollView : MonoBehaviour
    {
        public class TextModifiedEventArgs : EventArgs
        {
            public TextModifiedType TextModifiedType { get; private set; }

            public ScrollViewText ScrollViewText { get; private set; }

            public TextModifiedEventArgs(TextModifiedType textModifiedType, ScrollViewText scrollViewText)
            {
                TextModifiedType = textModifiedType;
                ScrollViewText = scrollViewText;
            }
        }

        public delegate void TextModifiedEventHandler(object sender, TextModifiedEventArgs e);
        public event TextModifiedEventHandler TextModified;

        [SerializeField]
        private GameObject _scrollViewTextPrefab = default;

        protected ScrollRect _scrollRect;
        protected GridLayoutGroup _content;

        // We need to manually keep track of content children because calling Destroy() takes a frame to update,
        // so using GetChild() won't return the expected results.
        protected List<GameObject> _contentChildren = new List<GameObject>();

        public virtual void ShowRenameField(int rowIndex) { }

        public virtual void AcceptSuggestion(int rowIndex) { }

        public virtual void DeleteRow(int rowIndex)
        {
            var childStartIndex = rowIndex * _content.constraintCount;
            for (var i = _content.constraintCount - 1; i >= 0; i--)
            {
                var childIndex = childStartIndex + i;
                var child = _contentChildren[childIndex];
                if (child.GetComponent<ScrollViewText>())
                {
                    TextModified?.Invoke(this, new TextModifiedEventArgs(TextModifiedType.Destroying, child.GetComponent<ScrollViewText>()));
                }
                Destroy(child);
                _contentChildren.Remove(child);
            }
        }

        public GameObject InstantiateScrollViewText()
        {
            GameObject scrollViewText = Instantiate(_scrollViewTextPrefab);
            AddToScrollView(scrollViewText.transform);
            TextModified?.Invoke(this, new TextModifiedEventArgs(TextModifiedType.Instantiated, scrollViewText.GetComponent<ScrollViewText>()));
            return scrollViewText;
        }

        public GameObject InstantiateScrollViewText(int siblingIndex)
        {
            return InstantiateScrollViewText(_scrollViewTextPrefab, siblingIndex);
        }

        public GameObject InstantiateScrollViewText(GameObject scrollViewTextPrefab, int siblingIndex)
        {
            GameObject scrollViewText = Instantiate(scrollViewTextPrefab);
            AddToScrollView(scrollViewText.transform, siblingIndex);
            TextModified?.Invoke(this, new TextModifiedEventArgs(TextModifiedType.Instantiated, scrollViewText.GetComponent<ScrollViewText>()));
            return scrollViewText;
        }

        public void AddToScrollView(Transform transform)
        {
            transform.SetParent(_content.transform, false);
            _contentChildren.Add(transform.gameObject);
        }

        public void AddToScrollView(Transform transform, int siblingIndex)
        {
            transform.SetParent(_content.transform, false);
            transform.SetSiblingIndex(siblingIndex);
            _contentChildren.Insert(siblingIndex, transform.gameObject);
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

        private void Awake()
        {
            _scrollRect = GetComponent<ScrollRect>();
            _content = _scrollRect.content.GetComponent<GridLayoutGroup>();
        }

        private IEnumerator ScrollToPercentCoroutine (float percent)
        {
            yield return new WaitForEndOfFrame();
            _scrollRect.verticalNormalizedPosition = Mathf.Clamp01(percent);
        }
    }
}
