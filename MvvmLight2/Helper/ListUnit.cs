using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvvmLight2.Model;

namespace MvvmLight2.Helper
{
    public class ListUnit : ObservableCollection<DictUnit>
    {
        public ListUnit()
        {
            using (var context = new DBTeachingLoadEntities())
            {
                var QueryUnit = from u in context.DictUnits
                                where u.StatusDel == true
                                orderby u.Unit
                                select u;
                foreach (DictUnit e in QueryUnit)
                    this.Add(e);
            }
        }
    }
}
