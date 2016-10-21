using System;
using System.Collections.ObjectModel;
using MvvmLight2.Model;

namespace MvvmLight2.ServiceData
{
    /// <summary>
    /// Интерфейс работы с данными по университету/филиалу
    /// </summary>
    public interface IServiceUniversity
    {
        /// <summary>
        /// Получение данных по университетам
        /// </summary>
        /// <param name="callback"></param>
        void GetDataUniversity(Action<ObservableCollection<University>, Exception> callback);

        /// <summary>
        /// Добавление данных по университету
        /// </summary>
        /// <param name="newUniversity"></param>
        /// <param name="callback"></param>
        void AddItemDataUniversity(University newUniversity);

        /// <summary>
        /// Редактирование данных по университету
        /// </summary>
        /// <param name="editUniversity"></param>
        void EditItemUniversity(University editUniversity);
        
        /// <summary>
        /// Удаление  данных по университету
        /// </summary>
        /// <param name="delUniversity"></param>
        void RemoveItemUniversity(University delUniversity);
    }
}
