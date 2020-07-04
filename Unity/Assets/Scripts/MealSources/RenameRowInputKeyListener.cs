using CalorieCounter.Managers;
using UnityEngine;

namespace CalorieCounter.MealSources
{
    [RequireComponent(typeof(ScrollViewRowHighlighter))]
    public class RenameRowInputKeyListener : MonoBehaviour
	{
        [SerializeField]
        private MealSourcesScrollView _mealSourcesScrollView = default;

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

            if (e.InputKeyCode == InputKeyCode.RenameRow)
            {
                _mealSourcesScrollView.ShowRenameField(_scrollViewRowHighlighter.HighlightedRowIndex);
                _scrollViewRowHighlighter.ExitHighlightRow();
            }
        }
    }
}

