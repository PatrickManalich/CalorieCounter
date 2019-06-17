using System.Collections.Generic;

namespace CalorieCounter.MealSources {

    public abstract class AbstractMealSources {

        public abstract MealSourceType MealSourceType { get; }

        public abstract IReadOnlyList<MealSource> MealSources { get; }

    }
}
