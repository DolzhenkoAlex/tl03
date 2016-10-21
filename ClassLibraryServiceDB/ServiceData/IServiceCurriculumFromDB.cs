using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibraryServiceDB.Model;

namespace ClassLibraryServiceDB.ServiceData
{
    public interface IServiceCurriculumFromDB
    {
        /// <summary>
        /// Интерфейс Загрузки титулов учебных планов университета
        /// </summary>
        /// <param name="callback"></param>
        void GetDataCurriculum(Action<ObservableCollection<Планы>, Exception> callback);

        /// <summary>
        /// Интерфейс получения данных по титулам учебных планов университета
        /// </summary>
        /// <param name="curriculums"></param>
        /// <param name="idUniversity"></param>
        /// <returns></returns>
        ObservableCollection<Планы> GetCurriculum(ObservableCollection<Планы> curriculums, string academicYear);

        /// <summary>
        /// Интерфейс получения данных по дисциплинам учебного плана университета
        /// </summary>
        /// <param name="idCurriculum"></param>
        /// <returns></returns>
        ObservableCollection<ПланыСтроки> GetListDiscipline(int idCurriculum);

        /// <summary>
        /// Интерфейс формирования часовой нагрузки
        /// </summary>
        /// <param name="idPlan"></param>
        /// <returns></returns>
        List<ПланыЧасы> GetListWatch(int idPlan);

        /// <summary>
        /// Интерфейс формирования данных по титулу учебного плана
        /// </summary>
        /// <param name="?"></param>
        /// <returns></returns>
        List<ПланыТитулы> GetListTitle(int idPlan);

        /// <summary>
        /// Интерфейс формирования данных по учебным практикам
        /// </summary>
        /// <param name="idPlan"></param>
        /// <returns></returns>
        List<ПланыПрактики> GetListPractic(int idPlan);

        /// <summary>
        /// Интерфейс получения данных о квалификации
        /// </summary>
        /// <param name="idPlan"></param>
        /// <returns></returns>
        ПланыКвалификации GetQualification(int idPlan);
    }
}
