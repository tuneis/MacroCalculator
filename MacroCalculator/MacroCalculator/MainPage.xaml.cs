using System;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MacroCalculator
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            Appearing += (object sender, EventArgs e) => TxtWeight.Focus();
        }

        /// <summary>
        /// Weight calculated.
        /// </summary>
        /// <returns></returns>
        private async Task<bool> IsWeightEntered()
        {
            if (string.IsNullOrWhiteSpace(TxtWeight.Text))
            {
                await DisplayAlert("Fail", "Uhh, you forgot to enter in your weight.\n\nThis is the only error check.\n\nIf you fail to enter any other information this app will crash or give you the wrong results. That's completely on you if you're starving or gain weight due to following incorrectly calculated macros.\n\nDon't be a bad end-user, finish filling in the form.", "Aww Shoot!");
                TxtWeight.Focus();
                return false;
            }
            return true;
        }

        /// <summary>
        /// Calculates flex dieting results.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        private async void CalculateFlexDieting_Clicked(object sender, EventArgs e)
        {
            if (await IsWeightEntered())
                await DisplayAlert("Flex Dieting Results", CalculateFlexDieting(), "OK");
        }

        /// <summary>
        /// Calculates flex dieting.
        /// </summary>
        /// <returns></returns>
        private string CalculateFlexDieting()
        {
            // flex dieting
            var fd = new FlexDietingCalculator(
                Convert.ToDouble(TxtHeight.Text),
                Convert.ToDouble(TxtWeight.Text),
                Convert.ToInt32(TxtAge.Text),
                Convert.ToDouble(TxtActivityFactor.Text),
                Convert.ToDouble(TxtDailyDeficitPercentage.Text),
                Convert.ToDouble(TxtCarbohydrate.Text),
                Convert.ToDouble(TxtProtein.Text),
                Convert.ToDouble(TxtFat.Text)
            );
            var sb = new StringBuilder();
            sb.AppendLine($"Daily Caloric Goal: {fd.CaloricGoal} cal\n");
            sb.AppendLine($"Calorie Deficit: {fd.CalorieDeficit} cal\n");
            sb.AppendLine($"TDEE: {fd.TDEE} cal\n");
            sb.AppendLine($"BMR: {fd.BMR} cal\n");
            sb.AppendLine($"Macro Split (%): {TxtCarbohydrate.Text}% {TxtProtein.Text}% {TxtFat.Text}%\n");
            sb.AppendLine($"Macros:\n{fd.Macros.ToString()}\n");
            var sign = fd.CalorieDeficit >= 0 ? "-" : "+";
            sb.AppendLine($"Weekly Weight Delta (+/-):\n{sign}{fd.WeeklyWeightDelta} lbs\n");
            return sb.ToString();
        }

        /// <summary>
        /// Calculates carb cycling results.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        private async void CalculateCarbCycling_Clicked(object sender, EventArgs e)
        {
            if (await IsWeightEntered())
                await DisplayAlert("Carb Cycling Results", CalculateCarbCycling(), "OK");
        }

        /// <summary>
        /// Calculates carb cycling.
        /// </summary>
        /// <returns></returns>
        private string CalculateCarbCycling()
        {

            var deficit = Convert.ToInt32(TxtDailyDeficit.Text);
            // carb cycling
            var ccc = new CarbCyclingCalculator(
                Convert.ToDouble(TxtHeight.Text),
                Convert.ToDouble(TxtWeight.Text),
                Convert.ToInt32(TxtAge.Text),
                Convert.ToDouble(TxtActivityFactor.Text),
               deficit
            );
            var sb = new StringBuilder();
            sb.AppendLine($"Avg. Caloric Goal: {ccc.CaloricGoal} cal\n");
            sb.AppendLine($"Calorie Deficit: {TxtDailyDeficit.Text} cal\n");
            sb.AppendLine($"TDEE: {ccc.TDEE} cal\n");
            sb.AppendLine($"BMR: {ccc.BMR} cal\n");
            sb.AppendLine($"Low Carb Day:\n{ccc.LowCarbDayMacros.ToString()}\n");
            sb.AppendLine($"Moderate Carb Day:\n{ccc.ModerateCarbDayMacros.ToString()}\n");
            sb.AppendLine($"High Carb Day:\n{ccc.HighCarbDayMacros.ToString()}\n");
            var sign = deficit >= 0 ? "-" : "+";
            sb.AppendLine($"Weekly Weight Delta (+/-):\n{sign}{ccc.WeeklyWeightDelta} lbs\n");
            return sb.ToString();
        }
    }
}
