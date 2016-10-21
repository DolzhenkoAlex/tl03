using System.Windows.Controls;

namespace MvvmLight2.ValidationRules
{
    /// <summary>
    /// Проверка превышения планового значения распределения для дисциплины
    /// </summary>
    public class ExamenRulecs : ValidationRule
    {
        bool isFaultLoad = true;

        public bool IsFaultLoad
        {
            get { return isFaultLoad; }
            set { isFaultLoad = value; }
        }

        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            if (IsFaultLoad)
                return new ValidationResult(true, null);
            else
                return new ValidationResult(false, "Превышение распределения");
        }
    }
}
