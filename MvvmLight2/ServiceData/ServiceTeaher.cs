using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.Services.Client;
using System.Linq;
using MvvmLight2.Model;

namespace MvvmLight2.ServiceData
{
    /// <summary>
    /// Сервис данных по преподавателям кафедры
    /// </summary>
    public class ServiceTeaher: IServiceTeacher
    {
        /// <summary>
        /// Загрузка данных по преподавателям кафедры 
        /// </summary>
        /// <param name="callback"></param>
        /// <param name="idFaculty"></param>
        public void GetDataTeacher(Action<ObservableCollection<Teacher>, Exception> callback, int idChair)
        {
            ObservableCollection<Teacher> teachers = new ObservableCollection<Teacher>();

            using (var context = new DBTeachingLoadEntities())
            {
                try
                {
                    /// Загрузка связанных сущностей Faculties и Chairs
                    var QueryTeacher = from teach in context.Teachers.
                                       Include("DictPost").
                                       Include("DictTypeEmployment")
                                       where (teach.IdChair == idChair) && (teach.StatusDel == true)
                                       orderby teach.LastName
                                       select teach;

                    /// Формирование коллекции классов сущностей Teacher
                    foreach (Teacher t in QueryTeacher)
                        teachers.Add(t);
                }
                catch (EntityException ex)
                {
                    //throw new ApplicationException("Ошибка загрузки данных" + ex.ToString());
                }
            }
            callback(teachers, null);
        }

        /// <summary>
        /// Получение данных по преподавателям кафедры 
        /// </summary>
        /// <param name="teachers"></param>
        /// <param name="idFaculty"></param>
        /// <returns></returns>
        public ObservableCollection<Teacher> GetTeachers(ObservableCollection<Teacher> teachers, int idChair)
        {
            using (var context = new DBTeachingLoadEntities())
            {
                /// Загрузка связанных сущностей Faculties и Chairs
                var QueryTeacher = from teach in context.Teachers
                                       .Include("DictPost")
                                       .Include("DictTypeEmployment")
                                   where (teach.IdChair == idChair) && (teach.StatusDel == true)
                                   orderby teach.LastName
                                   select teach;

                /// Формирование коллекции классов сущностей Teacher
                foreach (Teacher t in QueryTeacher)
                    teachers.Add(t);

                return teachers;
            }
        }

        /// <summary>
        /// Добавление данных по новому преподавателю кафедры 
        /// </summary>
        /// <param name="newTeacher"></param>
        public void AddItemDataTeacher(Teacher newTeacher)
        {
            using (var context = new DBTeachingLoadEntities())
            {
                try
                {
                    context.Teachers.AddObject(newTeacher);
                    context.SaveChanges();
                }
                catch (DataServiceRequestException ex)
                {
                    throw new ApplicationException("Ошибка добавления данных" + ex.ToString());
                }
                
            }
        }

        /// <summary>
        /// Редактирование данных по преподавателю
        /// </summary>
        /// <param name="editTeacher"></param>
        public void EditItemTeacher(Teacher editTeacher)
        {
            using (var context = new DBTeachingLoadEntities())
            {
                try
                {
                    var teacherEdit = (from teacher in context.Teachers
                                     where teacher.Id == editTeacher.Id
                                     select teacher).FirstOrDefault();

                    if (teacherEdit != null)
                    {
                        context.Teachers.ApplyCurrentValues(editTeacher);
                        context.SaveChanges();
                    }
                }
                catch (OptimisticConcurrencyException ex)
                {
                    throw new InvalidOperationException(string.Format(
                       "Преподаватель с Id '{0}' не может быть отредактирован.\n", editTeacher.Id), ex);
                }
            }
        }

        /// <summary>
        /// Удаление данных по кафедре
        /// </summary>
        /// <param name="delChair"></param>
        public void RemoveItemTeacher(Teacher delTeacher)
        {
            using (var context = new DBTeachingLoadEntities())
            {
                try
                {
                    var deleteTeacher = (from teacher in context.Teachers
                                       where teacher.Id == delTeacher.Id
                                         select teacher).FirstOrDefault();

                    if (deleteTeacher != null)
                    {
                        // Помечаем запись, как удаленную
                        deleteTeacher.StatusDel = false;
                        context.Teachers.ApplyCurrentValues(deleteTeacher);

                        // В случае удаления из базы данных
                        //context.Teachers.DeleteObject(deleteTeacher);
                        
                        context.SaveChanges();
                    }
                }
                catch (OptimisticConcurrencyException ex)
                {
                    throw new InvalidOperationException(string.Format(
                       "Преподаватель с Id '{0}' не может быть удален.\n", delTeacher.Id), ex);
                }
            }
        }

    }
}
