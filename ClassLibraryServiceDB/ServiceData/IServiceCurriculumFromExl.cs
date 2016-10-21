using ClassLibraryServiceDB.Model;
using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;

namespace ClassLibraryServiceDB.ServiceData
{
    public interface IServiceCurriculumFromExl
    {
        /// <summary>
        /// Интерфейс формирования данных по учебному плану
        /// </summary>
        /// <param name="workbook"></param> // книга Excel
        /// <returns></returns>
        TitleCurriculum GetCurriculum(Excel.Workbook workbook);

        /// <summary>
        /// Интерфейс формирования  данных по дисциплинам учебного плана для очного обучения
        /// </summary>
        /// <returns></returns>
        //List<Discipline> GetListDiscipline(Workbook title);

        /// <summary>
        /// Интерфейс формирования  данных по дисциплинам учебного плана для заочного обучения
        /// </summary>
        /// <returns></returns>
        //List<Discipline> GetListDisciplineDistanceLearning(Workbook title);

    }
}
