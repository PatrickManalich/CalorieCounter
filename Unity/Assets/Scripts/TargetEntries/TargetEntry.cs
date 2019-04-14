using System;

namespace CalorieCounter.TargetEntries {

    public struct TargetEntry {

        public float Weight;
        public float CalorieMaintenanceLevel;
        public float RestDayCalories;
        public float RestDayProteinGrams;
        public float RestDayFatGrams;
        public float RestDayCarbGrams;
        public float TrainingDayCalories;
        public float TrainingDayProteinGrams;
        public float TrainingDayFatGrams;
        public float TrainingDayCarbGrams;

        private const float HeightInInches = 68;
        private const int AgeInYears = 23;
        private const float ActivityMultiplier = 1.375f;
        private const int WeeklyCalorieSurplus = 2100;
        private const int TrainingDays = 3;
        private const int RestDayCalorieSurplus = 150;
        private const int TrainingDayCalorieSurplus = (WeeklyCalorieSurplus - ((7 - TrainingDays) * RestDayCalorieSurplus)) / TrainingDays;
        private const float ProteinGramsPerPound = 1.15f;
        private const float RestDayFatPercentage = 0.28f;
        private const float TrainingDayFatPercentage = 0.22f;


        public TargetEntry(float weight) {
            Weight = weight;
            CalorieMaintenanceLevel = Round(((4.536f * Weight) + (15.88f * HeightInInches) - (5 * AgeInYears) + 5) * ActivityMultiplier);

            RestDayCalories = Round(CalorieMaintenanceLevel + RestDayCalorieSurplus);
            RestDayProteinGrams = Round(Weight * ProteinGramsPerPound);
            RestDayFatGrams = Round(RestDayCalories * RestDayFatPercentage / 9);
            RestDayCarbGrams = Round((RestDayCalories - (RestDayProteinGrams * 4) - RestDayFatGrams * 9) / 4);

            TrainingDayCalories = Round(CalorieMaintenanceLevel + TrainingDayCalorieSurplus);
            TrainingDayProteinGrams = Round(Weight * ProteinGramsPerPound);
            TrainingDayFatGrams = Round(TrainingDayCalories * TrainingDayFatPercentage / 9);
            TrainingDayCarbGrams = Round((TrainingDayCalories - (TrainingDayProteinGrams * 4) - TrainingDayFatGrams * 9) / 4);
        }

        public override string ToString() {
            return "CML:" + CalorieMaintenanceLevel + ", RestDay [ Calories:" + RestDayCalories + ", Fat: " + RestDayFatGrams + ", Carbs:" + RestDayCarbGrams +
                ", Protein:" + RestDayProteinGrams + " ], TrainingDay [ Calories:" + TrainingDayCalories + ", Fat: " + TrainingDayFatGrams + ", Carbs:" + TrainingDayCarbGrams +
                ", Protein:" + TrainingDayProteinGrams + " ]";
        }

        private static float Round(float number) {
            return (float)Math.Round(number, 1);
        }
    }
}
