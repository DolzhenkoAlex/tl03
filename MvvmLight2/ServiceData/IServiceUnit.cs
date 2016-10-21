using System;
using System.Collections.ObjectModel;
using MvvmLight2.Model;

namespace MvvmLight2.ServiceData
{
    /// <summary>
    /// Интерфейс работы с данными по единицам измерения
    /// </summary>
    public interface IServiceUnit
    {
        /// <summary>
        /// Интерфейс доступа к данным по единицам измерения
        /// </summary>
        void GetDataUnit(Action<ObservableCollection<DictUnit>, Exception> callback);

        /// <summary>
        /// Загрузка данных по единицам измерения
        /// </summary>
        /// <param name="teachers"></param>
        /// <param name="idFaculty"></param>
        /// <returns></returns>
        ObservableCollection<DictUnit> GetUnit(ObservableCollection<DictUnit> units);

        /// <summary>
        /// Добавление данных по новой единице измерения
        /// </summary>
        /// <param name="newChair"></param>
        /// <returns></returns>
        void AddItemDataUnit(DictUnit newUnit);

        /// <summary>
        /// Редактирование данных по единице измерения
        /// </summary>
        /// <param name="editChair"></param>
        void EditItemUnit(DictUnit editUnit);

        /// <summary>
        /// Удаление данных по единице измерения
        /// </summary>
        /// <param name="delChair"></param>
        void RemoveItemUnit(DictUnit delUnit);

        /// <summary>
        /// Интерфейс нахождения единицы измерения по коду
        /// </summary>
        /// <param name="codeChair"></param>
        /// <returns></returns>
        DictUnit GetItemUnit(string uUnit);
    }
}
