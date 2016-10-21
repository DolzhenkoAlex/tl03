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
    /// Сервис данных по кафедрам
    /// </summary>
    public class ServiceChair: IServiceChair
    {
        /// <summary>
        /// Загрузка данных по кафедрам из базы данных
        /// </summary>
        /// <param name="callback"></param>
        /// <param name="idFaculty"></param>
        public void GetDataChair(Action<ObservableCollection<Chair>, Exception> callback, int idFaculty)
        {
            ObservableCollection<Chair> chairs = new ObservableCollection<Chair>();

            using (var context = new DBTeachingLoadEntities())
            {
                try
                {
                    var QueryChair = from c in context.Chairs
                                     where (c.IdFaculty == idFaculty) && (c.StatusDel == true)
                                     orderby c.CodeChair
                                     select c;
                    foreach (Chair c in QueryChair)
                        chairs.Add(c);
                }
                catch (EntityException ex)
                {
                    //throw new ApplicationException("Ошибка загрузки данных" + ex.ToString());
                }
            }
            callback(chairs, null);
        }
        
        /// <summary>
        /// Получение данных по кафедрам из базы данных по определенному факультету
        /// </summary>
        /// <param name="teachers"></param>
        /// <param name="idFaculty"></param>
        /// <returns></returns>
        public ObservableCollection<Chair> GetChair(ObservableCollection<Chair> chairs, int idFaculty)
        {
            using (var context = new DBTeachingLoadEntities())
            {
                var QueryChair = from c in context.Chairs
                                 where (c.IdFaculty == idFaculty) && (c.StatusDel == true)
                                 orderby c.CodeChair
                                 select c;

                foreach (Chair c in QueryChair)
                    chairs.Add(c);
            }

            return chairs;
        }

        /// <summary>
        /// Добавление данных по новой кафедре
        /// </summary>
        /// <param name="newFaculty"></param>
        public void AddItemDataChair(Chair newChair)
        {
            using (var context = new DBTeachingLoadEntities())
            {
                try
                {
                    context.Chairs.AddObject(newChair);
                    context.SaveChanges();
                }
                catch (DataServiceRequestException ex)
                {
                    throw new ApplicationException("Ошибка добавления данных" + ex.ToString());
                }
            }
        }

        /// <summary>
        /// Редактирование данных по кафедре
        /// </summary>
        /// <param name="delChair"></param>
        public void EditItemChiar(Chair editChair)
        {
            using (var context = new DBTeachingLoadEntities())
            {
                try
                {
                    var chairEdit = (from chair in context.Chairs
                                   where chair.Id == editChair.Id
                                   select chair).FirstOrDefault();
                    if (chairEdit != null)
                    {
                        context.Chairs.ApplyCurrentValues(editChair);
                        context.SaveChanges();
                    }
                }
                catch (OptimisticConcurrencyException ex)
                {
                    throw new InvalidOperationException(string.Format(
                       "Кафедра с Id '{0}' не может быть отредактирован.\n", editChair.Id), ex);
                }
            }
        }

        /// <summary>
        /// Удаление данных по кафедре
        /// </summary>
        /// <param name="delChair"></param>
        public void RemoveItemChair(Chair delChair)
        {
            using (var context = new DBTeachingLoadEntities())
            {
                try
                {
                    var deleteChair = (from chair in context.Chairs
                                         where chair.Id == delChair.Id
                                         select chair).FirstOrDefault();

                    if (deleteChair != null)
                    {
                        // Помечаем запись, как удаленную
                        deleteChair.StatusDel = false;
                        context.Chairs.ApplyCurrentValues(deleteChair);

                        // В случае удаления из базы данных
                        //context.Chairs.DeleteObject(deleteChair);
                        
                        context.SaveChanges();
                    }
                }
                catch (OptimisticConcurrencyException ex)
                {
                    throw new InvalidOperationException(string.Format(
                       "Кафедра с Id '{0}' не может быть удален.\n", delChair.Id), ex);
                }
            }
        }

        // <summary>
        /// Нахождения кафдры по коду
        /// </summary>
        /// <param name="codeChair"></param>
        /// <returns></returns>
        public Chair GetItemChair(int codeChair)
        {
            using (var context = new DBTeachingLoadEntities())
            {
                var chair = (from ch in context.Chairs
                                 where (ch.CodeChair == codeChair)
                                 select ch).FirstOrDefault();
                return chair;
            }
        }
    }
}
