using System;
using System.Collections.ObjectModel;
using MvvmLight2.Model;

namespace MvvmLight2.ServiceData
{
    /// <summary>
    /// Интерфейс работы с данными по квалификациям
    /// </summary>
    public interface IServiceQualification
    {
        /// <summary>
        /// Интерфейс доступа к данным по квалификациям 
        /// </summary>
        void GetDataQualification(Action<ObservableCollection<DictQualification>, Exception> callback);

        /// <summary>
        /// Загрузка данных по квалификациям
        /// </summary>
        /// <param name="teachers"></param>
        /// <param name="idFaculty"></param>
        /// <returns></returns>
        ObservableCollection<DictQualification> GetQualification(ObservableCollection<DictQualification> qualifications);

        /// <summary>
        /// Добавление данных по новой квалификации
        /// </summary>
        /// <param name="newChair"></param>
        /// <returns></returns>
        void AddItemDataQualification(DictQualification newQualification);

        /// <summary>
        /// Редактирование данных по квалификации
        /// </summary>
        /// <param name="editChair"></param>
        void EditItemQualification(DictQualification editQualification);

        /// <summary>
        /// Удаление данных по квалификации
        /// </summary>
        /// <param name="delChair"></param>
        void RemoveItemQualification(DictQualification delQualification);

        /// <summary>
        /// Интерфейс нахождения квалификации по коду
        /// </summary>
        /// <param name="codeChair"></param>
        /// <returns></returns>
        DictQualification GetItemQualification(string qQualification);
    }
}
