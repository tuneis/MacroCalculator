using System;

namespace MacroCalculator
{
    public class Macro
    {
        public double Protein { get; set; }
        public double Carbohydrate { get; set; }
        public double Fat { get; set; }
        public override string ToString()
        {
            return $"{Math.Round(Carbohydrate)}g C, {Math.Round(Protein)}g P, {Math.Round(Fat)}g F";
        }
    }
}
