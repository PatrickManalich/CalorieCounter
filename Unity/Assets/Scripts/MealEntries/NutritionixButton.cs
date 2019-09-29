using UnityEngine;
using UnityEngine.UI;

namespace CalorieCounter.MealEntries
{

    [RequireComponent(typeof(Button))]
    public class NutritionixButton : MonoBehaviour
    {

        private const string NutritionixUrl = "https://www.nutritionix.com/";

        private Button _button;

        private void Start()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(() => Application.OpenURL(NutritionixUrl));
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveListener(() => Application.OpenURL(NutritionixUrl));
        }
    }

}
