using CalorieCounter.MealSources;
using Newtonsoft.Json;

namespace CalorieCounter.MealEntries {

    public class MealProportion {

        public float ServingAmount { get; }
        public MealSource MealSource { get; }
        public float Fat { get; }
        public float Carbs { get; }
        public float Protein { get; }
        public float Calories { get; }

        public MealProportion() { }

        [JsonConstructor]
        public MealProportion(float servingAmount, MealSource mealSource) {
            ServingAmount = servingAmount;
            MealSource = mealSource;
            Fat = GlobalMethods.Round(mealSource.Fat * ServingAmount);
            Carbs = GlobalMethods.Round(mealSource.Carbs * ServingAmount);
            Protein = GlobalMethods.Round(mealSource.Protein * ServingAmount);
            Calories = GlobalMethods.Round((Fat * 9) + (Carbs * 4) + (Protein * 4));
        }

        public static bool operator ==(MealProportion mealProportion1, MealProportion mealProportion2)
        {
            if (mealProportion1 is null)
            {
                if (mealProportion2 is null)
                {
                    return true;
                }
                return false;
            }
            return mealProportion1.Equals(mealProportion2);
        }

        public static bool operator !=(MealProportion mealProportion1, MealProportion mealProportion2)
        {
            return !(mealProportion1 == mealProportion2);
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as MealProportion);
        }

        public override int GetHashCode()
        {
            return (ServingAmount, MealSource, Fat, Carbs, Protein, Calories).GetHashCode();
        }

        public override string ToString() {
            return $"Serving Amount: {ServingAmount}, Meal Source ID: {MealSource.Id}, [ Fat: {Fat}, Carbs: {Carbs}, " +
                $"Protein: {Protein}, Calories: {Calories} ]";
        }

        public bool Equals(MealProportion other)
        {
            if (other is null)
            {
                return false;
            }
            if (ReferenceEquals(this, other))
            {
                return true;
            }
            if (GetType() != other.GetType())
            {
                return false;
            }
            return (ServingAmount == other.ServingAmount) && (MealSource == other.MealSource) && (Fat == other.Fat)
                && (Carbs == other.Carbs) && (Protein == other.Protein) && (Calories == other.Calories);
        }
    }
}
