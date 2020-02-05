using UnityEngine;
using UnityEngine.UI;

namespace CalorieCounter.MealEntries
{

    [RequireComponent(typeof(Button))]
    public class NutritionixButton : MonoBehaviour
    {
        [SerializeField]
        private string _Url = default;

        private Button _button;

        private void Start()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(() => Application.OpenURL(_Url));
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveListener(() => Application.OpenURL(_Url));
        }
    }

}
