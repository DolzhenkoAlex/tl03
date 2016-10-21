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
    /// Сервис данных по типам обучения
    /// </summary>
    public class ServiceTypeTraining : IServiceTypeTraining
    {
        /// <summary>
        /// Загрузка данных по типам обучения из базы данных
        /// </summary>
        /// <param name="callback"></param>
        /// <param name="idFaculty"></param>
        public void GetDataTypeTraining(Action<ObservableCollection<DictTypeTraining>, Exception> callback)
        {
            ObservableCollection<DictTypeTraining> typetraining = new ObservableCollection<DictTypeTraining>();

            using (var context = new DBTeachingLoadEntities())
            {
                try
                {
                    var QueryTypeTraining = from c in context.DictTypeTrainings
                                            where c.StatusDel == true
                                            orderby c.TypeWork
                                            select c;
                    foreach (DictTypeTraining c in QueryTypeTraining)
                        typetraining.Add(c);
                }
                catch (EntityException ex)
                {
                    //throw new ApplicationException("Ошибка загрузки данных" + ex.ToString());
                }
            }
            callback(typetraining, null);
        }

        /// <summary>
        /// Получение данных по типам обучения из базы данных
        /// </summary>
        /// <param name="teachers"></param>
        /// <param name="idFaculty"></param>
        /// <returns></returns>
        public ObservableCollection<DictTypeTraining> GetTypeTraining(ObservableCollection<DictTypeTraining> typetraining)
        {
            using (var context = new DBTeachingLoadEntities())
            {
                var QueryTypeTraining = from c in context.DictTypeTrainings
                                          where c.StatusDel == true
                                          orderby c.TypeWork
                                          select c;

                foreach (DictTypeTraining c in QueryTypeTraining)
                    typetraining.Add(c);
            }

            return typetraining;
        }

        /// <summary>
        /// Добавление данных по новому типу обучения
        /// </summary>
        /// <param name="newFaculty"></param>
        public void AddItemDataTypeTraining(DictTypeTraining newTypeTraining)
        {
            using (var context = new DBTeachingLoadEntities())
            {
                try
                {
                    context.DictTypeTrainings.AddObject(newTypeTraining);
                    context.SaveChanges();
                }
                catch (DataServiceRequestException ex)
                {
                    throw new ApplicationException("Ошибка добавления данных" + ex.ToString());
                }
            }
        }

        /// <summary>
        /// Редактирование данных по типам обучения
        /// </summary>
        /// <param name="delChair"></param>
        public void EditItemTypeTraining(DictTypeTraining editTypeTraining)
        {
            using (var context = new DBTeachingLoadEntities())
            {
                try
                {
                    var TypeTrainingEdit = (from typetraining in context.DictTypeTrainings
                                            where typetraining.Id == editTypeTraining.Id
                                            select typetraining).FirstOrDefault();
                    if (TypeTrainingEdit != null)
                    {
                        context.DictTypeTrainings.ApplyCurrentValues(editTypeTraining);
                        context.SaveChanges();
                    }
                }
                catch (OptimisticConcurrencyException ex)
                {
                    throw new InvalidOperationException(string.Format(
                       "Тип обучения с Id '{0}' не может быть отредактирован.\n", editTypeTraining.Id), ex);
                }
            }
        }

        /// <summary>
        /// Удаление данных по типам обучения
        /// </summary>
        /// <param name="delChair"></param>
        public void RemoveItemTypeTraining(DictTypeTraining delTypeTraining)
        {
            using (var context = new DBTeachingLoadEntities())
            {
                try
                {
                    var deleteTypeTraining = (from typetraining in context.DictTypeTrainings
                                              where typetraining.Id == delTypeTraining.Id
                                              select typetraining).FirstOrDefault();

                    if (deleteTypeTraining != null)
                    {

                        deleteTypeTraining.StatusDel = false;
                        context.DictTypeTrainings.ApplyCurrentValues(deleteTypeTraining);
                        // context.DictEducationForms.DeleteObject(deleteEducationForm);

                        context.SaveChanges();
                    }
                }
                catch (OptimisticConcurrencyException ex)
                {
                    throw new InvalidOperationException(string.Format(
                       "Тип обучения с Id '{0}' не может быть удален.\n", delTypeTraining.Id), ex);
                }
            }
        }

        // <summary>
        /// Нахождение типа обучения по коду
        /// </summary>
        /// <param name="codeChair"></param>
        /// <returns></returns>
        public DictTypeTraining GetItemTypeTraining(string tTrain)
        {
            using (var context = new DBTeachingLoadEntities())
            {
                var typetraining = (from ch in context.DictTypeTrainings
                                    where (ch.TypeWork == tTrain)
                                      select ch).FirstOrDefault();
                return typetraining;
            }
        }
    }
}
