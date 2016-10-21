using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvvmLight2.Model;

namespace MvvmLight2.Design
{
    /// <summary>
    /// Класс данных приказов по нормам времени, используемых при проектировании приложения
    /// </summary>
    public class DesignerServiceOrder
    {
        public ObservableCollection<OrderStandardTime> Orders { get; private set; }

        public ObservableCollection<StandartTime> StandartsTime { get; private set; }

        public DesignerServiceOrder()
        {
            Orders = new ObservableCollection<OrderStandardTime>
            {
                new OrderStandardTime
                {
                    NumberOrder = "206",
                    DataOrder = new DateTime(2011, 08, 01),
                    IdUniversity = 1,
                    IdAcademicYear = 1
                },

                new OrderStandardTime
                {
                    NumberOrder = "306",
                    DataOrder = new DateTime(2012, 08, 01),
                    IdUniversity = 1,
                    IdAcademicYear = 1
                },

                new OrderStandardTime
                {
                    NumberOrder = "406",
                    DataOrder = new DateTime(2013, 08, 01),
                    IdUniversity = 1,
                    IdAcademicYear = 1
                },
            };

            StandartsTime = new ObservableCollection<StandartTime> 
            {
                new StandartTime()
                {
                    TypeOfWork = "Чтение лекций",
                    NormTime = 1,
                    Notes = "для дневной формы обучения"
                },
                new StandartTime()
                {
                    TypeOfWork = "Проведение лаб/практ. работ",
                    NormTime = 1,
                    Notes = "для дневной формы обучения"
                },
                new StandartTime()
                {
                    TypeOfWork = "Чтение обзорных лекций",
                    NormTime = 12,
                    Notes = "для заочной формы обучения"
                }, 
                new StandartTime()
                {
                    TypeOfWork = "Проведение текущих консультаций ",
                    NormTime = 5,
                    Notes = "для дневной формы обучения"
                }

            };
        }
    }
}
