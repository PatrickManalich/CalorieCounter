using RotaryHeart.Lib.SerializableDictionary;
using System;
using UnityEngine;

namespace CalorieCounter.Managers
{

    public class InputKeyManager : MonoBehaviour
    {
        public class InputKeyPressedEventArgs : EventArgs
        {
            public InputKeyCode InputKeyCode { get; private set; }

            public InputKeyPressedEventArgs(InputKeyCode inputKeyCode)
            {
                InputKeyCode = inputKeyCode;
            }
        }

        public delegate void InputKeyPressedEventHandler(object sender, InputKeyPressedEventArgs e);
        public event InputKeyPressedEventHandler InputKeyPressed;


        [Serializable]
        private class InputKeyCodeDictionary : SerializableDictionaryBase<KeyCode, InputKeyCode> { }

        [SerializeField]
        private InputKeyCodeDictionary _inputKeyCodeDictionary = default;

        private void Update()
        {
            foreach(var keyCode in _inputKeyCodeDictionary.Keys)
            {
                if (Input.GetKeyDown(keyCode))
                {
                    var inputKeyCode = _inputKeyCodeDictionary[keyCode];
                    InputKeyPressed?.Invoke(this, new InputKeyPressedEventArgs(inputKeyCode));
                }
            }
        }
    }
}