using System;
using System.Collections.ObjectModel;
using MvvmLight2.Model;

namespace MvvmLight2.ServiceData
{
    /// <summary>
    /// Интерфейс работы с данными по преподавателям
    /// </summary>
    public interface IServiceTeacher
    {
        /// <summary>
        /// Интерфейс загрузки данных по преподавателям по определенной  кафедре 
        /// </summary>
        /// <param name="callback"></param>
        /// <param name="idFaculty"></param>
        void GetDataTeacher(Action<ObservableCollection<Teacher>, Exception> callback, int idChair);

        /// <summary>
        /// Интерфейс доступа к данным по преподавателям по определенной  кафедре 
        /// </summary>
        /// <param name="teachers"></param>
        /// <param name="idFaculty"></param>
        /// <returns></returns>
        ObservableCollection<Teacher> GetTeachers(ObservableCollection<Teacher> teachers, int idChair);

        /// <summary>
        /// Интерфейс добавления  данных по новому преподавателю кафедры 
        /// </summary>
        /// <param name="newTeacher"></param>
        void AddItemDataTeacher(Teacher newTeacher);

        /// <summary>
        ///  Интерфейс удаления данных по преподавателю
        /// </summary>
        /// <param name="delChair"></param>
        void RemoveItemTeacher(Teacher delTeacher);

        /// <summary>
        /// Интерфейс редактирования данных по преподавателю
        /// </summary>
        /// <param name="editTeacher"></param>
        void EditItemTeacher(Teacher editTeacher);
    }
}
