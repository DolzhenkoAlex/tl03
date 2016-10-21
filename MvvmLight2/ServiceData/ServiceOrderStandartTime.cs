using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.Services.Client;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvvmLight2.Model;

namespace MvvmLight2.ServiceData
{
    /// <summary>
    /// Сервис данных для сущености OrderStandardTime
    /// </summary>
    public class ServiceOrderStandartTime : IServiceOrderStandartTime
    {
        /// <summary>
        /// Загрузка данных по приказам норм времени
        /// </summary>
        /// <param name="callback"></param>
        /// <param name="idUniversity"></param>
        public void GetDataOrders(Action<ObservableCollection<OrderStandardTime>, Exception> callback, int idUniversity)
        {
            ObservableCollection<OrderStandardTime> orders = new ObservableCollection<OrderStandardTime>();

            using (var context = new DBTeachingLoadEntities())
            {
                try
                {

                    var QueryOrder = from ord in context.OrderStandardTimes
                                     .Include("DictAcademicYear")
                                     where (ord.IdUniversity == idUniversity)
                                     && (ord.StatusDel == true)
                                     orderby ord.NumberOrder
                                     select ord;
                    foreach (OrderStandardTime ord in QueryOrder)
                        orders.Add(ord);
                }
                catch (EntityException ex)
                {
                    //throw new ApplicationException("Ошибка загрузки данных" + ex.ToString());
                }
            }
            callback(orders, null);
        }

        /// <summary>
        /// Получение данных по приказам норм времени
        /// </summary>
        /// <param name="orders"></param>
        /// <param name="idUniversity"></param>
        /// <returns></returns>
        public ObservableCollection<OrderStandardTime> GetOrder(ObservableCollection<OrderStandardTime> orders, int idUniversity)
        {
            using (var context = new DBTeachingLoadEntities())
            {
                var QueryOrder = from ord in context.OrderStandardTimes
                                 .Include("DictAcademicYear")
                                 where (ord.IdUniversity == idUniversity) 
                                 //&& (ord.StatusDel == true)
                                 orderby ord.NumberOrder
                                 select ord;
                foreach (OrderStandardTime ord in QueryOrder)
                    orders.Add(ord);
            }
            return orders;
        }

        /// <summary>
        /// Редактирование данных по приказу норм времени
        /// </summary>
        /// <param name="editOrder"></param>
        public void EditItemOrder(OrderStandardTime editOrder)
        {
            using (var context = new DBTeachingLoadEntities())
            {
                try
                {
                    var order = (from or in context.OrderStandardTimes
                                 where (or.Id == editOrder.Id)
                                 select or).FirstOrDefault();

                    if (order != null)
                    {
                        context.OrderStandardTimes.ApplyCurrentValues(editOrder);
                        context.SaveChanges();
                    }
                }
                catch (OptimisticConcurrencyException ex)
                {
                    throw new InvalidOperationException(string.Format(
                       "Приказ с Id '{0}' не может быть отредактирован.\n", editOrder.Id), ex);
                }
            }
        }

        /// <summary>
        /// Добавление данных по новому приказу норм времени
        /// </summary>
        /// <param name="newOrder"></param>
        public void AddItemDataOrder(OrderStandardTime newOrder)
        {
            using (var context = new DBTeachingLoadEntities())
            {
                try
                {
                    context.OrderStandardTimes.AddObject(newOrder);
                    context.SaveChanges();
                }
                catch (DataServiceRequestException ex)
                {
                    throw new ApplicationException("Ошибка добавления данных" + ex.ToString());
                }
            }
        }

        /// <summary>
        /// Удаление данных по приказу норм времени
        /// </summary>
        /// <param name="delOrder"></param>
        public void RemoveItemOrder(OrderStandardTime delOrder)
        {
            using (var context = new DBTeachingLoadEntities())
            {
                try
                {
                    var deleteOrder = (from or in context.OrderStandardTimes
                                       where or.Id == delOrder.Id
                                       select or).FirstOrDefault();

                    if (deleteOrder != null)
                    {
                        context.OrderStandardTimes.DeleteObject(deleteOrder);
                        context.SaveChanges();
                    }
                }
                catch (OptimisticConcurrencyException ex)
                {
                    throw new InvalidOperationException(string.Format(
                       "Приказ с Id '{0}' не может быть удален.\n", delOrder.Id), ex);
                }
            }
        }

        /// <summary>
        /// Загрузки данных по нормам времени для определенного приказа из базы данных
        /// </summary>
        /// <param name="callback"></param>
        /// <param name="idOrder"></param>
        /// <returns></returns>
        public ObservableCollection<StandartTime> GetStandartTime(ObservableCollection<StandartTime> standartTimes, int idOrder)
        {
            using (var context = new DBTeachingLoadEntities())
            {
                var QueryStandart = from st in context.StandartTimes
                                    .Include("DictTypeTraining")
                                    .Include("DictUnit")
                                    where st.IdOrderStandartTime == idOrder
                                    orderby st.DictTypeTraining.TypeWork
                                    select st;
                foreach (StandartTime st in QueryStandart)
                    standartTimes.Add(st);

                return standartTimes;
            }
        }

        /// <summary>
        ///  Редактирование данных по нормам времени
        /// </summary>
        /// <param name="editStandartTime"></param>
        public void EditItemStandartTime(StandartTime editStandartTime)
        {
            using (var context = new DBTeachingLoadEntities())
            {
                try
                {
                    var standartTimeEdit = (from st in context.StandartTimes
                                            where st.Id == editStandartTime.Id
                                            select st).FirstOrDefault();

                    if (standartTimeEdit != null)
                    {
                        context.StandartTimes.ApplyCurrentValues(editStandartTime);
                        context.SaveChanges();
                    }
                }
                catch (OptimisticConcurrencyException ex)
                {
                    throw new InvalidOperationException(string.Format(
                       "Норма времени с Id '{0}' не может быть отредактирован.\n", editStandartTime.Id), ex);
                }
            }
        }

        /// <summary>
        /// Добавление данных по новым нормам времени
        /// </summary>
        /// <param name="newStandartTime"></param>
        public void AddItemStandartTime(StandartTime newStandartTime)
        {
            using (var context = new DBTeachingLoadEntities())
            {
                try
                {
                    context.StandartTimes.AddObject(newStandartTime);
                    context.SaveChanges();
                }
                catch (DataServiceRequestException ex)
                {
                    throw new ApplicationException("Ошибка добавления данных" + ex.ToString());
                }

                
            }
        }

        /// <summary>
        /// Удаления данных по нормам времени
        /// </summary>
        /// <param name="delStandartTime"></param>
        public void RemoveItemStandartTime(StandartTime delStandartTime)
        {
            using (var context = new DBTeachingLoadEntities())
            {
                try
                {
                    var deleteStandart = (from st in context.StandartTimes
                                          where st.Id == delStandartTime.Id
                                          select st).First();

                    context.StandartTimes.DeleteObject(deleteStandart);
                    context.SaveChanges();
                }
                catch (OptimisticConcurrencyException ex)
                {
                    throw new InvalidOperationException(string.Format(
                       "Норма времени с Id '{0}' не может быть удален.\n", delStandartTime.Id), ex);
                }
            }
        }
    }
}
