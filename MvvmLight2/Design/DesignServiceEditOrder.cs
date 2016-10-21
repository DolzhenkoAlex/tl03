using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvvmLight2.Model;

namespace MvvmLight2.Design
{
    /// <summary>
    /// Класс данных по приказам норм времени, используемых при проектировании приложения
    /// </summary>
    public class DesignServiceEditOrder
    {
        public OrderStandardTime Order { get; private set; }

        public DesignServiceEditOrder()
        {
            Order = new OrderStandardTime
                {
                    NumberOrder = "206",
                    DataOrder = new DateTime(2011, 08, 01),
                    IdUniversity = 1,
                    IdAcademicYear = 1
                };
        }
    }
}
