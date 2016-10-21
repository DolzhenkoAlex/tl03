using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using ClassLibraryServiceDB.Model;

namespace MvvmLight2.Helper
{
     [ValueConversion(typeof(Планы), typeof(string))]
    public class DurationStudyConvert : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string qualification = String.Empty;

            if (value != null)
            {
                Планы plan = value as Планы;
                if (plan.ПланыКвалификации.Count > 0)
                    qualification = plan.ПланыКвалификации.First().СрокОбучения;
            }
            return qualification;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException("Отсутствует ConvertBack");
        }
    }
}
