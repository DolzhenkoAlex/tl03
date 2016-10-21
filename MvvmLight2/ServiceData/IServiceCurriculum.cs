using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvvmLight2.Model;

namespace MvvmLight2.ServiceData
{
    public interface IServiceCurriculum
    {
        /// <summary>
        /// Интерфейс Загрузки данных по учебным планам
        /// </summary>
        /// <param name="callback"></param>
        void GetDataCurriculum(Action<ObservableCollection<Curriculum>, Exception> callback, int idAcademicYear);

        /// <summary>
        /// Интерфейс получения данных по учебным планам
        /// </summary>
        /// <param name="curriculums"></param>
        /// <param name="idUniversity"></param>
        /// <returns></returns>
        ObservableCollection<Curriculum> GetCurriculum(ObservableCollection<Curriculum> curriculums, int idAcademicYear);

        /// <summary>
        /// Интерфейс Загрузки данных по дисциплинам учебного плана
        /// </summary>
        /// <param name="standartTime"></param>
        /// <param name="IdCurriculum"></param>
        /// <returns></returns>
        ObservableCollection<DisciplinePlan> GetDiscipline(ObservableCollection<DisciplinePlan> disciplines, int IdCurriculum);

        /// <summary>
        /// Интерфейс формирования данных для отчета Закрепление дисциплин
        /// </summary>
        /// <param name="idAcademicYear"></param>
        /// <returns></returns>
        ObservableCollection<tlsp_getAllDiscipline_Result> GetAllDisciplines(int idAcademicYear);

        
        /// <summary>
        /// Интерфейс добавления данных по новому учебному плану
        /// </summary>
        /// <param name="curriculum"></param>
        void AddItemCurriculum(Curriculum newCurriculum);

        /// <summary>
        /// Интерфейс редактирования данных по учебному плану
        /// </summary>
        /// <param name="editGroup"></param>
        void EditItemCurriculum(Curriculum editCurriculum);

        /// <summary>
        /// Интерфейс удаления данных по учебному плану
        /// </summary>
        /// <param name="delCurriculum"></param>
        void RemoveItemCurriculum(Curriculum delCurriculum);

        /// <summary>
        /// Интерфейс добавления новых данных по дисциплине учебного плана
        /// </summary>
        /// <param name="newDiscipline"></param>
        void AddItemDiscipline(DisciplinePlan newDiscipline);

        /// <summary>
        /// Интерфейс редактирования данных по дисциплине учебного плана
        /// </summary>
        /// <param name="editDiscipline"></param>
        void EditItemDiscipline(DisciplinePlan editDiscipline);

        /// <summary>
        /// Интерфейс удаления данных по дисциплине учебного плана
        /// </summary>
        /// <param name="delDiscipline"></param>
        void RemoveItemDiscipline(DisciplinePlan delDiscipline);

        /// <summary>
        /// Интерфейс удаления данных по дисциплине учебного плана с возвратом успеха операции
        /// </summary>
        /// <param name="delDiscipline"></param>
        /// <returns></returns>
        bool RemoveItemDiscipline(DisciplinePlan delDiscipline, out string message);


        /// <summary>
        /// Интерфейс получения титульных данных учебных планов  для отчета по трудоемкости
        /// </summary>
        /// <param name="idAcademicYear"></param>
        /// <returns></returns>
        ObservableCollection<tlsp_getCurriculumForComplexReport_Result> GetCurriculumForComplexReport(
            ObservableCollection<tlsp_getCurriculumForComplexReport_Result> curriculumForReport, int idAcademicYear);


        /// <summary>
        /// Интерфейс получения количества студентов, обучающихся по каждому учебному плану
        /// </summary>
        /// <param name="idAcademicYear"></param>
        /// <param name="course"></param>
        /// <returns></returns>
        ObservableCollection<tlsp_getCurriculumWithStudent_Result> GetCurriculumWithStudent(
            ObservableCollection<tlsp_getCurriculumWithStudent_Result> curriculumWithStudent, int idAcademicYear);
    }
}
