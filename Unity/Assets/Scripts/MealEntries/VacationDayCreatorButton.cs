using UnityEngine;
using UnityEngine.UI;

namespace CalorieCounter.MealEntries
{

    [RequireComponent(typeof(Button))]
    public class VacationDayCreatorButton : MonoBehaviour
    {
        [SerializeField]
        private VacationDayCreator _vacationDayCreator = default;

        private Button _button;

        private void Start()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(Button_OnClick);
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveListener(Button_OnClick);
        }

        private void Button_OnClick()
        {
            _vacationDayCreator.Show();
        }
    }
}
