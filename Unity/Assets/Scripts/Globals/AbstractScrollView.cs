﻿using System;
using System.Collections;
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
            public TextModifiedEventType TextModifiedEventType { get; private set; }

            public ScrollViewText ScrollViewText { get; private set; }

            public TextModifiedEventArgs(TextModifiedEventType textModifiedEventType, ScrollViewText scrollViewText)
            {
                TextModifiedEventType = textModifiedEventType;
                ScrollViewText = scrollViewText;
            }
        }

        public delegate void TextModifiedEventHandler(object sender, TextModifiedEventArgs e);
        public event TextModifiedEventHandler TextModified;

        [SerializeField]
        private GameObject _scrollViewTextPrefab = default;

        protected ScrollRect _scrollRect;
        protected GridLayoutGroup _content;

        public virtual void RenameRow(int rowIndex) { }

        public virtual void DeleteRow(int rowIndex)
        {
            var childStartIndex = rowIndex * _content.constraintCount;
            for (var i = _content.constraintCount - 1; i >= 0; i--)
            {
                var childIndex = childStartIndex + i;
                DestroyFromScrollView(childIndex);
            }
        }

        public GameObject InstantiateScrollViewText()
        {
            GameObject scrollViewText = Instantiate(_scrollViewTextPrefab);
            AddToScrollView(scrollViewText.transform);
            TextModified?.Invoke(this, new TextModifiedEventArgs(TextModifiedEventType.Instantiated, scrollViewText.GetComponent<ScrollViewText>()));
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

        protected void DestroyFromScrollView(int childIndex)
        {
            var child = _content.transform.GetChild(childIndex).gameObject;
            if (child.GetComponent<ScrollViewText>())
            {
                TextModified?.Invoke(this, new TextModifiedEventArgs(TextModifiedEventType.Destroying, child.GetComponent<ScrollViewText>()));
            }
            Destroy(child);
        }

        private void Start()
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
