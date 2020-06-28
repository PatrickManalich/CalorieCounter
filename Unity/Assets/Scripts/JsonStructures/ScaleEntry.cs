using Newtonsoft.Json;
using System;

namespace CalorieCounter.ScaleEntries {

    public class ScaleEntry {

        public DateTime DateTime { get; }
        public float Weight { get; }
        public float BodyFat { get; }
        public float BodyWater { get; }
        public float MuscleMass { get; }
        public float BoneMass { get; }
        public float Bmi { get; }

        public ScaleEntry() { }

        [JsonConstructor]
        public ScaleEntry(DateTime dateTime, float weight, float bodyFat, float bodyWater, float muscleMass, float boneMass, float bmi) {
            DateTime = dateTime;
            Weight = weight > 0 ? GlobalMethods.Round(weight) : 0;
            BodyFat = bodyFat > 0 ? GlobalMethods.Round(bodyFat) : 0;
            BodyWater = bodyWater > 0 ? GlobalMethods.Round(bodyWater) : 0;
            MuscleMass = muscleMass > 0 ? GlobalMethods.Round(muscleMass) : 0;
            BoneMass = boneMass > 0 ? GlobalMethods.Round(boneMass) : 0;
            Bmi = bmi > 0 ? GlobalMethods.Round(bmi) : 0;
        }

        public static bool operator ==(ScaleEntry scaleEntry1, ScaleEntry scaleEntry2)
        {
            if (scaleEntry1 is null)
            {
                if (scaleEntry2 is null)
                {
                    return true;
                }
                return false;
            }
            return scaleEntry1.Equals(scaleEntry2);
        }

        public static bool operator !=(ScaleEntry scaleEntry1, ScaleEntry scaleEntry2)
        {
            return !(scaleEntry1 == scaleEntry2);
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as ScaleEntry);
        }

        public override int GetHashCode()
        {
            return (DateTime, Weight, BodyFat, BodyWater, MuscleMass, BoneMass, Bmi).GetHashCode();
        }

        public override string ToString() {
            return $"Date: {DateTime.ToShortDateString()}, Weight: {Weight}, Body Fat: {BodyFat}, Body Water: {BodyWater}, " +
                $"Muscle Mass: {MuscleMass}, Bone Mass: {BoneMass}, BMI: {Bmi}";
        }
        public bool Equals(ScaleEntry other)
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
            return (DateTime == other.DateTime) && (Weight == other.Weight) && (BodyFat == other.BodyFat) &&
                (BodyWater == other.BodyWater) && (MuscleMass == other.MuscleMass) && (BoneMass == other.BoneMass) && (Bmi == other.Bmi);
        }
    }
}
