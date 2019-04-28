using CalorieCounter.Globals;
using System;

namespace CalorieCounter.TargetEntries {

    public struct TargetEntry {

        public DateTime Date;
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


        public TargetEntry(DateTime date, float weight) {
            Date = date;
            Weight = weight;
            CalorieMaintenanceLevel = GlobalMethods.Round(((4.536f * Weight) + (15.88f * HeightInInches) - (5 * AgeInYears) + 5) * ActivityMultiplier);

            RestDayCalories = GlobalMethods.Round(CalorieMaintenanceLevel + RestDayCalorieSurplus);
            RestDayProteinGrams = GlobalMethods.Round(Weight * ProteinGramsPerPound);
            RestDayFatGrams = GlobalMethods.Round(RestDayCalories * RestDayFatPercentage / 9);
            RestDayCarbGrams = GlobalMethods.Round((RestDayCalories - (RestDayProteinGrams * 4) - RestDayFatGrams * 9) / 4);

            TrainingDayCalories = GlobalMethods.Round(CalorieMaintenanceLevel + TrainingDayCalorieSurplus);
            TrainingDayProteinGrams = GlobalMethods.Round(Weight * ProteinGramsPerPound);
            TrainingDayFatGrams = GlobalMethods.Round(TrainingDayCalories * TrainingDayFatPercentage / 9);
            TrainingDayCarbGrams = GlobalMethods.Round((TrainingDayCalories - (TrainingDayProteinGrams * 4) - TrainingDayFatGrams * 9) / 4);
        }

        public override string ToString() {
            return "CML:" + CalorieMaintenanceLevel + ", RestDay [ Calories:" + RestDayCalories + ", Fat: " + RestDayFatGrams + ", Carbs:" + RestDayCarbGrams +
                ", Protein:" + RestDayProteinGrams + " ], TrainingDay [ Calories:" + TrainingDayCalories + ", Fat: " + TrainingDayFatGrams + ", Carbs:" + TrainingDayCarbGrams +
                ", Protein:" + TrainingDayProteinGrams + " ]";
        }
    }
}
