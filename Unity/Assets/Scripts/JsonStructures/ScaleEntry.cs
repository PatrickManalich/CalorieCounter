using Newtonsoft.Json;
using System;

namespace CalorieCounter.ScaleEntries {

    public class ScaleEntry {

        public DateTime dateTime;

        public float weight;

        public float bodyFat;

        public float bodyWater;

        public float muscleMass;

        public float boneMass;

        public float bmi;

        public ScaleEntry() { }

        [JsonConstructor]
        public ScaleEntry(DateTime dateTime, float weight, float bodyFat, float bodyWater, float muscleMass, float boneMass, float bmi) {
            this.dateTime = dateTime;
            this.weight = weight > 0 ? GlobalMethods.Round(weight) : 0;
            this.bodyFat = bodyFat > 0 ? GlobalMethods.Round(bodyFat) : 0;
            this.bodyWater = bodyWater > 0 ? GlobalMethods.Round(bodyWater) : 0;
            this.muscleMass = muscleMass > 0 ? GlobalMethods.Round(muscleMass) : 0;
            this.boneMass = boneMass > 0 ? GlobalMethods.Round(boneMass) : 0;
            this.bmi = bmi > 0 ? GlobalMethods.Round(bmi) : 0;
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
            return (dateTime, weight, bodyFat, bodyWater, muscleMass, boneMass, bmi).GetHashCode();
        }

        public override string ToString() {
            return $"Date: {dateTime.ToShortDateString()}, Weight: {weight}, Body Fat: {bodyFat}, Body Water: {bodyWater}, " +
                $"Muscle Mass: {muscleMass}, Bone Mass: {boneMass}, BMI: {bmi}";
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
            return (dateTime == other.dateTime) && (weight == other.weight) && (bodyFat == other.bodyFat) &&
                (bodyWater == other.bodyWater) && (muscleMass == other.muscleMass) && (boneMass == other.boneMass) && (bmi == other.bmi);
        }
    }
}
