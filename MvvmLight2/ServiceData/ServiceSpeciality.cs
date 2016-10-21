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
    /// Сервис данных по направлениям
    /// </summary>
   public class ServiceSpeciality : IServiceSpeciality
    {
         /// <summary>
        /// Загрузка данных по направлениям из базы данных
        /// </summary>
        /// <param name="callback"></param>
        /// <param name="idFaculty"></param>
        public void GetDataSpeciality(Action<ObservableCollection<DictSpeciality>, Exception> callback)
        {
            ObservableCollection<DictSpeciality> specialitys = new ObservableCollection<DictSpeciality>();

            using (var context = new DBTeachingLoadEntities())
            {
                try
                {
                    var QuerySpeciality = from c in context.DictSpecialities
                                          .Include("DictQualification")
                                          where c.StatusDel == true
                                          orderby c.DictQualification.NameQualification, c.CodeSpeciality
                                          select c;
                    foreach (DictSpeciality c in QuerySpeciality)
                        specialitys.Add(c);
                }
                catch (EntityException ex)
                {
                    //throw new ApplicationException("Ошибка загрузки данных" + ex.ToString());
                }
            }
            callback(specialitys, null);
        }
        
        /// <summary>
        /// Получение данных по направлениям из базы данных
        /// </summary>
        /// <param name="teachers"></param>
        /// <param name="idFaculty"></param>
        /// <returns></returns>
        public ObservableCollection<DictSpeciality> GetSpeciality(ObservableCollection<DictSpeciality> specialitys)
        {
            using (var context = new DBTeachingLoadEntities())
            {
                var QuerySpeciality = from c in context.DictSpecialities
                                       .Include("DictQualification")
                                      where c.StatusDel == true
                                      orderby c.DictQualification.NameQualification, c.CodeSpeciality 
                                      select c;

                foreach (DictSpeciality c in QuerySpeciality)
                    specialitys.Add(c);
            }

            return specialitys;
        }

        /// <summary>
        /// Добавление данных по новому направлению
        /// </summary>
        /// <param name="newFaculty"></param>
        public void AddItemDataSpeciality(DictSpeciality newSpeciality)
        {
            using (var context = new DBTeachingLoadEntities())
            {
                try
                {
                    context.DictSpecialities.AddObject(newSpeciality);
                    context.SaveChanges();
                }
                catch (DataServiceRequestException ex)
                {
                    throw new ApplicationException("Ошибка добавления данных" + ex.ToString());
                }
            }
        }

        /// <summary>
        /// Редактирование данных по направлению
        /// </summary>
        /// <param name="delChair"></param>
        public void EditItemSpeciality(DictSpeciality editSpeciality)
        {
            using (var context = new DBTeachingLoadEntities())
            {
                try
                {
                    var SpecialityEdit = (from speciality in context.DictSpecialities
                                          where speciality.Id == editSpeciality.Id
                                          select speciality).FirstOrDefault();
                    if (SpecialityEdit != null)
                    {
                        context.DictSpecialities.ApplyCurrentValues(editSpeciality);
                        context.SaveChanges();
                    }
                }
                catch (OptimisticConcurrencyException ex)
                {
                    throw new InvalidOperationException(string.Format(
                       "Направление с Id '{0}' не может быть отредактировано.\n", editSpeciality.Id), ex);
                }
            }
        }

        /// <summary>
        /// Удаление данных по направлению
        /// </summary>
        /// <param name="delChair"></param>
        public void RemoveItemSpeciality(DictSpeciality delSpeciality)
        {
            using (var context = new DBTeachingLoadEntities())
            {
                try
                {
                    var deleteSpeciality = (from speciality in context.DictSpecialities
                                            where speciality.Id == delSpeciality.Id
                                            select speciality).FirstOrDefault();

                    if (deleteSpeciality != null)
                    {

                        context.DictSpecialities.DeleteObject(deleteSpeciality);
                        
                        context.SaveChanges();
                    }
                }
                catch (OptimisticConcurrencyException ex)
                {
                    throw new InvalidOperationException(string.Format(
                       "Направление с Id '{0}' не может быть удалено.\n", delSpeciality.Id), ex);
                }
            }
        }

        // <summary>
        /// Нахождения направления по коду
        /// </summary>
        /// <param name="codeChair"></param>
        /// <returns></returns>
        public DictSpeciality GetItemSpeciality(string codeSpeciality)
        {
            using (var context = new DBTeachingLoadEntities())
            {
                var speciality = (from ch in context.DictSpecialities
                                  where (ch.CodeSpeciality == codeSpeciality)
                                  select ch).FirstOrDefault();
                return speciality;
            }
        }
    }
    }

