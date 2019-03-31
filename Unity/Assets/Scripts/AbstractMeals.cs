using System.Collections.Generic;

namespace CalorieCounter {

    public abstract class AbstractMeals {

        public enum MealType { Small, Large }

        public abstract IReadOnlyList<Meal> Meals { get; }

    }
}
