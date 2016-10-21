using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;
using MvvmLight2.Model;

namespace MvvmLight2.Helper
{
    /// <summary>
    /// Список приказов по нормам времени
    /// </summary>
    public class ListOrderStandartTime: ObservableCollection<OrderStandardTime>
    {
        public ListOrderStandartTime()
        {
            //AppSettingsReader ar = new AppSettingsReader();
            //int idUniver = (int)ar.GetValue("IdUniversity", typeof(int));

            int idUniver = 1;

            using (var context = new DBTeachingLoadEntities())
            {
                var QueryOrder = from ord in context.OrderStandardTimes
                                 .Include("DictAcademicYear")
                                 where (ord.IdUniversity == idUniver) && (ord.StatusDel == true)
                                 orderby ord.NumberOrder
                                 select ord;
                foreach (OrderStandardTime ord in QueryOrder)
                    this.Add(ord);
            }
        }
    }
}

