using System;
using UnityEngine;

namespace CalorieCounter.MealSources {

    [Serializable]
    public class MealSource {

        public string id;

        [HideInInspector]
        public string servingSize;

        [HideInInspector]
        public float fat;

        [HideInInspector]
        public float carbs;

        [HideInInspector]
        public float protein;

        [HideInInspector]
        public float calories;

        [HideInInspector]
        public string description;

        [HideInInspector]
        public MealSourceType mealSourceType;

        [HideInInspector]
        public bool archived;

        private const string CustomMealSourceServingSize = "Meal";

        public MealSource(string servingSize, float fat, float carbs, float protein, string description, MealSourceType mealSourceType)
            : this(Guid.NewGuid().ToString(), servingSize, fat, carbs, protein, description, mealSourceType, false)
        {
        }

        public MealSource(string id, string servingSize, float fat, float carbs, float protein, string description, MealSourceType mealSourceType, bool archived)
        {
            this.id = id;
            this.servingSize = servingSize;
            this.fat = fat > 0 ? GlobalMethods.Round(fat) : 0;
            this.carbs = carbs > 0 ? GlobalMethods.Round(carbs) : 0;
            this.protein = protein > 0 ? GlobalMethods.Round(protein) : 0;
            calories = GlobalMethods.Round((fat * 9) + (carbs * 4) + (protein * 4));
            this.description = description;
            this.mealSourceType = mealSourceType;
            this.archived = archived;
        }

        public static MealSource CreateCustomMealSource(float fat, float carbs, float protein) {
            return new MealSource(CustomMealSourceServingSize, fat, carbs, protein, "", MealSourceType.Custom);
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
            return (id, servingSize, fat, carbs, protein, calories, description, mealSourceType, archived).GetHashCode();
        }

        public override string ToString() {
            return $"ID: {id}, Serving Size: {servingSize}, [ Fat: {fat}, Carbs: {carbs}, Protein: {protein}, " +
                $"Calories: {calories} ], Description: {description}, Meal Type: {mealSourceType}, Archived: {archived}";
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
            return (id == other.id) && (servingSize == other.servingSize) && (fat == other.fat) && (carbs == other.carbs) &&
                (protein == other.protein) && (calories == other.calories) && (description == other.description) &&
                (mealSourceType == other.mealSourceType) && (archived == other.archived);
        }
    }
}
