using System;

namespace CalorieCounter.MealEntries {

    public struct MealSource {

        public string Name;
        public string ServingSize;
        public float Fat;
        public float Carbs;
        public float Protein;
        public float Calories;
        public string Description;
        public MealTypes MealType;

        public MealSource(string name, string servingSize, float fat, float carbs, float protein, string description, MealTypes mealType) {
            Name = name;
            ServingSize = servingSize;
            Fat = fat > 0 ? Round(fat) : 0;
            Carbs = carbs > 0 ? Round(carbs) : 0;
            Protein = protein > 0 ? Round(protein) : 0;
            Calories = Round((fat * 9) + (carbs * 4) + (protein * 4));
            Description = description;
            MealType = mealType;
        }

        public static bool operator ==(MealSource meal1, MealSource meal2) {
            return meal1.Equals(meal2);
        }

        public static bool operator !=(MealSource meal1, MealSource meal2) {
            return !meal1.Equals(meal2);
        }

        public override bool Equals(object obj) {
            return base.Equals(obj);
        }
        public override int GetHashCode() {
            return base.GetHashCode();
        }

        public override string ToString() {
            return Name + " (per " + ServingSize + "), [ Fat:" + Fat + ", Carbs:" + Carbs + ", Protein:" + Protein + ", Calories:" + Calories + " ], Description:" + Description + ", Meal Type:" + MealType;
        }

        private static float Round(float number) {
            return (float)Math.Round(number, 1);
        }
    }
}
