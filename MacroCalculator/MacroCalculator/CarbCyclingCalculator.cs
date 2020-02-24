namespace MacroCalculator
{
    public class CarbCyclingCalculator
    {
        public CarbCyclingCalculator(double heightInInches, double weightInPounds, int ageInYears, double activityFactor = 1.5, int dailyDeficit = 500)
        {
            HeightInInches = heightInInches;
            WeightInPounds = weightInPounds;
            AgeInYears = ageInYears;
            ActivityFactor = activityFactor;
            BMR = 66 + (6.3 * WeightInPounds) + (12.9 * HeightInInches) - (6.8 * AgeInYears);
            TDEE = BMR * ActivityFactor;
            CaloricGoal = TDEE - dailyDeficit;
            WeeklyWeightDelta = (double)dailyDeficit * 7 / 3500.0;
        }

        public Macro LowCarbDayMacros =>
            new Macro
            {
                Carbohydrate = 0.25 * WeightInPounds,
                Protein = CaloricGoal * 0.35 / 4,
                Fat = CaloricGoal * 0.4 / 9
            };

        public Macro ModerateCarbDayMacros =>
            new Macro
            {
                Carbohydrate = 0.75 * WeightInPounds,
                Protein = CaloricGoal * 0.35 / 4,
                Fat = CaloricGoal * 0.35 / 9
            };

        public Macro HighCarbDayMacros =>
            new Macro
            {
                Carbohydrate = 1.75 * WeightInPounds,
                Protein = CaloricGoal * 0.25 / 4,
                Fat = CaloricGoal * 0.25 / 9
            };

        public double HeightInInches { get; }
        public double WeightInPounds { get; }
        public int AgeInYears { get; }
        public double ActivityFactor { get; }
        public double BMR { get; }
        public double TDEE { get; }
        public double CaloricGoal { get; }
        public double WeeklyWeightDelta { get; }
    }
}
