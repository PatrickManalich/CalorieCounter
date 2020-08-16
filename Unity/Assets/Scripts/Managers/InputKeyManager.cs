using RotaryHeart.Lib.SerializableDictionary;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace CalorieCounter.Managers
{
    public class InputKeyManager : MonoBehaviour
    {
        public class InputKeyPressedEventArgs : EventArgs
        {
            public InputKeyCode InputKeyCode { get; }

            public InputKeyPressedEventArgs(InputKeyCode inputKeyCode)
            {
                InputKeyCode = inputKeyCode;
            }
        }

        public event EventHandler<InputKeyPressedEventArgs> InputKeyPressed;

        private Dictionary<KeyCode, InputKeyCode> _inputKeyCodeDictionary = new Dictionary<KeyCode, InputKeyCode>()
        {
            { KeyCode.Escape, InputKeyCode.ToggleMenu },
            { KeyCode.Delete, InputKeyCode.RemoveRow },
            { KeyCode.F2, InputKeyCode.RenameRow },
            { KeyCode.S, InputKeyCode.AcceptSuggestion },
            { KeyCode.Tab, InputKeyCode.SelectNext },
        };

        private void Update()
        {
            foreach(var keyCode in _inputKeyCodeDictionary.Keys)
            {
                if (Input.GetKeyDown(keyCode))
                {
                    InputKeyPressed?.Invoke(this, new InputKeyPressedEventArgs(_inputKeyCodeDictionary[keyCode]));
                }
            }
        }
    }
}