using System;
using System.Collections.ObjectModel;
using System.Linq;
using MvvmLight2.Model;

namespace MvvmLight2.Helper
{
    /// <summary>
    /// Справочние квалификаций
    /// </summary>
    public class ListQualification : ObservableCollection<DictQualification>
    {
        public ListQualification()
        {
            using (var context = new DBTeachingLoadEntities())
            {
                var QueryQualification = from q in context.DictQualifications
                                         where q.StatusDel == true
                                         //orderby q.NameQualification
                                        select q;

                foreach (DictQualification q in QueryQualification)
                    this.Add(q);
            }
        }

        public DictQualification FindQualification(
            ObservableCollection<DictQualification> listQualification,
            string qualification,
            Func<DictQualification, string, bool> predicate)
        {
            DictQualification qual = new DictQualification();
            foreach (var q in listQualification)
            {
                if (predicate(q, qualification))
                {
                    qual = q;
                    break;
                }
            }
            return qual;
        }
    }
}
