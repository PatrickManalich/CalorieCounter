using System;
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

        private Queue<Action> _actionQueue = new Queue<Action>();
        private bool _isBlocking;

        [SerializeField]
        private GameObject _scrollViewTextPrefab = default;

        public void InstantiateScrollViewText(int siblingIndex, string text)
        {
            InstantiateScrollViewText(_scrollViewTextPrefab, siblingIndex, text);
        }

        public void InstantiateScrollViewText(GameObject scrollViewTextPrefab, int siblingIndex, string text)
        {
            void InstantiateAction()
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
            _actionQueue.Enqueue(InstantiateAction);
        }

        public void AddToScrollView(Transform transformToAdd)
        {
            void AddAction()
            {
                transformToAdd.SetParent(Content.transform, false);
                ContentChildren.Add(transformToAdd.gameObject);
            }
            _actionQueue.Enqueue(AddAction);
        }

        public void RemoveRow(int rowIndex)
        {
            void DestroyAction()
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
            _actionQueue.Enqueue(DestroyAction);
            _actionQueue.Enqueue(BlockAction);
        }

        public void ScrollToTop()
        {
            ScrollToPercent(1);
        }

        public void ScrollToBottom()
        {
            ScrollToPercent(0);
        }

        public void ScrollToPercent(float percent)
        {
            _actionQueue.Enqueue(BlockAction);
            void ScrollAction()
            {
                ScrollRect.verticalNormalizedPosition = Mathf.Clamp01(percent);
            }
            _actionQueue.Enqueue(ScrollAction);
        }

        private void Awake()
        {
            RectTransform = GetComponent<RectTransform>();
            ScrollRect = GetComponent<ScrollRect>();
            Content = ScrollRect.content.GetComponent<GridLayoutGroup>();
        }

        private void Update()
        {
            while (_actionQueue.Count > 0)
            {
                if (_isBlocking)
                {
                    _isBlocking = false;
                    return;
                }
                else
                {
                    _actionQueue.Dequeue()?.Invoke();
                }
            }
        }

        private void BlockAction()
        {
            _isBlocking = true;
        }
    }
}
