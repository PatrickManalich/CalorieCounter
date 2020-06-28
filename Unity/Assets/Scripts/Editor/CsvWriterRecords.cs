
using CalorieCounter.MealEntries;
using CalorieCounter.ScaleEntries;
using CalorieCounter.TargetEntries;

namespace CalorieCounter.EditorExtensions
{

    public class ScaleEntryRecord
    {
        public string Date { get; }
        public float Weight { get; }
        public float BodyFat { get; }
        public float BodyWater { get; }
        public float MuscleMass { get; }
        public float BoneMass { get; }
        public float Bmi { get; }

        public ScaleEntryRecord(ScaleEntry scaleEntry)
        {
            Date = scaleEntry.DateTime.ToShortDateString();
            Weight = scaleEntry.Weight;
            BodyFat = scaleEntry.BodyFat;
            BodyWater = scaleEntry.BodyWater;
            MuscleMass = scaleEntry.MuscleMass;
            BoneMass = scaleEntry.BoneMass;
            Bmi = scaleEntry.Bmi;
        }
    }


    public class TargetEntryRecord
    {
        public string Date { get; }
        public float Weight { get; }
        public float CalorieMaintenanceLevel { get; }
        public float RestDayFat { get; }
        public float RestDayCarbs { get; }
        public float RestDayProtein { get; }
        public float RestDayCalories { get; }
        public float TrainingDayFat { get; }
        public float TrainingDayCarbs { get; }
        public float TrainingDayProtein { get; }
        public float TrainingDayCalories { get; }

        public TargetEntryRecord(TargetEntry targetEntry)
        {
            Date = targetEntry.DateTime.ToShortDateString();
            Weight = targetEntry.Weight;
            CalorieMaintenanceLevel = targetEntry.CalorieMaintenanceLevel;
            RestDayFat = targetEntry.RestDayFat;
            RestDayCarbs = targetEntry.RestDayCarbs;
            RestDayProtein = targetEntry.RestDayProtein;
            RestDayCalories = targetEntry.RestDayCalories;
            TrainingDayFat = targetEntry.TrainingDayFat;
            TrainingDayCarbs = targetEntry.TrainingDayCarbs;
            TrainingDayProtein = targetEntry.TrainingDayProtein;
            TrainingDayCalories = targetEntry.TrainingDayCalories;
        }
    }

    public class ResultRecord
    {
        public string Date { get; }
        public string DayType { get; }
        public string DayOfWeek { get; }
        public float TotalFat { get; }
        public float TargetFat { get; }
        public float TotalCarbs { get; }
        public float TargetCarbs { get; }
        public float TotalProtein { get; }
        public float TargetProtein { get; }
        public float TotalCalories { get; }
        public float TargetCalories { get; }

        public ResultRecord(MealEntry mealEntry, TargetEntry targetEntry)
        {
            Date = mealEntry.DateTime.ToShortDateString();
            DayType = mealEntry.DayType.ToString();
            DayOfWeek = mealEntry.DateTime.DayOfWeek.ToString();

            if (mealEntry.DayType == CalorieCounter.DayType.Rest || mealEntry.DayType == CalorieCounter.DayType.Training)
            {
                TotalFat = mealEntry.TotalFat;
                TargetFat = mealEntry.DayType == CalorieCounter.DayType.Rest ? targetEntry.RestDayFat : targetEntry.TrainingDayFat;
                TotalCarbs = mealEntry.TotalCarbs;
                TargetCarbs = mealEntry.DayType == CalorieCounter.DayType.Rest ? targetEntry.RestDayCarbs : targetEntry.TrainingDayCarbs;
                TotalProtein = mealEntry.TotalProtein;
                TargetProtein = mealEntry.DayType == CalorieCounter.DayType.Rest ? targetEntry.RestDayProtein : targetEntry.TrainingDayProtein;
                TotalCalories = mealEntry.TotalCalories;
                TargetCalories = mealEntry.DayType == CalorieCounter.DayType.Rest ? targetEntry.RestDayCalories : targetEntry.TrainingDayCalories;
            }
        }
    }
}