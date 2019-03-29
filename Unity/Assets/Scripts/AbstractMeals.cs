using System.Collections.Generic;

namespace CalorieCounter {

    public abstract class AbstractMeals {

        public enum MealType { Small, Large }

        public abstract IReadOnlyCollection<Meal> Meals { get; }

    }
}
