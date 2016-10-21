using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.Services.Client;
using System.Linq;
using MvvmLight2.Helper;
using MvvmLight2.Model;

namespace MvvmLight2.ServiceData
{
    /// <summary>
    /// Сервис данных для студенческих групп
    /// </summary>
    public class ServiceGroup : IServiceGroup
    {
        /// <summary>
        /// Получение данных по группам из базы данных
        /// </summary>
        /// <param name="callback"></param>
        /// <param name="idFaculty"></param>
        public void GetDataGroup(Action<ObservableCollection<StudentGroup>, Exception> callback, int idFaculty, int idAcademicYear)
        {
            ObservableCollection<StudentGroup> groups = new ObservableCollection<StudentGroup>();

            using (var context = new DBTeachingLoadEntities())
            {
                try
                {
                    var QueryGroup = from gr in context.StudentGroups
                                            .Include("DictAcademicYear")
                                            .Include("DictQualification")
                                            .Include("DictSpeciality")
                                            .Include("DictEducationForm")
                                     where (gr.IdFaculty == idFaculty) && (gr.IdAcademicYear == idAcademicYear) && (gr.StatusDel == true)
                                     orderby gr.DictQualification.NameQualification, gr.DictEducationForm.FormEducation descending, gr.NameGroup
                                     select gr;

                    foreach (StudentGroup gr in QueryGroup)
                        groups.Add(gr);
                }
                catch (EntityException ex)
                {
                    //throw new ApplicationException("Ошибка загрузки данных" + ex.ToString());
                }
            }
            callback(groups, null);
        }


        /// <summary>
        /// Загрузка данных по группам из базы данных
        /// </summary>
        /// <param name="groups"></param>
        /// <param name="idFaculty"></param>
        /// <param name="idAcademicYear"></param>
        /// <returns></returns>
        public ObservableCollection<StudentGroup> GetGroup(ObservableCollection<StudentGroup> groups, int idFaculty, int idAcademicYear)
        {
            using (var context = new DBTeachingLoadEntities())
            {
                var QueryGroup = from gr in context.StudentGroups
                                            .Include("DictAcademicYear")
                                            .Include("DictQualification")
                                            .Include("DictSpeciality")
                                            .Include("DictEducationForm")
                                            .Include("Faculty")
                                 where (gr.IdFaculty == idFaculty) && (gr.IdAcademicYear == idAcademicYear) && (gr.StatusDel == true)
                                 orderby gr.DictQualification.NameQualification, gr.DictEducationForm.FormEducation descending, gr.NameGroup
                                 select gr;

                foreach (StudentGroup gr in QueryGroup)
                    groups.Add(gr);
            }

            return groups;
        }

        /// <summary>
        /// Загрузка данных по группам из базы данных
        /// </summary>
        /// <param name="idFaculty"></param>
        /// <param name="idAcademicYear"></param>
        /// <returns></returns>
        public ObservableCollection<StudentGroup> GetGroup(int idFaculty, int idAcademicYear)
        {
            ObservableCollection<StudentGroup> groups = new ObservableCollection<StudentGroup>();
            using (var context = new DBTeachingLoadEntities())
            {
                var QueryGroup = from gr in context.StudentGroups
                                            .Include("DictAcademicYear")
                                            .Include("DictQualification")
                                            .Include("DictSpeciality")
                                            .Include("DictEducationForm")
                                            .Include("Faculty")
                                 where (gr.IdFaculty == idFaculty) && (gr.IdAcademicYear == idAcademicYear) && (gr.StatusDel == true)
                                 orderby gr.DictQualification.NameQualification, gr.DictEducationForm.FormEducation descending, gr.NameGroup
                                 select gr;

                foreach (StudentGroup gr in QueryGroup)
                    groups.Add(gr);
            }

            return groups;
        }


        /// <summary>
        /// Загрузки данных по группам всех факультетов 
        /// </summary>
        /// <param name="groups"></param>
        /// <param name="idAcademicYear"></param>
        /// <returns></returns>
        public ObservableCollection<StudentGroup> GetAllGroup(ObservableCollection<StudentGroup> groups, int idAcademicYear)
        {
            using (var context = new DBTeachingLoadEntities())
            {
                var QueryGroup = from gr in context.StudentGroups
                                            .Include("DictAcademicYear")
                                            .Include("DictQualification")
                                            .Include("DictSpeciality")
                                            .Include("DictEducationForm")
                                            .Include("Faculty")
                                 where (gr.IdAcademicYear == idAcademicYear) && (gr.StatusDel == true)
                                 orderby gr.DictQualification.NameQualification, gr.DictEducationForm.FormEducation descending, gr.NameGroup
                                 select gr;

                foreach (StudentGroup gr in QueryGroup)
                    groups.Add(gr);
            }

            return groups;
        }

        /// <summary>
        /// Загрузки данных по группам всех факультетов 
        /// </summary>
        /// <param name="idAcademicYear"></param>
        /// <returns></returns>
        public ObservableCollection<StudentGroup> GetAllGroup(int idAcademicYear)
        {
            ObservableCollection<StudentGroup> groups = new ObservableCollection<StudentGroup>();

            using (var context = new DBTeachingLoadEntities())
            {
                
                var QueryGroup = from gr in context.StudentGroups
                                            .Include("DictAcademicYear")
                                            .Include("DictQualification")
                                            .Include("DictSpeciality")
                                            .Include("DictEducationForm")
                                            .Include("Faculty")
                                 where (gr.IdAcademicYear == idAcademicYear) && (gr.StatusDel == true)
                                 orderby gr.DictQualification.NameQualification, gr.DictEducationForm.FormEducation descending, gr.NameGroup
                                 select gr;

                foreach (StudentGroup gr in QueryGroup)
                    groups.Add(gr);
            }
            return groups;
        }

        /// <summary>
        /// Формирование данных по группам для отчета
        /// </summary>
        /// <param name="idAcademicYear"></param>
        /// <returns></returns>
        public ObservableCollection<tlsp_getGroups_Result> GetGroupForReport(int idAcademicYear)
        {
            ObservableCollection<tlsp_getGroups_Result> groups = new ObservableCollection<tlsp_getGroups_Result>();
            using (var context = new DBTeachingLoadEntities())
            {
                var query = context.tlsp_getGroups(idAcademicYear);

                foreach (var group in query)
                    groups.Add(group);
            }
            return groups;
        }


        /// <summary>
        /// Загрузка списка учебных планов 
        /// </summary>
        /// <param name="idAcademicYear"></param>
        /// <returns></returns>
        public ObservableCollection<CurriculumShort> GetCurriculum(int idAcademicYear)
        {
            ObservableCollection<CurriculumShort> listCurriculum = new ObservableCollection<CurriculumShort>();
            using (var context = new DBTeachingLoadEntities())
            {
                var QueryCurriculum = from curr in context.Curriculum
                                      where curr.IdAcademicYear == idAcademicYear
                                      orderby curr.Name
                                      select curr;

                foreach (var c in QueryCurriculum)
                    listCurriculum.Add(new CurriculumShort
                    {
                        Id = c.Id,
                        Name = c.Name
                    });

                return listCurriculum;
            }
        }

        /// <summary>
        /// Редактирование данных по группе
        /// </summary>
        /// <param name="editGroup"></param>
        public void EditItemGroup(StudentGroup editGroup)
        {
            using (var context = new DBTeachingLoadEntities())
            {
                try
                {
                    var groupEdit = (from gr in context.StudentGroups
                                     where gr.Id == editGroup.Id
                                     select gr).FirstOrDefault();

                    if (groupEdit != null)
                    {
                        context.StudentGroups.ApplyCurrentValues(editGroup);
                        context.SaveChanges();
                    }
                }
                catch (OptimisticConcurrencyException ex)
                {
                    throw new InvalidOperationException(string.Format(
                       "Группа с Id '{0}' не может быть отредактирован.\n", editGroup.Id), ex);
                }
            }
        }

        /// <summary>
        /// Добавлние данных по новой группе
        /// </summary>
        /// <param name="newGroup"></param>
        public void AddItemDataGroup(StudentGroup newGroup)
        {
            using (var context = new DBTeachingLoadEntities())
            {
                try
                {
                    context.StudentGroups.AddObject(newGroup);
                    context.SaveChanges();
                }
                catch (DataServiceRequestException ex)
                {
                    throw new ApplicationException("Ошибка добавления данных" + ex.ToString());
                }
            }
        }

        /// <summary>
        /// Удаление данных по группе
        /// </summary>
        /// <param name="delGroup"></param>
        public void RemoveItemGroup(StudentGroup delGroup)
        {
            using (var context = new DBTeachingLoadEntities())
            {
                try
                {
                    var deleteGroup = (from gr in context.StudentGroups
                                       where gr.Id == delGroup.Id
                                       select gr).FirstOrDefault();

                    if (deleteGroup != null)
                    {
                        // Помечаем запись, как удаленную
                        deleteGroup.StatusDel = false;
                        context.StudentGroups.ApplyCurrentValues(deleteGroup);

                        // В случае удаления из базы данных
                        //context.StudentGroups.DeleteObject(deleteGroup);
                        context.SaveChanges();
                    }
                }
                catch (OptimisticConcurrencyException ex)
                {
                    throw new InvalidOperationException(string.Format(
                       "Группа с Id '{0}' не может быть удален.\n", delGroup.Id), ex);
                }
            }
        }
    }
}
