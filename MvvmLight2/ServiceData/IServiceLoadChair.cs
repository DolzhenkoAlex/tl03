using System;
using System.Collections.ObjectModel;
using MvvmLight2.Model;

namespace MvvmLight2.ServiceData
{
    /// <summary>
    /// Интерфейс работы с данными по нагрузке кафедры
    /// </summary>
    public interface IServiceLoadChair
    {
        /// <summary>
        /// Интерфейс загрузки данных по нагрузке кафедры из базы данных
        /// </summary>
        /// <param name="callback"></param>
        /// <param name="IdChair"></param>
        /// <param name="IdAcademicYear"></param>
        void GetDataLoadChairTeaching(Action<ObservableCollection<TeachingLoad>, Exception> callback, int idChair, int idAcademicYear);


        /// <summary>
        /// Интерфейс загрузки обобщенных данных по нагрузке кафедры из базы данных для заданного учебного года
        /// </summary>
        /// <param name="callback"></param>
        /// <param name="idChair"></param>
        /// <param name="idAcademicYear"></param>
        void GetDataAllLoadChair(Action<ObservableCollection<TeachingLoadChair>, Exception> callback, int idChair, int idAcademicYear);

        /// <summary>
        /// Интерфейс получения обобщенных данных по нагрузке кафедры из базы данных для заданного учебного года
        /// </summary>
        /// <param name="allLoadChair"></param>
        /// <param name="idChair"></param>
        /// <param name="idAcademicYear"></param>
        /// <returns></returns>
        ObservableCollection<TeachingLoadChair> GetAllLoadChair(ObservableCollection<TeachingLoadChair> allLoadChair, int idChair, int idAcademicYear);

        ObservableCollection<TeachingLoadChair> GetAllLoadChair(int idChair, int idAcademicYear);

        /// <summary>
        /// Интерфейс получения  данных по дисциплинам нагрузки кафедры для заданного варианта нагрузки
        /// </summary>
        /// <param name="callback"></param>
        /// <param name="idChair"></param>
        /// <param name="idAcademicYear"></param>
        /// <returns></returns>
        ObservableCollection<TeachingLoad> GetLoadChairTeaching(ObservableCollection<TeachingLoad> loadChair, int idChair, int idAcademicYear, string nameLoad);

        ObservableCollection<TeachingLoad> GetLoadChairTeaching(int idChair, int idAcademicYear, string nameLoad);

        /// <summary>
        /// Интерфейс получения данных по дисциплинам нагрузки кафедры для заданного варианта нагрузки для очного отделения
        /// </summary>
        /// <param name="callback"></param>
        /// <param name="idChair"></param>
        /// <param name="idAcademicYear"></param>
        /// <returns></returns>
        ObservableCollection<TeachingLoad> GetLoadChairTeachingFullTime(ObservableCollection<TeachingLoad> loadChair, int idChair, int idAcademicYear, string nameLoad);

        /// <summary>
        /// Интерфейс получения данных по дисциплинам нагрузки кафедры для заданного варианта нагрузки для заочного отделения
        /// </summary>
        /// <param name="callback"></param>
        /// <param name="idChair"></param>
        /// <param name="idAcademicYear"></param>
        /// <returns></returns>
        ObservableCollection<TeachingLoad> GetLoadChairTeachingPartTime(ObservableCollection<TeachingLoad> loadChair, int idChair, int idAcademicYear, string nameLoad);

        /// <summary>
        /// Интерфейс получения данных по расределенным/нераспределенным дисциплинам кафедры 
        /// </summary>
        /// <param name="loadChair"></param>
        /// <param name="idChair"></param>
        /// <param name="idAcademicYear"></param>
        /// <param name="nameTeacher"></param>
        /// <param name="isLoad"></param>
        /// <returns></returns>
        ObservableCollection<TeachingLoad> GetDisciplinesChairIsLoad(ObservableCollection<TeachingLoad> loadChair, int idChair, int idAcademicYear, string nameLoad, bool isLoad);

        /// <summary>
        /// Интерфейс получения обобщенных данных по нагрузке кафедры
        /// </summary>
        /// <param name="teach"></param>
        /// <param name="idChair"></param>
        /// <param name="idAcademicYear"></param>
        /// <returns></returns>
        TeachingLoadChair GetLoadChair(TeachingLoadChair load, int idChair, int? idAcademicYear, string nameLoadChair);
        

        /// <summary>
        /// Интерфейс получения кафедр факультета
        /// </summary>
        /// <param name="chairs"></param>
        /// <param name="idFaculty"></param>
        /// <returns></returns>
        //ObservableCollection<Chair> GetChair(ObservableCollection<Chair> chairs, int idFaculty);

        /// <summary>
        /// Интерфейс загрузки дисциплин кафедры
        /// </summary>
        /// <param name="idChair"></param>
        /// <param name="idAcademicYear"></param>
        /// <returns></returns>
        ObservableCollection<tlsp_setDisciplineChairWithGroup_Result> GetDisciplinesChair(int idChair, int? idAcademicYear);

        /// <summary>
        /// Интерфейс загрузки норм времени
        /// </summary>
        /// <param name="IdAcademicYear"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        ObservableCollection<tlsp_getStandartTime_Result> GetStandartTime(int? IdAcademicYear, bool status);

        /// <summary>
        /// Интерфейс редактирования данных по дисциплине 
        /// </summary>
        /// <param name="editTeachingLoadChair"></param>
        void EditItemTeachingLoadChair(TeachingLoad editTeachingLoadChair);

        /// <summary>
        /// Интерфейс редактирования общих данных по нагрузке кафедры
        /// </summary>
        /// <param name="editLoadChair"></param>
        void EditItemLoadChair(TeachingLoadChair editLoadChair);

        /// <summary>
        /// Интерфейс добавления новой нагрузки в список нагрузки кафедры
        /// </summary>
        /// <param name="newLoadChair"></param>
        void AddItemLoadChair(TeachingLoad newLoadChair);

        /// <summary>
        /// Интерфейс добавления новой обобщенной нагрузки кафедры
        /// </summary>
        /// <param name="newLoad"></param>
        void AddLoadChair(TeachingLoadChair newLoad);

        /// <summary>
        /// Интерфейс удаления нагрузки в списке нагрузки кафедры
        /// </summary>
        /// <param name="delLoadChair"></param>
        void RemoveItemLoadChair(TeachingLoad delLoadChair);

        /// <summary>
        /// Интерфейс удаления нагрузки в списке нагрузки кафедры с возвратом успеха операции
        /// </summary>
        /// <param name="delLoadChair"></param>
        bool RemoveItemLoadChair(TeachingLoad delLoadChair, out string messageError);

        /// <summary>
        /// Интерфейс удаления нагрузки
        /// </summary>
        /// <param name="delLoad"></param>
        void RemoveLoad(TeachingLoadChair delLoad);

        /// <summary>
        /// Интерфейс получения данных по дисциплинам нагрузки кафедры для заданного варианта нагрузки для очного отделения
        /// </summary>
        /// <param name="callback"></param>
        /// <param name="idChair"></param>
        /// <param name="idAcademicYear"></param>
        /// <returns></returns>
        ObservableCollection<TeachingLoad> GetLoadChairTeachingFullTime(int idChair, int idAcademicYear, string nameLoad);

        /// <summary>
        /// Интерфейс получения данных по дисциплинам нагрузки кафедры для заданного варианта нагрузки для заочного отделения
        /// </summary>
        /// <param name="callback"></param>
        /// <param name="idChair"></param>
        /// <param name="idAcademicYear"></param>
        /// <returns></returns>
        ObservableCollection<TeachingLoad> GetLoadChairTeachingPartTime(int idChair, int idAcademicYear, string nameLoad);

    }
}
