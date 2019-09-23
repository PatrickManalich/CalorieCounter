using CalorieCounter;
using System;

namespace CalorieCounterOld.TargetEntries {

    public struct TargetEntry {

        public DateTime Date;
        public float Weight;
        public float CalorieMaintenanceLevel;
        public float RestDayFat;
        public float RestDayCarbs;
        public float RestDayProtein;
        public float RestDayCalories;
        public float TrainingDayFat;
        public float TrainingDayCarbs;
        public float TrainingDayProtein;
        public float TrainingDayCalories;

        private const float HeightInInches = 68;
        private const int AgeInYears = 23;
        private const float ActivityMultiplier = 1.375f;
        private const int WeeklyCalorieSurplus = 2100;
        private const int TrainingDays = 3;
        private const int RestDayCalorieSurplus = 150;
        private const int TrainingDayCalorieSurplus = (WeeklyCalorieSurplus - ((7 - TrainingDays) * RestDayCalorieSurplus)) / TrainingDays;
        private const float ProteinPerPound = 1.15f;
        private const float RestDayFatPercentage = 0.28f;
        private const float TrainingDayFatPercentage = 0.22f;

        public TargetEntry(DateTime date, float weight) {
            Date = date;
            Weight = weight;
            CalorieMaintenanceLevel = GlobalMethods.Round(((4.536f * Weight) + (15.88f * HeightInInches) - (5 * AgeInYears) + 5) * ActivityMultiplier);

            RestDayCalories = GlobalMethods.Round(CalorieMaintenanceLevel + RestDayCalorieSurplus);
            RestDayProtein = GlobalMethods.Round(Weight * ProteinPerPound);
            RestDayFat = GlobalMethods.Round(RestDayCalories * RestDayFatPercentage / 9);
            RestDayCarbs = GlobalMethods.Round((RestDayCalories - (RestDayProtein * 4) - RestDayFat * 9) / 4);

            TrainingDayCalories = GlobalMethods.Round(CalorieMaintenanceLevel + TrainingDayCalorieSurplus);
            TrainingDayProtein = GlobalMethods.Round(Weight * ProteinPerPound);
            TrainingDayFat = GlobalMethods.Round(TrainingDayCalories * TrainingDayFatPercentage / 9);
            TrainingDayCarbs = GlobalMethods.Round((TrainingDayCalories - (TrainingDayProtein * 4) - TrainingDayFat * 9) / 4);
        }

        public static bool operator ==(TargetEntry targetEntry1, TargetEntry targetEntry2) {
            return targetEntry1.Equals(targetEntry2);
        }

        public static bool operator !=(TargetEntry targetEntry1, TargetEntry targetEntry2) {
            return !targetEntry1.Equals(targetEntry2);
        }

        public override bool Equals(object obj) {
            return base.Equals(obj);
        }
        public override int GetHashCode() {
            return base.GetHashCode();
        }

        public override string ToString() {
            return string.Format("Date: {0}, Weight: {1}, Calorie Maintenance Level:{2}, Rest Day: [ Fat: {3}, Carbs: {4}, Protein: {5}, Calories: {6} ], Training Day: [ Fat: {7}, Carbs: {8}, Protein: {9}, Calories: {10} ]",
                Date.ToShortDateString(), Weight, CalorieMaintenanceLevel, RestDayFat, RestDayCarbs, RestDayProtein, RestDayCalories, TrainingDayFat, TrainingDayCarbs, TrainingDayProtein, TrainingDayCalories);
        }
    }
}
