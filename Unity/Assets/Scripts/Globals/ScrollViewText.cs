﻿using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace CalorieCounter
{
    public class ScrollViewText : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {

        public class HighlightedEventArgs : EventArgs
        {
            public HighlightedType HighlightedType { get; }
            public int SiblingIndex { get; }

            public HighlightedEventArgs(HighlightedType highlightedType, int siblingIndex)
            {
                HighlightedType = highlightedType;
                SiblingIndex = siblingIndex;
            }
        }

        public event EventHandler<HighlightedEventArgs> Highlighted;

        public void OnPointerEnter(PointerEventData eventData)
        {
            Highlighted?.Invoke(this, new HighlightedEventArgs(HighlightedType.Entered, transform.GetSiblingIndex()));
        }

        public void OnPointerExit(PointerEventData pointerEventData)
        {
            Highlighted?.Invoke(this, new HighlightedEventArgs(HighlightedType.Exited, transform.GetSiblingIndex()));
        }
    }
}
