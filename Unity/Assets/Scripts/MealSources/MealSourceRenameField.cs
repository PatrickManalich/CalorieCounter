using TMPro;
using UnityEngine;

namespace CalorieCounter.MealSources
{
    public class MealSourceRenameField : MonoBehaviour
    {

        [SerializeField]
        private TMP_InputField _inputField = default;

        private bool _renaming = false;

        public void ShowRenameField(Transform parentTransform, string oldNameText)
        {
            if (!_renaming)
            {
                _inputField.transform.SetParent(parentTransform, false);
                _inputField.gameObject.SetActive(true);
                _inputField.text = oldNameText;
                _inputField.Select();
                _renaming = true;
            }
        }
    }
}
