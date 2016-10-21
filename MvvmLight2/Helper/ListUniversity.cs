using System.Collections.ObjectModel;
using System.Linq;
using MvvmLight2.Model;

namespace MvvmLight2.Helper
{
    class ListUniversity: ObservableCollection<University>
    {
        public ListUniversity()
        {
            using (var context = new DBTeachingLoadEntities())
            {
                var QueryUniversity = from univer in context.Universities
                                      where univer.StatusDel == true
                                      select univer;
                foreach (var u in QueryUniversity)
                    this.Add(u);
            }
        }
    }
}

