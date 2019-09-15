using CalorieCounter.Utilities;
using TMPro;
using UnityEngine;

namespace CalorieCounter.MealSources
{
    public class MealSourceRenameField : MonoBehaviour
    {
        public bool Shown { get; private set; } = false;

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
            Shown = true;
        }

        public void Hide()
        {
            _inputField.text = "";
            _inputField.gameObject.SetActive(false);
            _inputField.transform.SetParent(transform, false);
            OldNamedMealSource = default;
            Shown = false;
        }
    }
}
