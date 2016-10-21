using System;
using System.Collections.ObjectModel;
using System.Linq;
using MvvmLight2.Model;

namespace MvvmLight2.Helper
{
    /// <summary>
    /// Справочник направлений подготовки и профилей
    /// </summary>
    public class ListSpeciality : ObservableCollection<DictSpeciality>
    {
        public ListSpeciality()
        {
            using (var context = new DBTeachingLoadEntities())
            {
                var QuerySpeciality = from spec in context.DictSpecialities
                                      .Include("DictQualification")
                                      where spec.StatusDel == true
                                      orderby spec.CodeSpeciality
                                      select spec;
                foreach (DictSpeciality s in QuerySpeciality)
                    this.Add(s);
            }
        }

        public DictSpeciality FindSpeciality(
            ObservableCollection<DictSpeciality> listSpeciality,
            string speciality,
            int idQualification,
            Func<DictSpeciality, string, int, bool> predicate)
        {
            DictSpeciality spec = new DictSpeciality();
            foreach (var sp in listSpeciality)
            {
                if (predicate(sp, speciality, idQualification))
                {
                    spec = sp;
                    break;
                }
            }
            return spec;
        }
    }
}
