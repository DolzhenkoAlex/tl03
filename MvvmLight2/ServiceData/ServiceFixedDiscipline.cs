using MvvmLight2.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvvmLight2.ServiceData
{
    public class ServiceFixedDiscipline:IServiceFixedDiscipline
    {
        /// <summary>
        /// Сервис формирования данных по закрепленным дисциплинам 
        /// </summary>
        /// <param name="disciplines"></param>
        /// <param name="idAcademicYear"></param>
        /// <returns></returns>
        public ObservableCollection<tlsp_getFixedDisciplines_Result> GetAllFixedDisciplines(int idAcademicYear)
        {
            ObservableCollection<tlsp_getFixedDisciplines_Result> disciplines = new ObservableCollection<tlsp_getFixedDisciplines_Result>(); 
            
            using (var context = new DBTeachingLoadEntities())
            {
                try
                {
                    var query = context.tlsp_getFixedDisciplines(idAcademicYear);

                    foreach (tlsp_getFixedDisciplines_Result disc in query)
                        disciplines.Add(disc);
                }
                catch (EntityException ex)
                {
                    //throw new ApplicationException("Ошибка загрузки данных" + ex.ToString());
                }
            }
            return disciplines;
        }

        
    }
}
