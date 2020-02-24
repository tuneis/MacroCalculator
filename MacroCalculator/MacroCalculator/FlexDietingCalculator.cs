namespace MacroCalculator
{
    public class FlexDietingCalculator
    {
        public FlexDietingCalculator(
            double heightInInches,
            double weightInPounds,
            int ageInYears,
            double activityFactor = 1.55,
            double dailyDeficitPercentage = 20,
            double carbohydratePercentage = 50,
            double proteinPercentage = 25,
            double fatPercentage = 25
        )
        {
            HeightInInches = heightInInches;
            WeightInPounds = weightInPounds;
            AgeInYears = ageInYears;
            ActivityFactor = activityFactor;
            BMR = 66 + (6.3 * WeightInPounds) + (12.9 * HeightInInches) - (6.8 * AgeInYears);
            TDEE = BMR * ActivityFactor;
            CaloricGoal = TDEE * (1 - (dailyDeficitPercentage / 100.0));
            CalorieDeficit = TDEE - CaloricGoal;
            CarbohydratePercentage = carbohydratePercentage / 100.0;
            ProteinPercentage = proteinPercentage / 100.0;
            FatPercentage = fatPercentage / 100.0;
            WeeklyWeightDelta = CalorieDeficit * 7 / 3500.0;
        }

        public Macro Macros =>
            new Macro
            {
                Carbohydrate = CaloricGoal * CarbohydratePercentage / 4,
                Protein = CaloricGoal * ProteinPercentage / 4,
                Fat = CaloricGoal * FatPercentage / 9
            };

        public double BMR { get; }
        public double TDEE { get; }
        public double CaloricGoal { get; }
        public double CalorieDeficit { get; }
        public double WeeklyWeightDelta { get; }
        public double FatPercentage { get; }
        public double ProteinPercentage { get; }
        public double CarbohydratePercentage { get; }
        public double ActivityFactor { get; }
        public int AgeInYears { get; }
        public double WeightInPounds { get; }
        public double HeightInInches { get; }
    }
}
