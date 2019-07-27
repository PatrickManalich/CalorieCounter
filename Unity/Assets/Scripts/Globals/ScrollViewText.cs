using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace CalorieCounter
{
    public class ScrollViewText : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {

        public class HighlightedEventArgs : EventArgs
        {
            public HighlightedEventType HighlightedEventType { get; private set; }
            public int SiblingIndex { get; private set; }

            public HighlightedEventArgs(HighlightedEventType highlightedEventType, int siblingIndex)
            {
                HighlightedEventType = highlightedEventType;
                SiblingIndex = siblingIndex;
            }
        }

        public delegate void HighlightedEventHandler(object sender, HighlightedEventArgs e);
        public event HighlightedEventHandler HighlightedEvent;

        public void OnPointerEnter(PointerEventData eventData)
        {
            HighlightedEvent?.Invoke(this, new HighlightedEventArgs(HighlightedEventType.Enter, transform.GetSiblingIndex()));
        }

        public void OnPointerExit(PointerEventData pointerEventData)
        {
            HighlightedEvent?.Invoke(this, new HighlightedEventArgs(HighlightedEventType.Exit, transform.GetSiblingIndex()));
        }
    }
}
