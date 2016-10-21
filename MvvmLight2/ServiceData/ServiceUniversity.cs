using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.Services.Client;
using System.Linq;
using MvvmLight2.Model;
using System.Data.SqlClient;

namespace MvvmLight2.ServiceData
{
    /// <summary>
    /// Сервис данных по университету/филиалам
    /// </summary>
 
    public class ServiceUniversity: IServiceUniversity
    {
        /// <summary>
        /// Загрузка данных по университетам/филиалам из базы данных
        /// </summary>
        /// <param name="callback"></param>
        public void GetDataUniversity(Action<ObservableCollection<University>, Exception> callback)
        {
            ObservableCollection<University> universities = new ObservableCollection<University>();
            Exception error = null;
            //string error = string.Empty;
            using (var context = new DBTeachingLoadEntities())
            {
                try
                {
                    var query = context.Universities.Where(x => x.StatusDel == true);
                    foreach (var u in query)
                        universities.Add(u);
                }
                catch (EntityException ex)
                {

                    error = new Exception(String.Format("Ошибка загрузки данных\n {0}\n {1}", ex.Message, ex.InnerException.Message));
                }
            }
                
            callback(universities, error);
        }

        /// <summary>
        /// Добавдение записи по университету/факультету в базу данных
        /// </summary>
        /// <param name="newUniversity"></param>
        public void AddItemDataUniversity(University newUniversity)
        {
            using (var context = new DBTeachingLoadEntities())
            {
                try
                {
                    context.Universities.AddObject(newUniversity);
                }
                catch (DataServiceRequestException ex)
                {
                    throw new ApplicationException("Ошибка добавления данных" + ex.ToString());
                }

                context.SaveChanges();
            }
        }

        /// <summary>
        /// Редактирование записи по университету/факультету в базе данных
        /// </summary>
        /// <param name="editUniversity"></param>
        public void EditItemUniversity(University editUniversity)
        {
            using (var context = new DBTeachingLoadEntities())
            {
                try
                {
                    var editUniver = (from u in context.Universities
                                      where u.Id == editUniversity.Id
                                      select u).FirstOrDefault();
                    if (editUniver != null)
                    {
                        context.Universities.ApplyCurrentValues(editUniversity);
                        context.SaveChanges();
                    }
                }
                catch (OptimisticConcurrencyException ex)
                {
                    throw new InvalidOperationException(string.Format(
                       "Университет с Id '{0}' не может быть отредактирован.\n", editUniversity.Id), ex);
                }
            }
        }

        /// <summary>
        /// Удаление записи по университету/факультету из базы данных
        /// </summary>
        /// <param name="delUniversity"></param>
        public void RemoveItemUniversity(University delUniversity)
        {
            
            using (var context = new DBTeachingLoadEntities())
            {
                try
                {
                    var deleteUniversity = (from u in context.Universities
                                            where u.Id == delUniversity.Id
                                            select u).FirstOrDefault();
                    if (deleteUniversity != null)
                    {
                        // Помечаем запись, как удаленную
                        deleteUniversity.StatusDel = false;
                        context.Universities.ApplyCurrentValues(deleteUniversity);
                        
                        // В случае удаления из базы данных
                        //context.Universities.DeleteObject(deleteUniversity);
                        
                        context.SaveChanges();
                    }
                    
                    
                    
                }
                catch (OptimisticConcurrencyException ex)
                {
                    throw new InvalidOperationException(string.Format(
                        "Университет с Id '{0}' не может быть удален.\n", delUniversity.Id), ex);
                }
             }
        }
        
    }
}
