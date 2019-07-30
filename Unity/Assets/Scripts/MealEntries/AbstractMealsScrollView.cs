using System.Collections.Generic;

namespace CalorieCounter.MealEntries {

    public abstract class AbstractMealsScrollView : AbstractScrollView {

        public abstract List<MealProportion> GetMealProportions();
        public abstract void AddMealProportion(MealProportion mealProportion);
        public abstract void SubtractMealProportion(MealProportion mealProportion);
        public abstract void ClearMealProportions();
    }
}
