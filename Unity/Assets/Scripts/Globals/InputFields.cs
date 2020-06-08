using CalorieCounter.Utilities;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace CalorieCounter
{
    public class InputFields : MonoBehaviour
    {
        public bool IsShown { get; private set; } = false;

        public bool IsValid => !_inputFields.Exists(i => i.text == string.Empty);

        [SerializeField]
        protected List<TMP_InputField> _inputFields = default;

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
            if (IsValid)
            {
                FindObjectOfType<InteractableHandler>()?.Execute(gameObject);
            }
            else
            {
                FindObjectOfType<InteractableHandler>()?.UndoExecute(gameObject);
            }
        }
    }
}
