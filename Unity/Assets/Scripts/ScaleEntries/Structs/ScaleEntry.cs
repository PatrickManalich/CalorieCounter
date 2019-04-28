﻿using CalorieCounter.Globals;
using System;

namespace CalorieCounter.ScaleEntries {

    public struct ScaleEntry {

        public DateTime Date;
        public float Weight;
        public float BodyFat;
        public float BodyWater;
        public float MuscleMass;
        public float BoneMass;
        public float Bmi;

        public ScaleEntry(float weight, float bodyFat, float bodyWater, float muscleMass, float boneMass, float bmi) {
            Date = DateTime.Today;
            Weight = weight > 0 ? GlobalMethods.Round(weight) : 0;
            BodyFat = bodyFat > 0 ? GlobalMethods.Round(bodyFat) : 0;
            BodyWater = bodyWater > 0 ? GlobalMethods.Round(bodyWater) : 0;
            MuscleMass = muscleMass > 0 ? GlobalMethods.Round(muscleMass) : 0;
            BoneMass = boneMass > 0 ? GlobalMethods.Round(boneMass) : 0;
            Bmi = bmi > 0 ? GlobalMethods.Round(bmi) : 0;
        }

        public override string ToString() {
            return Date.ToShortDateString() + ", [ Weight:" + Weight + ", BodyFat:" + BodyFat + ", BodyWater:" + BodyWater + ", MuscleMass:" + MuscleMass + ", BoneMass:" + BoneMass + ", Bmi:" + Bmi + " ]";
        }
    }
}
