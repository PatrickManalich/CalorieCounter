using CalorieCounter.Utilities;
using System.Linq;
using TMPro;
using UnityEngine;

namespace CalorieCounter.MealSources
{
    public class MealSourceRenameField : MonoBehaviour
    {
        public bool IsShown { get; private set; } = false;

        public NamedMealSource OldNamedMealSource { get; private set; } = default;

        public NamedMealSource NewNamedMealSource {
            get 
            {
                var newNamedMealSource = OldNamedMealSource;
                newNamedMealSource.name = _inputField.text == "" ? OldNamedMealSource.name : _inputField.text;
                return newNamedMealSource;
            }
        }

        [SerializeField]
        private TMP_InputField _inputField = default;


        public void Show(Transform parentTransform, NamedMealSource oldNamedMealSource)
        {
            OldNamedMealSource = oldNamedMealSource;
            _inputField.transform.SetParent(parentTransform, false);
            _inputField.gameObject.SetActive(true);
            _inputField.text = OldNamedMealSource.name;
            _inputField.Select();
            FindObjectOfType<InteractableHandler>()?.Execute(gameObject);
            IsShown = true;
        }

        public void Hide()
        {
            _inputField.text = "";
            _inputField.gameObject.SetActive(false);
            _inputField.transform.SetParent(transform, false);
            OldNamedMealSource = default;
            IsShown = false;
        }

        private void Awake()
        {
            _inputField.onValidateInput = ValidateNonDecimalInput;
        }

        private static char ValidateNonDecimalInput(string text, int charIndex, char addedChar)
        {
            return char.IsLetterOrDigit(addedChar) || GlobalConsts.ValidSpecialChars.Contains(addedChar) ? addedChar : '\0';
        }
    }
}
