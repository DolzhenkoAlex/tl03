using System;
using System.Collections.ObjectModel;
using MvvmLight2.Model;

namespace MvvmLight2.ServiceData
{
    public interface IServiceStandartTime
    {
        /// <summary>
        /// Интерфейс получения данных по нормам времени для определенного приказа из базы данных
        /// </summary>
        /// <param name="callback"></param>
        /// <param name="idOrder"></param>
        void GetDataStandartTime(Action<ObservableCollection<StandartTime>, Exception> callback, int idOrder);

        /// <summary>
        /// Интерфейс загрузки данных по нормам времени для определенного приказа из базы данных
        /// </summary>
        /// <param name="callback"></param>
        /// <param name="idOrder"></param>
        /// <returns></returns>
        ObservableCollection<StandartTime> GetStandartTime(ObservableCollection<StandartTime> standartsTime, int idOrder);

        /// <summary>
        /// Интерфейс редактирования данных по нормам времени
        /// </summary>
        /// <param name="editStandartTime"></param>
        void EditItemStandartTime(StandartTime editStandartTime);

        /// <summary>
        /// Интерфейс добавления данных по новым нормам времени
        /// </summary>
        /// <param name="newStandartTime"></param>
        void AddItemStandartTime(StandartTime newStandartTime);

        /// <summary>
        /// Интерфейс удаления данных по нормам времени
        /// </summary>
        /// <param name="delStandartTime"></param>
        void RemoveItemStandartTime(StandartTime delStandartTime);
    }
}
