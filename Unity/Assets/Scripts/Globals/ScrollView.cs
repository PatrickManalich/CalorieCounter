using System;
using System.Collections.Generic;
using UnityEngine;

namespace CalorieCounter
{
    [RequireComponent(typeof(ScrollViewAssistant))]
    public class ScrollView : MonoBehaviour
    {
        public class RowChangedEventArgs : EventArgs
        {
            public int RowIndex { get; }

            public RowChangedEventArgs(int rowIndex)
            {
                RowIndex = rowIndex;
            }
        }

        public event EventHandler<RowChangedEventArgs> RowAdded;

        public event EventHandler<RowChangedEventArgs> RowRemoved;

        protected ScrollViewAssistant ScrollViewAssistant { get; private set; }

        public void AddToScrollView(List<Transform> transforms)
        {
            foreach (var transform in transforms)
            {
                ScrollViewAssistant.AddToScrollView(transform);
                transform.gameObject.SetActive(true);
            }
            ScrollViewAssistant.ScrollToBottom();
        }

        protected void InvokeRowAdded(int rowIndex)
        {
            RowAdded?.Invoke(this, new RowChangedEventArgs(rowIndex));
        }

        protected void InvokeRowRemoved(int rowIndex)
        {
            RowRemoved?.Invoke(this, new RowChangedEventArgs(rowIndex));
        }

        private void Awake()
        {
            ScrollViewAssistant = GetComponent<ScrollViewAssistant>();
        }
    }
}
