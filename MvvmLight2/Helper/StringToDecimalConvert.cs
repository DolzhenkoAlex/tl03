using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace MvvmLight2.Helper
{
    
   /// <summary>
    /// Конвертер строки в формам Decimal
   /// </summary>
   [ValueConversion(typeof(decimal),typeof(string))]
    public class StringToDecimalConverter:IValueConverter
    {
       public object Convert(object value,Type typeTarget, object param, CultureInfo culture)
       {
           if (value != null)
               return value.ToString();
           else
               return string.Empty;
       }

       public object ConvertBack(object value, Type typeTarget, object param, CultureInfo culture)
       {
           if ((value != null) && (value.ToString() != ""))
               return Decimal.Parse(value.ToString(), NumberStyles.AllowDecimalPoint);
           else
               return 0;
       }
    }
}
