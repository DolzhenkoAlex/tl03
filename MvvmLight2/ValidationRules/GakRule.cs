using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace MvvmLight2.ValidationRules
{
    /// <summary>
    /// Правило корректности ввода объема работ члена ГАК/ГЭК
    /// </summary>
    public class GakRule:ValidationRule
    {
        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            decimal gak;
            try
            {
                if (((string)value).Length > 0)
                    gak = Decimal.Parse(value.ToString(), NumberStyles.AllowDecimalPoint);
                else
                    gak = 0;
                return new ValidationResult(true,null);
            }
            catch
            {
                return new ValidationResult(false, "Введен недопустимый символ");
            }
        }
    }
}
