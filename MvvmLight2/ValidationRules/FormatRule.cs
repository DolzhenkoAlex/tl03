using System.Windows.Controls;

namespace MvvmLight2.ValidationRules
{
    /// <summary>
    /// Проверка наличия отрицательных и нечисловых значений
    /// </summary>
    public class FormatRule : ValidationRule
    {
        double result;
        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {

            if (double.TryParse(value.ToString(), out result))
                if (result >= 0)
                    return new ValidationResult(true, null);

            return new ValidationResult(false, "Нарушен формат ввода");
        }
    }
}
