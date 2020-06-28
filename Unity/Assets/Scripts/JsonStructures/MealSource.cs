using Newtonsoft.Json;
using System;

namespace CalorieCounter.MealSources {

    public class MealSource {

        public string Id { get; }
        public string ServingSize { get; }
        public float Fat { get; }
        public float Carbs { get; }
        public float Protein { get; }
        public float Calories { get; }
        public string Description { get; }
        public MealSourceType MealSourceType { get; }
        public bool Archived { get; }

        private const string CustomMealSourceServingSize = "Meal";

        public MealSource() { }

        public MealSource(MealSource mealSource, bool archived) : this(mealSource.Id, mealSource.ServingSize, mealSource.Fat, mealSource.Carbs,
            mealSource.Protein, mealSource.Description, mealSource.MealSourceType, archived) {}

        public MealSource(string servingSize, float fat, float carbs, float protein, string description, MealSourceType mealSourceType)
            : this(Guid.NewGuid().ToString(), servingSize, fat, carbs, protein, description, mealSourceType, false) {}

        [JsonConstructor]
        public MealSource(string id, string servingSize, float fat, float carbs, float protein, string description, MealSourceType mealSourceType, bool archived)
        {
            Id = id;
            ServingSize = servingSize;
            Fat = fat > 0 ? GlobalMethods.Round(fat) : 0;
            Carbs = carbs > 0 ? GlobalMethods.Round(carbs) : 0;
            Protein = protein > 0 ? GlobalMethods.Round(protein) : 0;
            Calories = GlobalMethods.Round((fat * 9) + (carbs * 4) + (protein * 4));
            Description = description;
            MealSourceType = mealSourceType;
            Archived = archived;
        }

        public static MealSource CreateCustomMealSource(float fat, float carbs, float protein) {
            return new MealSource(CustomMealSourceServingSize, fat, carbs, protein, string.Empty, MealSourceType.Custom);
        }

        public static bool operator ==(MealSource mealSource1, MealSource mealSource2)
        {
            if (mealSource1 is null)
            {
                if (mealSource2 is null)
                {
                    return true;
                }
                return false;
            }
            return mealSource1.Equals(mealSource2);
        }

        public static bool operator !=(MealSource mealSource1, MealSource mealSource2)
        {
            return !(mealSource1 == mealSource2);
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as MealSource);
        }

        public override int GetHashCode()
        {
            return (Id, ServingSize, Fat, Carbs, Protein, Calories, Description, MealSourceType, Archived).GetHashCode();
        }

        public override string ToString() {
            return $"ID: {Id}, Serving Size: {ServingSize}, [ Fat: {Fat}, Carbs: {Carbs}, Protein: {Protein}, " +
                $"Calories: {Calories} ], Description: {Description}, Meal Type: {MealSourceType}, Archived: {Archived}";
        }

        public bool Equals(MealSource other)
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
            return (Id == other.Id) && (ServingSize == other.ServingSize) && (Fat == other.Fat) && (Carbs == other.Carbs) &&
                (Protein == other.Protein) && (Calories == other.Calories) && (Description == other.Description) &&
                (MealSourceType == other.MealSourceType) && (Archived == other.Archived);
        }
    }
}
