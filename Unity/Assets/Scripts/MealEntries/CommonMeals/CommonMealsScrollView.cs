using CalorieCounter.MealSources;
using UnityEngine;

namespace CalorieCounter.MealEntries {

    public class CommonMealsScrollView : AbstractMealsScrollView {

        [SerializeField]
        private MealSourcesAdapter _mealSourcesAdapter = default;

        protected override string GetMealSourceName(MealProportion mealProportion)
        {
            return _mealSourcesAdapter.GetMealSourceName(mealProportion.mealSource);
        }
    }
}
