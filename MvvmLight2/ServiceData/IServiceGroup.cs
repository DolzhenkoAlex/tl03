using System;
using System.Collections.ObjectModel;
using MvvmLight2.Helper;
using MvvmLight2.Model;

namespace MvvmLight2.ServiceData
{
    /// <summary>
    /// Интерфейс работы с данными по студенческим группам
    /// </summary>
    public interface IServiceGroup
    {
        /// <summary>
        /// Интерфейс Загрузки данных по группам факультета из базы данных
        /// </summary>
        /// <param name="callback"></param>
        /// <param name="idFaculty"></param>
        ///  <param name="idAcademicYear"></param>
        void GetDataGroup(Action<ObservableCollection<StudentGroup>, Exception> callback, int idFaculty, int idAcademicYear);

        /// <summary>
        /// Интерфейс Загрузки данных по группам факультета 
        /// </summary>
        /// <param name="groups"></param>
        /// <param name="idFaculty"></param>
        /// <returns></returns>
        ObservableCollection<StudentGroup> GetGroup(ObservableCollection<StudentGroup> groups, int idFaculty, int idAcademicYear);

        /// <summary>
        /// Интерфейс Загрузки данных по группам факультета 
        /// </summary>
        /// <param name="groups"></param>
        /// <param name="idFaculty"></param>
        /// <returns></returns>
        ObservableCollection<StudentGroup> GetGroup(int idFaculty, int idAcademicYear);

        /// <summary>
        /// Формирование данных по группам для отчета
        /// </summary>
        /// <param name="idAcademicYear"></param>
        /// <returns></returns>
        ObservableCollection<tlsp_getGroups_Result> GetGroupForReport(int idAcademicYear);


        /// <summary>
        /// Интерфейс Загрузки данных по группам всех факультетов 
        /// </summary>
        /// <param name="groups"></param>
        /// <param name="idAcademicYear"></param>
        /// <returns></returns>
        ObservableCollection<StudentGroup> GetAllGroup(ObservableCollection<StudentGroup> groups, int idAcademicYear);


        /// <summary>
        /// Интерфейс Загрузки данных по группам всех факультетов 
        /// </summary>
        /// <param name="groups"></param>
        /// <param name="idAcademicYear"></param>
        /// <returns></returns>
        ObservableCollection<StudentGroup> GetAllGroup(int idAcademicYear);

        /// <summary>
        /// Интерфейс загрузки списка учебных планов 
        /// </summary>
        /// <param name="idAcademicYear"></param>
        /// <returns></returns>
        ObservableCollection<CurriculumShort> GetCurriculum(int idAcademicYear);

        /// <summary>
        /// Интерфейс редактирования данных по группе
        /// </summary>
        /// <param name="editGroup"></param>
        void EditItemGroup(StudentGroup editGroup);

       /// <summary>
        /// Интерфейс добавления данных по новой группе
       /// </summary>
       /// <param name="newGroup"></param>
        void AddItemDataGroup(StudentGroup newGroup);

        /// <summary>
        /// Интерфейс удаления данных по группе
        /// </summary>
        /// <param name="delGroup"></param>
        void RemoveItemGroup(StudentGroup delGroup);
    }
}
