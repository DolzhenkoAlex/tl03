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
    /// Сервис данных по квалификациям
    /// </summary>
    public class ServiceQualification : IServiceQualification
    {
        /// <summary>
        /// Загрузка данных по квалификациям из базы данных
        /// </summary>
        /// <param name="callback"></param>
        /// <param name="idFaculty"></param>
        public void GetDataQualification(Action<ObservableCollection<DictQualification>, Exception> callback)
        {
            ObservableCollection<DictQualification> qualifications = new ObservableCollection<DictQualification>();

            using (var context = new DBTeachingLoadEntities())
            {
                try
                {
                    var QueryQualification = from c in context.DictQualifications
                                             where c.StatusDel == true
                                             orderby c.NameQualification
                                             select c;
                    foreach (DictQualification c in QueryQualification)
                        qualifications.Add(c);
                }
                catch (EntityException ex)
                {
                    //throw new ApplicationException("Ошибка загрузки данных" + ex.ToString());
                }
            }
            callback(qualifications, null);
        }

        /// <summary>
        /// Получение данных по квалификациям из базы данных
        /// </summary>
        /// <param name="teachers"></param>
        /// <param name="idFaculty"></param>
        /// <returns></returns>
        public ObservableCollection<DictQualification> GetQualification(ObservableCollection<DictQualification> qualifications)
        {
            using (var context = new DBTeachingLoadEntities())
            {
                var QueryQualification = from c in context.DictQualifications
                                         where c.StatusDel == true
                                         orderby c.NameQualification
                                         select c;

                foreach (DictQualification c in QueryQualification)
                    qualifications.Add(c);
            }

            return qualifications;
        }

        /// <summary>
        /// Добавление данных по новой квалификации
        /// </summary>
        /// <param name="newFaculty"></param>
        public void AddItemDataQualification(DictQualification newQualification)
        {
            using (var context = new DBTeachingLoadEntities())
            {
                try
                {
                    context.DictQualifications.AddObject(newQualification);
                    context.SaveChanges();
                }
                catch (DataServiceRequestException ex)
                {
                    throw new ApplicationException("Ошибка добавления данных" + ex.ToString());
                }
            }
        }

        /// <summary>
        /// Редактирование данных по квалификации
        /// </summary>
        /// <param name="delChair"></param>
        public void EditItemQualification(DictQualification editQualification)
        {
            using (var context = new DBTeachingLoadEntities())
            {
                try
                {
                    var QualificationEdit = (from qualification in context.DictQualifications
                                             where qualification.Id == editQualification.Id
                                             select qualification).FirstOrDefault();
                    if (QualificationEdit != null)
                    {
                        context.DictQualifications.ApplyCurrentValues(editQualification);
                        context.SaveChanges();
                    }
                }
                catch (OptimisticConcurrencyException ex)
                {
                    throw new InvalidOperationException(string.Format(
                       "Квалификация с Id '{0}' не может быть отредактирована.\n", editQualification.Id), ex);
                }
            }
        }

        /// <summary>
        /// Удаление данных по квалификации
        /// </summary>
        /// <param name="delChair"></param>
        public void RemoveItemQualification(DictQualification delQualification)
        {
            using (var context = new DBTeachingLoadEntities())
            {
                try
                {
                    var deleteQualification = (from qualification in context.DictQualifications
                                               where qualification.Id == delQualification.Id
                                               select qualification).FirstOrDefault();

                    if (deleteQualification != null)
                    {

                        deleteQualification.StatusDel = false;
                        context.DictQualifications.ApplyCurrentValues(deleteQualification);
                        //context.DictQualifications.DeleteObject(deleteQualification);

                        context.SaveChanges();
                    }
                }
                catch (OptimisticConcurrencyException ex)
                {
                    throw new InvalidOperationException(string.Format(
                       "Квалификация с Id '{0}' не может быть удалена.\n", delQualification.Id), ex);
                }
            }
        }

        // <summary>
        /// Нахождения квалификации по коду
        /// </summary>
        /// <param name="codeChair"></param>
        /// <returns></returns>
        public DictQualification GetItemQualification(string qQualification)
        {
            using (var context = new DBTeachingLoadEntities())
            {
                var qualification = (from ch in context.DictQualifications
                                     where (ch.NameQualification == qQualification)
                                     select ch).FirstOrDefault();
                return qualification;
            }
        }
    }
}