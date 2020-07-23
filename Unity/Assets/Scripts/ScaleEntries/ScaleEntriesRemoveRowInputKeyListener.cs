using CalorieCounter.Managers;
using UnityEngine;

namespace CalorieCounter.ScaleEntries
{
    [RequireComponent(typeof(ScrollViewRowHighlighter))]
    public class ScaleEntriesRemoveRowInputKeyListener : MonoBehaviour
    {
        [SerializeField]
        private ScaleEntriesScrollView _scaleEntriesScrollView = default;

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
            if (e.InputKeyCode == InputKeyCode.RemoveRow && _scrollViewRowHighlighter.IsRowHighlighted)
            {
                _scaleEntriesScrollView.RemoveRow(_scrollViewRowHighlighter.HighlightedRowIndex);
                _scrollViewRowHighlighter.ExitHighlightRow();
            }
        }
    }
}