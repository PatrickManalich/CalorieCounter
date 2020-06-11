using CalorieCounter.Utilities;
using System.Linq;
using TMPro;
using UnityEngine;

namespace CalorieCounter.MealSources
{
    public class MealSourceRenameField : MonoBehaviour
    {
        public bool IsShown { get; private set; } = false;

        public NamedMealSource OldNamedMealSource { get; private set; } = null;

        public NamedMealSource NewNamedMealSource {
            get 
            {
                var newName = _inputField.text == string.Empty ? OldNamedMealSource.name : _inputField.text;
                var newNamedMealSource = new NamedMealSource(newName, OldNamedMealSource.mealSource);
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
            IsShown = true;
        }

        public void Hide()
        {
            _inputField.text = string.Empty;
            _inputField.gameObject.SetActive(false);
            _inputField.transform.SetParent(transform, false);
            OldNamedMealSource = null;
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
