using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvvmLight2.Model;

namespace MvvmLight2.Helper
{
    /// <summary>
    /// Справочние видов трудовых отношений
    /// </summary>
    public class ListTypeEmployment : ObservableCollection<DictTypeEmployment>
    {
        public ListTypeEmployment()
        {
            using (var context = new DBTeachingLoadEntities())
            {
                var QueryTypeEmployment = from type in context.DictTypeEmployments
                                          where type.StatusDel == true
                                          select type;
                foreach (DictTypeEmployment t in QueryTypeEmployment)
                    this.Add(t);
            }
        }
    }
}
