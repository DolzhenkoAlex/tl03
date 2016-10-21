using System;
using System.Collections.ObjectModel;
using MvvmLight2.Model;

namespace MvvmLight2.ServiceData
{
    /// <summary>
    /// Интерфейс работы с данными по типам занятости
    /// </summary>
    public interface IServiceTypeEmployment
    {
        /// <summary>
        /// Интерфейс доступа к данным по типам занятости
        /// </summary>
        void GetDataTypeEmployment(Action<ObservableCollection<DictTypeEmployment>, Exception> callback);

        /// <summary>
        /// Загрузка данных по типам занятости
        /// </summary>
        /// <param name="teachers"></param>
        /// <param name="idFaculty"></param>
        /// <returns></returns>
        ObservableCollection<DictTypeEmployment> GetTypeEmployment(ObservableCollection<DictTypeEmployment> typeemployment);

        /// <summary>
        /// Добавление данных по новому типу занятости
        /// </summary>
        /// <param name="newChair"></param>
        /// <returns></returns>
        void AddItemDataTypeEmployment(DictTypeEmployment newTypeEmployment);

        /// <summary>
        /// Редактирование данных по типам занятости
        /// </summary>
        /// <param name="editChair"></param>
        void EditItemTypeEmployment(DictTypeEmployment editTypeEmployment);

        /// <summary>
        /// Удаление данных по типам занятости
        /// </summary>
        /// <param name="delChair"></param>
        void RemoveItemTypeEmployment(DictTypeEmployment delTypeEmployment);

        /// <summary>
        /// Интерфейс нахождения типа занятости по коду
        /// </summary>
        /// <param name="codeChair"></param>
        /// <returns></returns>
        DictTypeEmployment GetItemTypeEmployment(string tEmp);
    }
}

