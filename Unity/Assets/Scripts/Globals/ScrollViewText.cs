using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace CalorieCounter
{
    [RequireComponent(typeof(TextMeshProUGUI))]
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

        public TextMeshProUGUI Text { get; private set; }

        public void OnPointerEnter(PointerEventData eventData)
        {
            Highlighted?.Invoke(this, new HighlightedEventArgs(HighlightedType.Entered, transform.GetSiblingIndex()));
            EventSystem.current.SetSelectedGameObject(null);
        }

        public void OnPointerExit(PointerEventData pointerEventData)
        {
            Highlighted?.Invoke(this, new HighlightedEventArgs(HighlightedType.Exited, transform.GetSiblingIndex()));
        }

        private void Awake()
        {
            Text = GetComponent<TextMeshProUGUI>();
        }
    }
}
