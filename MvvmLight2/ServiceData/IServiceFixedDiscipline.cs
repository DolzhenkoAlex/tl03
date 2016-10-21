using MvvmLight2.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvvmLight2.ServiceData
{
    public interface IServiceFixedDiscipline
    {
        /// <summary>
        /// Интерфейс Формирования данных по закрепленным дисциплинам 
        /// </summary>
        /// <param name="disciplines"></param>
        /// <param name="idAcademicYear"></param>
        /// <returns></returns>
        ObservableCollection<tlsp_getFixedDisciplines_Result> GetAllFixedDisciplines(int idAcademicYear);
    }
}
