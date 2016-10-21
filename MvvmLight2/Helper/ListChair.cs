using System;
using System.Collections.ObjectModel;
using System.Linq;
using MvvmLight2.Model;

namespace MvvmLight2.Helper
{
    public class ListChair: ObservableCollection<Chair>
    {
        public ListChair()
        {
            using (var context = new DBTeachingLoadEntities())
            {
                var QueryChair = from chair in context.Chairs
                                 where chair.StatusDel == true
                                 orderby chair.NameChair
                                 select chair;
                foreach (var c in QueryChair)
                    this.Add(c);
            }
        }
    }
}
