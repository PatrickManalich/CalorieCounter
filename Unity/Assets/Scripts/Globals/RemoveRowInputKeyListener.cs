using CalorieCounter.Managers;
using UnityEngine;

namespace CalorieCounter
{
    [RequireComponent(typeof(ScrollViewRowHighlighter))]
    public class RemoveRowInputKeyListener : MonoBehaviour
	{
        [SerializeField]
        private ScrollViewAssistant _scrollViewAssistant = default;

        private ScrollViewRowHighlighter _scrollViewRowHighlighter;

        private void Start()
		{
            _scrollViewRowHighlighter = GetComponent<ScrollViewRowHighlighter>();
            GameManager.InputKeyManager.InputKeyPressed += InputKeyManager_OnInputKeyPressed;
        }

        private void OnDestroy()
		{
            GameManager.InputKeyManager.InputKeyPressed -= InputKeyManager_OnInputKeyPressed;
        }

        private void InputKeyManager_OnInputKeyPressed(object sender, InputKeyManager.InputKeyPressedEventArgs e)
        {
            if (!_scrollViewRowHighlighter.IsRowHighlighted)
            {
                return;
            }

            if (e.InputKeyCode == InputKeyCode.RemoveRow)
            {
                _scrollViewAssistant.RemoveRow(_scrollViewRowHighlighter.HighlightedRowIndex);
                _scrollViewRowHighlighter.ExitHighlightRow();
            }
        }
    }
}

