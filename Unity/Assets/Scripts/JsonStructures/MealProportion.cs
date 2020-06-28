using CalorieCounter.MealSources;
using Newtonsoft.Json;

namespace CalorieCounter.MealEntries {

    public class MealProportion {

        public float servingAmount;

        public MealSource mealSource;

        public float fat;

        public float carbs;

        public float protein;

        public float calories;

        public MealProportion() { }

        [JsonConstructor]
        public MealProportion(float servingAmount, MealSource mealSource) {
            this.servingAmount = servingAmount;
            this.mealSource = mealSource;
            fat = GlobalMethods.Round(mealSource.Fat * this.servingAmount);
            carbs = GlobalMethods.Round(mealSource.Carbs * this.servingAmount);
            protein = GlobalMethods.Round(mealSource.Protein * this.servingAmount);
            calories = GlobalMethods.Round((fat * 9) + (carbs * 4) + (protein * 4));
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
            return (servingAmount, mealSource, fat, carbs, protein, calories).GetHashCode();
        }

        public override string ToString() {
            return $"Serving Amount: {servingAmount}, Meal Source ID: {mealSource.Id}, [ Fat: {fat}, Carbs: {carbs}, " +
                $"Protein: {protein}, Calories: {calories} ]";
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
            return (servingAmount == other.servingAmount) && (mealSource == other.mealSource) && (fat == other.fat)
                && (carbs == other.carbs) && (protein == other.protein) && (calories == other.calories);
        }
    }
}
