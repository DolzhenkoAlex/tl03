using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.Services.Client;
using System.Linq;
using MvvmLight2.Model;

namespace MvvmLight2.ServiceData
{
    /// <summary>
    /// Сервис данных по нормам времени
    /// </summary>
    class ServiceStandartTime: IServiceStandartTime
    {
        /// <summary>
        /// Получения данных по нормам времени для определенного приказа из базы данных
        /// </summary>
        /// <param name="callback"></param>
        /// <param name="idOrder"></param>
        public void GetDataStandartTime(Action<ObservableCollection<StandartTime>, Exception> callback, int idOrder)
        {
            ObservableCollection<StandartTime> standartsTime = new ObservableCollection<StandartTime>();

            using (var context = new DBTeachingLoadEntities())
            {
                try
                {
                    var QueryStandart = from st in context.StandartTimes
                                        .Include("DictTypeTraining")
                                        where st.IdOrderStandartTime == idOrder
                                        orderby st.DictTypeTraining.TypeWork
                                        select st;
                    foreach (StandartTime st in QueryStandart)
                        standartsTime.Add(st);
                }
                catch (EntityException ex)
                {
                    //throw new ApplicationException("Ошибка загрузки данных" + ex.ToString());
                }
            }
            callback(standartsTime, null);
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
                                     select st).First();
                    context.StandartTimes.ApplyCurrentValues(editStandartTime);

                    context.SaveChanges();
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
                StandartTime standartTime = new StandartTime();
                try
                {
                    standartTime.IdOrderStandartTime = newStandartTime.IdOrderStandartTime;
                    standartTime.NormTime = newStandartTime.NormTime;
                    standartTime.TypeOfWork = newStandartTime.TypeOfWork;
                    standartTime.UnitMesurement = newStandartTime.UnitMesurement;
                    standartTime.Notes = newStandartTime.Notes;

                    context.StandartTimes.AddObject(standartTime);
                }
                catch (DataServiceRequestException ex)
                {
                    throw new ApplicationException("Ошибка добавления данных" + ex.ToString());
                }

                context.SaveChanges();
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
