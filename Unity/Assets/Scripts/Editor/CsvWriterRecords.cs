
using CalorieCounter.MealEntries;
using CalorieCounter.ScaleEntries;
using CalorieCounter.TargetEntries;

namespace CalorieCounter.EditorExtensions
{

    public class ScaleEntryRecord
    {
        public string Date { get; set; }
        public float Weight { get; set; }
        public float BodyFat { get; set; }
        public float BodyWater { get; set; }
        public float MuscleMass { get; set; }
        public float BoneMass { get; set; }
        public float Bmi { get; set; }

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
        public string Date { get; set; }
        public float Weight { get; set; }
        public float CalorieMaintenanceLevel { get; set; }
        public float RestDayFat { get; set; }
        public float RestDayCarbs { get; set; }
        public float RestDayProtein { get; set; }
        public float RestDayCalories { get; set; }
        public float TrainingDayFat { get; set; }
        public float TrainingDayCarbs { get; set; }
        public float TrainingDayProtein { get; set; }
        public float TrainingDayCalories { get; set; }

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
        public string Date { get; set; }
        public string DayType { get; set; }
        public string DayOfWeek { get; set; }
        public float TotalFat { get; set; }
        public float TargetFat { get; set; }
        public float TotalCarbs { get; set; }
        public float TargetCarbs { get; set; }
        public float TotalProtein { get; set; }
        public float TargetProtein { get; set; }
        public float TotalCalories { get; set; }
        public float TargetCalories { get; set; }

        public ResultRecord(MealEntry mealEntry, TargetEntry targetEntry)
        {
            Date = mealEntry.dateTime.ToShortDateString();
            DayType = mealEntry.dayType.ToString();
            DayOfWeek = mealEntry.dateTime.DayOfWeek.ToString();

            if (mealEntry.dayType == CalorieCounter.DayType.Rest || mealEntry.dayType == CalorieCounter.DayType.Training)
            {
                TotalFat = mealEntry.totalFat;
                TargetFat = mealEntry.dayType == CalorieCounter.DayType.Rest ? targetEntry.RestDayFat : targetEntry.TrainingDayFat;
                TotalCarbs = mealEntry.totalCarbs;
                TargetCarbs = mealEntry.dayType == CalorieCounter.DayType.Rest ? targetEntry.RestDayCarbs : targetEntry.TrainingDayCarbs;
                TotalProtein = mealEntry.totalProtein;
                TargetProtein = mealEntry.dayType == CalorieCounter.DayType.Rest ? targetEntry.RestDayProtein : targetEntry.TrainingDayProtein;
                TotalCalories = mealEntry.totalCalories;
                TargetCalories = mealEntry.dayType == CalorieCounter.DayType.Rest ? targetEntry.RestDayCalories : targetEntry.TrainingDayCalories;
            }
        }
    }
}