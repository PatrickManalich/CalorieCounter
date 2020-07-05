using CalorieCounter.Managers;
using UnityEngine;

namespace CalorieCounter.MealEntries
{
    [RequireComponent(typeof(ScrollViewRowHighlighter))]
	public class AcceptSuggestionInputKeyListener : MonoBehaviour
	{
        [SerializeField]
        private MealProportionsScrollView _mealProportionsScrollView = default;

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
            if (e.InputKeyCode == InputKeyCode.AcceptSuggestion && _scrollViewRowHighlighter.IsRowHighlighted)
            {
                _mealProportionsScrollView.AcceptSuggestion(_scrollViewRowHighlighter.HighlightedRowIndex);
                _scrollViewRowHighlighter.ExitHighlightRow();
            }
        }
    }
}

