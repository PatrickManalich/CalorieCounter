using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace CalorieCounter
{
    public class ScrollViewText : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {

        public class HighlightEventArgs : EventArgs
        {
            public HighlightEventType HighlightEventType { get; private set; }
            public int SiblingIndex { get; private set; }

            public HighlightEventArgs(HighlightEventType highlightEventType, int siblingIndex)
            {
                HighlightEventType = highlightEventType;
                SiblingIndex = siblingIndex;
            }
        }

        public delegate void HighlightEventHandler(object sender, HighlightEventArgs e);
        public event HighlightEventHandler HighlightEvent;

        public void OnPointerEnter(PointerEventData eventData)
        {
            HighlightEvent?.Invoke(this, new HighlightEventArgs(HighlightEventType.Enter, transform.GetSiblingIndex()));
        }

        public void OnPointerExit(PointerEventData pointerEventData)
        {
            HighlightEvent?.Invoke(this, new HighlightEventArgs(HighlightEventType.Exit, transform.GetSiblingIndex()));
        }
    }
}
