using CalorieCounter.Managers;
using CalorieCounter.Utilities;
using UnityEngine;

namespace CalorieCounter
{
    [RequireComponent(typeof(ScrollViewRowHighlighter))]
    public class DeleteRowInputKeyListener : MonoBehaviour
	{
        [SerializeField]
        private ScrollView _scrollView = default;

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
            if (!_scrollViewRowHighlighter.RowHighlighted)
            {
                return;
            }

            if (e.InputKeyCode == InputKeyCode.DeleteRow)
            {
                _scrollView.DeleteRow(_scrollViewRowHighlighter.HighlightedRowIndex);
                _scrollViewRowHighlighter.ExitHighlightRow();
            }
        }
    }
}

