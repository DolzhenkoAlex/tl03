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
    /// Сервис данных по должностям
    /// </summary>
    public class ServicePost : IServicePost
    {
        /// <summary>
        /// Загрузка данных по должностям из базы данных
        /// </summary>
        /// <param name="callback"></param>
        /// <param name="idFaculty"></param>
        public void GetDataPost(Action<ObservableCollection<DictPost>, Exception> callback)
        {
            ObservableCollection<DictPost> posts = new ObservableCollection<DictPost>();

            using (var context = new DBTeachingLoadEntities())
            {
                try
                {
                    var QueryPost = from c in context.DictPosts
                                    where c.StatusDel == true
                                    orderby c.Post
                                    select c;
                    foreach (DictPost c in QueryPost)
                        posts.Add(c);
                }
                catch (EntityException ex)
                {
                    //throw new ApplicationException("Ошибка загрузки данных" + ex.ToString());
                }
            }
            callback(posts, null);
        }

        /// <summary>
        /// Получение данных по должностям из базы данных
        /// </summary>
        /// <param name="teachers"></param>
        /// <param name="idFaculty"></param>
        /// <returns></returns>
        public ObservableCollection<DictPost> GetPost(ObservableCollection<DictPost> posts)
        {
            using (var context = new DBTeachingLoadEntities())
            {
                var QueryPost = from c in context.DictPosts
                                where c.StatusDel == true
                                orderby c.Post
                                select c;

                foreach (DictPost c in QueryPost)
                    posts.Add(c);
            }

            return posts;
        }

        /// <summary>
        /// Добавление данных по новой должности
        /// </summary>
        /// <param name="newFaculty"></param>
        public void AddItemDataPost(DictPost newPost)
        {
            using (var context = new DBTeachingLoadEntities())
            {
                try
                {
                    context.DictPosts.AddObject(newPost);
                    context.SaveChanges();
                }
                catch (DataServiceRequestException ex)
                {
                    throw new ApplicationException("Ошибка добавления данных" + ex.ToString());
                }
            }
        }

        /// <summary>
        /// Редактирование данных по должности
        /// </summary>
        /// <param name="delChair"></param>
        public void EditItemPost(DictPost editPost)
        {
            using (var context = new DBTeachingLoadEntities())
            {
                try
                {
                    var AcademicYearEdit = (from post in context.DictPosts
                                            where post.Id == editPost.Id
                                            select post).FirstOrDefault();
                    if (AcademicYearEdit != null)
                    {
                        context.DictPosts.ApplyCurrentValues(editPost);
                        context.SaveChanges();
                    }
                }
                catch (OptimisticConcurrencyException ex)
                {
                    throw new InvalidOperationException(string.Format(
                       "Должность преподавателя с Id '{0}' не может быть отредактирована.\n", editPost.Id), ex);
                }
            }
        }

        /// <summary>
        /// Удаление данных по должности
        /// </summary>
        /// <param name="delChair"></param>
        public void RemoveItemPost(DictPost delPost)
        {
            using (var context = new DBTeachingLoadEntities())
            {
                try
                {
                    var deletePost = (from post in context.DictPosts
                                      where post.Id == delPost.Id
                                      select post).FirstOrDefault();

                    if (deletePost != null)
                    {

                        deletePost.StatusDel = false;
                        context.DictPosts.ApplyCurrentValues(deletePost);
                      //  context.DictPosts.DeleteObject(deletePost);

                        context.SaveChanges();
                    }
                }
                catch (OptimisticConcurrencyException ex)
                {
                    throw new InvalidOperationException(string.Format(
                       "Должность преподавателя с Id '{0}' не может быть удалена.\n", delPost.Id), ex);
                }
            }
        }

        // <summary>
        /// Нахождение должности по коду
        /// </summary>
        /// <param name="codeChair"></param>
        /// <returns></returns>
        public DictPost GetItemPost(string pPost)
        {
            using (var context = new DBTeachingLoadEntities())
            {
                var post = (from ch in context.DictPosts
                            where (ch.Post == pPost)
                            select ch).FirstOrDefault();
                return post;
            }
        }
    }
}
