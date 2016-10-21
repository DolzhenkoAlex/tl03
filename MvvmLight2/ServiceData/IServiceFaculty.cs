using System;
using System.Collections.ObjectModel;
using MvvmLight2.Model;

namespace MvvmLight2.ServiceData
{
    /// <summary>
    /// Интерфейс работы с данными по факультетам
    /// </summary>
    public interface IServiceFaculty
    {
        /// <summary>
        /// Получение данных по факультетам
        /// </summary>
        /// <param name="callback"></param>
        void GetDataFaculty(Action<ObservableCollection<Faculty>, Exception> callback, int idUniversity);

        /// <summary>
        /// Загрузка данных по факультетам определенного университета
        /// </summary>
        /// <param name="teachers"></param>
        /// <param name="idFaculty"></param>
        /// <returns></returns>
        ObservableCollection<Faculty> GetFaculty(ObservableCollection<Faculty> faculties, int idUniversity);

        /// <summary>
        /// Интерфейс нахождения факультета по коду
        /// </summary>
        /// <param name="idFaculty"></param>
        /// <returns></returns>
        Faculty GetItemFaculty(int idFaculty);

        /// <summary>
        /// Добавление данных по новому факультету
        /// </summary>
        /// <param name="newFaculty"></param>
        /// <param name="callback"></param>
        void AddItemDataFaculty(Faculty newFaculty);

        /// <summary>
        /// Редактирование данных по факультету
        /// </summary>
        /// <param name="editFaculty"></param>
        void EditItemFaculty(Faculty editFaculty);

        /// <summary>
        /// Удаление  данных по факультету
        /// </summary>
        /// <param name="delFaculty"></param>
        void RemoveItemFaculty(Faculty delFaculty);
    }
}
