namespace CalorieCounter.MealEntries {

    public class CustomMealsScrollView : AbstractMealsScrollView {

        private const string CustomMealSourceName = "Custom Meal";

        protected override string GetMealSourceName(MealProportion mealProportion)
        {
            return CustomMealSourceName;
        }
    }
}
