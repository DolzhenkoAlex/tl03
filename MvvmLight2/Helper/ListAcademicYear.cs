using System;
using System.Collections.ObjectModel;
using System.Linq;
using MvvmLight2.Model;

namespace MvvmLight2.Helper
{
    /// <summary>
    /// Справочник учебных годов
    /// </summary>
    public class ListAcademicYear : ObservableCollection<DictAcademicYear>
    {
        public ListAcademicYear()
        {
            using (var context = new DBTeachingLoadEntities())
            {
                var QueryAcademicYear = from academicYear in context.DictAcademicYears
                                        where academicYear.StatusDel == true
                                        orderby academicYear.Year
                                        select academicYear;

                foreach (DictAcademicYear year in QueryAcademicYear)
                    this.Add(year);
            }
        }

        public DictAcademicYear FindAcademicYear(
            ObservableCollection<DictAcademicYear> listYears,
            string year,
            Func<DictAcademicYear, string, bool> predicate)
        {
            DictAcademicYear academicYear = new DictAcademicYear();
            foreach (var y in listYears)
            {
                if (predicate(y, year))
                {
                    academicYear = y;
                    break;
                }
            }
            return academicYear;
        }
    }
}
