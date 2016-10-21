using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvvmLight2.Model;

namespace MvvmLight2.Helper
{
    /// <summary>
    /// Фильтр выбора академического года
    /// </summary>
    public class FilterAcademicYear
    {
        /// <summary>
        /// Индекс выбора варианта фильтрации дисциплин
        /// </summary>
        int filter;

        public FilterAcademicYear(int filter)
        {
            this.filter = filter;
        }

        public bool FilterItem(object item)
        {
            StudentGroup group = item as StudentGroup;

            if (filter == group.IdAcademicYear)
                return true;
            else
                return false;
        }
    }
}
