using System.Collections.ObjectModel;
using System.Linq;
using MvvmLight2.Model;

namespace MvvmLight2.Helper
{
    public class ListTypeWork : ObservableCollection<DictTypeTraining>
    {
        public ListTypeWork()
        {
            using (var context = new DBTeachingLoadEntities())
            {
                var QueryTypeWork = from work in context.DictTypeTrainings
                                    where work.StatusDel == true
                                    orderby work.TypeWork
                                    select work;
                foreach (DictTypeTraining e in QueryTypeWork)
                    this.Add(e);
            }
        }
    }
}
