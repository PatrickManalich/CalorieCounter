using System.Collections.Generic;

namespace CalorieCounter {

    public interface IMeal {
        IReadOnlyCollection<Meal> Meals { get; }
    }
}