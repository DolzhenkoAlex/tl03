using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.Objects;
using System.Data.Services.Client;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvvmLight2.Model;

namespace MvvmLight2.ServiceData
{
    /// <summary>
    /// Сервис данных по факультетам
    /// </summary>
    public class ServiceFaculty : IServiceFaculty
    {
        /// <summary>
        /// Загрузка данных по факультетам из базы данных
        /// </summary>
        /// <param name="callback"></param>
        public void GetDataFaculty(Action<ObservableCollection<Faculty>, Exception> callback, int idUniversity)
        {
            ObservableCollection<Faculty> faculties = new ObservableCollection<Faculty>();
            using (var context = new DBTeachingLoadEntities())
            {
                try
                {
                    var QueryFaculry = from fac in context.Faculties
                                       where (fac.IdUniversity == idUniversity) && (fac.StatusDel == true)
                                       orderby fac.Code
                                       select fac;

                    foreach (Faculty f in QueryFaculry)
                        faculties.Add(f);
                }
                catch (EntityException ex)
                {
                    //throw new ApplicationException("Ошибка загрузки данных" + ex.ToString());
                }
                

            }
            callback(faculties, null);
        }

        /// <summary>
        /// Загрузка данных по факультетам определенного университета 
        /// </summary>
        /// <param name="teachers"></param>
        /// <param name="idFaculty"></param>
        /// <returns></returns>
        public ObservableCollection<Faculty> GetFaculty(ObservableCollection<Faculty> faculties, int idUniversity)
        {
            using (var context = new DBTeachingLoadEntities())
            {
                var QueryFaculry = from fac in context.Faculties
                                   where (fac.IdUniversity == idUniversity) && (fac.StatusDel == true)
                                   orderby fac.Code
                                   select fac;

                foreach (Faculty f in QueryFaculry)
                    faculties.Add(f);
            }

            return faculties;
        }

        /// <summary>
        /// Добавление данных по новому факультету
        /// </summary>
        /// <param name="newFaculty"></param>
        public void AddItemDataFaculty(Faculty newFaculty)
        {
            using (var context = new DBTeachingLoadEntities())
            {
                try
                {
                    context.Faculties.AddObject(newFaculty);
                    context.SaveChanges();
                }
                catch (DataServiceRequestException ex)
                {
                    throw new ApplicationException("Ошибка добавления данных" + ex.ToString());
                }
            }
        }


        /// <summary>
        /// Редактирование записи по факультету в базе данных
        /// </summary>
        /// <param name="editUniversity"></param>
        public void EditItemFaculty(Faculty editFaculty)
        {
            using (var context = new DBTeachingLoadEntities())
            {
                try
                {
                    var editFac = (from fac in context.Faculties
                                   where fac.Id == editFaculty.Id
                                   select fac).FirstOrDefault();
                    if (editFac != null)
                    {
                        context.Faculties.ApplyCurrentValues(editFaculty);
                        context.SaveChanges();
                    }
                }
                catch (OptimisticConcurrencyException ex)
                {
                    throw new InvalidOperationException(string.Format(
                       "Факультет с Id '{0}' не может быть отредактирован.\n", editFaculty.Id), ex);
                }
            }
        }


        /// <summary>
        /// Удаление записи по факультету из базы данных
        /// </summary>
        /// <param name="delUniversity"></param>
        public void RemoveItemFaculty(Faculty delFaculty)
        {
            using (var context = new DBTeachingLoadEntities())
            {
                try
                {
                    var deleteFaculty = (from fac in context.Faculties
                                         where fac.Id == delFaculty.Id
                                         select fac).FirstOrDefault();

                    if (deleteFaculty != null)
                    {
                        // Помечаем запись, как удаленную
                        deleteFaculty.StatusDel = false;
                        context.Faculties.ApplyCurrentValues(deleteFaculty);

                        // В случае удаления из базы данных
                        //context.Faculties.DeleteObject(deleteFaculty);
                        context.SaveChanges();
                    }
                }
                catch (OptimisticConcurrencyException ex)
                {
                    throw new InvalidOperationException(string.Format(
                       "Факультет с Id '{0}' не может быть удален.\n", delFaculty.Id), ex);
                }
            }
        }

        /// <summary>
        /// Нахождение факультета по коду
        /// </summary>
        /// <param name="idFaculty"></param>
        /// <returns></returns>
        public Faculty GetItemFaculty(int idFaculty)
        {
            using (var context = new DBTeachingLoadEntities())
            {
                try
                {
                    var faculty = (from fac in context.Faculties
                                   where fac.Id == idFaculty
                                   select fac).FirstOrDefault();
                    return faculty;
                }
                catch (OptimisticConcurrencyException ex)
                {
                    throw new InvalidOperationException(string.Format(
                       "Факультет с Id '{0}' не существует.\n", idFaculty), ex);
                }
            }
        }
    }
}
