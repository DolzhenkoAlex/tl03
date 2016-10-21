using System;
using System.Collections.ObjectModel;
using MvvmLight2.Model;

namespace MvvmLight2.ServiceData
{
    public interface IServiceOrderStandartTime
    {
        /// <summary>
        /// Интерфейс загрузки данных по приказам норм времени
        /// </summary>
        /// <param name="callback"></param>
        void GetDataOrders(Action<ObservableCollection<OrderStandardTime>, Exception> callback, int idUniversity);

        /// <summary>
        /// Интерфейс получения данных по приказам норм времени
        /// </summary>
        /// <param name="orders"></param>
        /// <param name="idUniversity"></param>
        /// <returns></returns>
        ObservableCollection<OrderStandardTime> GetOrder(ObservableCollection<OrderStandardTime> orders, int idUniversity);

        /// <summary>
        /// Интерфейс Добавления данных по новому приказу по нормам времени
        /// </summary>
        /// </summary>
        /// <param name="newOrder"></param>
        void AddItemDataOrder(OrderStandardTime newOrder);
        
        /// <summary>
        /// Интерфейс редактирования данных по приказу норм времени
        /// </summary>
        /// <param name="order"></param>
        void EditItemOrder(OrderStandardTime editOrder);

        /// <summary>
        /// Интерфейс удаления данных по приказу норм времени
        /// </summary>
        /// <param name="delOrder"></param>
        void RemoveItemOrder(OrderStandardTime delOrder);

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
