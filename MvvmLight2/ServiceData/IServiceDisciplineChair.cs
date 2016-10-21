using System;
using System.Collections.ObjectModel;
using MvvmLight2.Model;


namespace MvvmLight2.ServiceData
{
    /// <summary>
    /// Интерфейс работы с данными по дисциплинам кафедры
    /// </summary>
    public interface IServiceDisciplineChair
    {
        /// <summary>
        /// Интерфейс Загрузки данных по дисциплинам кафедры 
        /// для заданного учебного года
        /// </summary>
        /// <param name="callback"></param>
        /// <param name="idChair"></param>
        /// <param name="idAcademicYear"></param>
        void GetDataDiscipline(Action<ObservableCollection<tlsp_getDisciplineChair_Result>, Exception> callback, int idChair, int idAcademicYear);

        /// <summary>
        /// Интерфейс Формирования данных по дисциплинам кафедры 
        /// для заданного учебного года и всех учебных планов университета
        /// </summary>
        /// <param name="standartTime"></param>
        /// <param name="idChair"></param>
        /// <param name="idAcademicYear"></param>
        /// <returns></returns>
        ObservableCollection<tlsp_getDisciplineChair_Result> GetDiscipline(ObservableCollection<tlsp_getDisciplineChair_Result> disciplines, int idChair, int idAcademicYear);

        ObservableCollection<tlsp_getDisciplineChair_Result> GetDiscipline(int idChair, int idAcademicYear);

        /// <summary>
        /// Интерфейс Загрузки данных по дисциплинам кафедры 
        /// для заданного учебного года для очного отделения
        /// </summary>
        /// <param name="callback"></param>
        /// <param name="idChair"></param>
        /// <param name="idAcademicYear"></param>
        void GetDataDisciplineFullTime(Action<ObservableCollection<tlsp_getDisciplineChairFullTime_Result>, Exception> callback, int idChair, int idAcademicYear);

        /// <summary>
        /// Интерфейс Загрузки дисциплин кафедры 
        /// для заданного учебного года для очного отделения
        /// </summary>
        /// <param name="idChair"></param>
        /// <param name="idAcademicYear"></param>
        /// <returns></returns>
        ObservableCollection<tlsp_getDisciplineChairFullTime_Result>  GetDisciplineFullTime (int idChair, int idAcademicYear);
        
        /// <summary>
        /// Интерфейс Загрузки данных по дисциплинам кафедры 
        /// для заданного учебного года для заочного отделения
        /// </summary>
        /// <param name="callback"></param>
        /// <param name="idChair"></param>
        /// <param name="idAcademicYear"></param>
        void GetDataDisciplinePartTime(Action<ObservableCollection<tlsp_getDisciplineChairPartTime_Result>, Exception> callback, int idChair, int idAcademicYear);

        /// <summary>
        /// Интерфейс Загрузки дисциплин кафедры 
        /// для заданного учебного года для заочного отделения
        /// </summary>
        /// <param name="idChair"></param>
        /// <param name="idAcademicYear"></param>
        /// <returns></returns>
        ObservableCollection<tlsp_getDisciplineChairPartTime_Result> GetDisciplinePartTime(int idChair, int idAcademicYear);
        
        
        
        /// <summary>
        /// Интерфейс Формирования данных по дисциплинам кафедры 
        /// для заданного учебного года и конкретного учебного плана университета
        /// </summary>
        /// <param name="standartTime"></param>
        /// <param name="idChair"></param>
        /// <param name="idAcademicYear"></param>
        /// <param name="idCurrrculum"></param>
        /// <returns></returns>
        ObservableCollection<DisciplineChair> SetDisciplineOfCurriculum(ObservableCollection<DisciplineChair> disciplines, int idChair, int idAcademicYear, int idCurriculum);

        /// <summary>
        /// Интерфейс загрузки  данных по учебным планам
        /// для заданного учебного года
        /// </summary>
        /// <param name="curriculums"></param>
        /// <param name="idUniversity"></param>
        /// <returns></returns>
        ObservableCollection<Curriculum> GetCurriculum(int idAcademicYear);

        /// <summary>
        /// Интерфейс Формирования данных по дисциплинам кафедры для очного отделения
        /// для заданного учебного года и всех учебных планов университета
        /// </summary>
        /// <param name="standartTime"></param>
        /// <param name="idChair"></param>
        /// <param name="idAcademicYear"></param>
        /// <returns></returns>
        ObservableCollection<tlsp_getDisciplineChairFullTime_Result> GetDisciplineFullTime(ObservableCollection<tlsp_getDisciplineChairFullTime_Result> disciplines, int idChair, int idAcademicYear);

        /// <summary>
        /// Интерфейс Формирования данных по дисциплинам кафедры для заочного отделения
        /// для заданного учебного года и всех учебных планов университета
        /// </summary>
        /// <param name="standartTime"></param>
        /// <param name="idChair"></param>
        /// <param name="idAcademicYear"></param>
        /// <returns></returns>
        ObservableCollection<tlsp_getDisciplineChairPartTime_Result> GetDisciplinePartTime(ObservableCollection<tlsp_getDisciplineChairPartTime_Result> disciplines, int idChair, int idAcademicYear);


        /// <summary>
        /// Интерфейс формирования дисциплин кафедры
        /// для заданного учебного года
        /// на основе учебных планов университета
        /// </summary>
        /// <param name="standartTime"></param>
        /// <param name="idChair"></param>
        /// <param name="idAcademicYear"></param>
        /// <returns></returns>
        ObservableCollection<DisciplineChair> SetDiscipline(ObservableCollection<DisciplineChair> disciplines, int idChair, int idAcademicYear);

        /// <summary>
        /// Интерфейс добавления дисциплины в список дисциплин кафедры
        /// </summary>
        /// <param name="newDiscipline"></param>
        void AddItemDataDiscipline(DisciplineChair newDiscipline);

        /// <summary>
        /// Интерфейс удаления дисциплины из списка дисциплин кафедры
        /// </summary>
        /// <param name="delDisciplineId"></param>
        void RemoveItemDiscipline(int delDisciplineId);

        bool RemoveItemDiscipline(tlsp_getDisciplineChair_Result delDiscipline, out string message);

        /// <summary>
        /// Интерфейс удаления всех дисциплин кафедры
        /// </summary>
        /// <param name="delDisciplineID"></param>
        void RemoveALLDiscipline(int idAcademicYear, int idChair);

        bool RemoveALLDiscipline(int idAcademicYear, int idChair, out string message );
    }
}
