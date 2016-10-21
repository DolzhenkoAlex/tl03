using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvvmLight2.Helper
{
    /// <summary>
    /// Источник данных для отчета Сводные данные по трудоемкости
    /// </summary>
    public class ListSummaryCurriculumWork : ObservableCollection<SummaryOfCurriculumWork>
    {
        /// <summary>
        /// Добавление коллекции данных по трудоемкости
        /// </summary>
        /// <param name="works"></param>
        /// <returns></returns>
        public ListSummaryCurriculumWork AddWork(ObservableCollection<SummaryOfCurriculumWork> works)
        {
            foreach (var work in works)
                this.Add(work);

            return this;
        }
    }
}
