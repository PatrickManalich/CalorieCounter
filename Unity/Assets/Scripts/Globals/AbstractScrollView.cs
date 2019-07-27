using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CalorieCounter
{
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

        protected abstract GridLayoutGroup Content { get; }

        protected abstract ScrollViewRowHighlighter ScrollViewRowHighlighter { get; }

        public bool HasInputFields()
        {
            foreach (Transform child in Content.transform)
            {
                if (child.GetComponent<TMP_InputField>() != null)
                {
                    return true;
                }
            }
            return false;
        }

        protected abstract void OnRowDestroyedEvent(object sender, ScrollViewRowHighlighter.RowDestroyedEventArgs e);

        private void Awake()
        {
            ScrollViewRowHighlighter.RowDestroyedEvent += OnRowDestroyedEvent;
        }

        private void OnDestroy()
        {
            ScrollViewRowHighlighter.RowDestroyedEvent -= OnRowDestroyedEvent;
        }
    }
}
