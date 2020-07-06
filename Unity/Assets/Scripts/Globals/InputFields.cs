using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace CalorieCounter
{
    public class InputFields : MonoBehaviour
    {
        public event EventHandler ValidityChanged;

        public bool IsShown { get; private set; } = false;

        public bool IsValid => !_inputFields.Exists(i => i.text == string.Empty);

        [SerializeField]
        protected List<TMP_InputField> _inputFields = default;

        private bool _oldIsValid;

        public virtual void Show()
        {
            IsShown = true;
        }

        public virtual void Hide()
        {
            IsShown = false;
        }

        protected virtual void Awake()
        {
            foreach (var inputField in _inputFields)
            {
                inputField.onValueChanged.AddListener(InputField_OnValueChanged);
            }
        }

        private void OnDestroy()
        {
            foreach (var inputField in _inputFields)
            {
                inputField.onValueChanged.RemoveListener(InputField_OnValueChanged);
            }
        }

        private void InputField_OnValueChanged(string value)
        {
            if (IsValid != _oldIsValid)
            {
                ValidityChanged?.Invoke(this, EventArgs.Empty);
                _oldIsValid = IsValid;
            }
        }
    }
}
