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
    /// Сервис данных по типам занятости
    /// </summary>
    public class ServiceTypeEmployment : IServiceTypeEmployment
    {
        /// <summary>
        /// Загрузка данных по типам занятости из базы данных
        /// </summary>
        /// <param name="callback"></param>
        /// <param name="idFaculty"></param>
        public void GetDataTypeEmployment(Action<ObservableCollection<DictTypeEmployment>, Exception> callback)
        {
            ObservableCollection<DictTypeEmployment> typeemployment = new ObservableCollection<DictTypeEmployment>();

            using (var context = new DBTeachingLoadEntities())
            {
                try
                {
                    var QueryTypeEmployment = from c in context.DictTypeEmployments
                                              where c.StatusDel == true
                                              orderby c.TypeOfEmployment
                                              select c;
                    foreach (DictTypeEmployment c in QueryTypeEmployment)
                        typeemployment.Add(c);
                }
                catch (EntityException ex)
                {
                    //throw new ApplicationException("Ошибка загрузки данных" + ex.ToString());
                }
            }
            callback(typeemployment, null);
        }

        /// <summary>
        /// Получение данных по типам занятости из базы данных
        /// </summary>
        /// <param name="teachers"></param>
        /// <param name="idFaculty"></param>
        /// <returns></returns>
        public ObservableCollection<DictTypeEmployment> GetTypeEmployment(ObservableCollection<DictTypeEmployment> typeemployment)
        {
            using (var context = new DBTeachingLoadEntities())
            {
                var QueryTypeEmployment = from c in context.DictTypeEmployments
                                         where c.StatusDel == true
                                          orderby c.TypeOfEmployment
                                         select c;

                foreach (DictTypeEmployment c in QueryTypeEmployment)
                    typeemployment.Add(c);
            }

            return typeemployment;
        }

        /// <summary>
        /// Добавление данных по новому типу занятости
        /// </summary>
        /// <param name="newFaculty"></param>
        public void AddItemDataTypeEmployment(DictTypeEmployment newTypeEmployment)
        {
            using (var context = new DBTeachingLoadEntities())
            {
                try
                {
                    context.DictTypeEmployments.AddObject(newTypeEmployment);
                    context.SaveChanges();
                }
                catch (DataServiceRequestException ex)
                {
                    throw new ApplicationException("Ошибка добавления данных" + ex.ToString());
                }
            }
        }

        /// <summary>
        /// Редактирование данных по типам занятости
        /// </summary>
        /// <param name="delChair"></param>
        public void EditItemTypeEmployment(DictTypeEmployment editTypeEmployment)
        {
            using (var context = new DBTeachingLoadEntities())
            {
                try
                {
                    var TypeEmploymentEdit = (from typeemployment in context.DictTypeEmployments
                                              where typeemployment.Id == editTypeEmployment.Id
                                              select typeemployment).FirstOrDefault();
                    if (TypeEmploymentEdit != null)
                    {
                        context.DictTypeEmployments.ApplyCurrentValues(editTypeEmployment);
                        context.SaveChanges();
                    }
                }
                catch (OptimisticConcurrencyException ex)
                {
                    throw new InvalidOperationException(string.Format(
                       "Тип занятости с Id '{0}' не может быть отредактирован.\n", editTypeEmployment.Id), ex);
                }
            }
        }

        /// <summary>
        /// Удаление данных по типам занятости
        /// </summary>
        /// <param name="delChair"></param>
        public void RemoveItemTypeEmployment(DictTypeEmployment delTypeEmployment)
        {
            using (var context = new DBTeachingLoadEntities())
            {
                try
                {
                    var deleteTypeEmployment = (from typeemployment in context.DictTypeEmployments
                                               where typeemployment.Id == delTypeEmployment.Id
                                               select typeemployment).FirstOrDefault();

                    if (deleteTypeEmployment != null)
                    {

                        deleteTypeEmployment.StatusDel = false;
                        context.DictTypeEmployments.ApplyCurrentValues(deleteTypeEmployment);
                        // context.DictEducationForms.DeleteObject(deleteEducationForm);

                        context.SaveChanges();
                    }
                }
                catch (OptimisticConcurrencyException ex)
                {
                    throw new InvalidOperationException(string.Format(
                       "Тип занятости с Id '{0}' не может быть удален.\n", delTypeEmployment.Id), ex);
                }
            }
        }

        // <summary>
        /// Нахождение типа занятости по коду
        /// </summary>
        /// <param name="codeChair"></param>
        /// <returns></returns>
        public DictTypeEmployment GetItemTypeEmployment(string tEmp)
        {
            using (var context = new DBTeachingLoadEntities())
            {
                var typeemployment = (from ch in context.DictTypeEmployments
                                     where (ch.TypeOfEmployment == tEmp)
                                     select ch).FirstOrDefault();
                return typeemployment;
            }
        }
    }
}
