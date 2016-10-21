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
    /// Сервис данных по нагрузке преподавателя
    /// </summary>
    public class ServiceLoadTeaher: IServiceLoadTeaher
    {
        /// <summary>
        /// Загрузка обобщенных данных по нагрузке кафедры из базы данных для заданного учебного года
        /// </summary>
        /// <param name="callback"></param>
        /// <param name="idChair"></param>
        /// <param name="idAcademicYear"></param>
        public void GetDataAllLoadChair(Action<ObservableCollection<TeachingLoadChair>, Exception> callback, int idChair, int idAcademicYear)
        {
            // Коллекция вариантов обобщенных данных по нагрузке кафедры для заданного учебного года 
            ObservableCollection<TeachingLoadChair> allLoadChairAcademicYear = new ObservableCollection<TeachingLoadChair>();

            using (var context = new DBTeachingLoadEntities())
            {
                try
                {
                    var load = (from lc in context.TeachingLoadChairs
                                //.Include("TeachingLoads")
                                where (lc.IdChair == idChair) && (lc.IdAcademicYear == idAcademicYear)
                                select lc);
                    if (load != null)
                        foreach (TeachingLoadChair tl in load)
                            allLoadChairAcademicYear.Add(tl);
                }
                catch (EntityException ex)
                {
                    //throw new ApplicationException("Ошибка загрузки данных" + ex.ToString());
                }
            }
            callback(allLoadChairAcademicYear, null);
        }

        /// <summary>
        /// Получение обобщенных данных по нагрузке кафедры из базы данных для заданного учебного года
        /// </summary>
        /// <param name="allLoadChair"></param>
        /// <param name="idChair"></param>
        /// <param name="idAcademicYear"></param>
        /// <returns></returns>
        public ObservableCollection<TeachingLoadChair> GetAllLoadChair(ObservableCollection<TeachingLoadChair> allLoadChair, int idChair, int idAcademicYear)
        {
            using (var context = new DBTeachingLoadEntities())
            {
                var load = (from lc in context.TeachingLoadChairs
                            //.Include("TeachingLoads")
                            where (lc.IdChair == idChair) && (lc.IdAcademicYear == idAcademicYear)
                            select lc);
                if (load != null)
                    foreach (TeachingLoadChair tl in load)
                        allLoadChair.Add(tl);

                return allLoadChair;
            }
        }

        /// <summary>
        /// Получение обобщенных данных о нагрузке преподавателя
        /// </summary>
        /// <param name="IdTeacher"></param>
        /// <param name="IdAcademicYear"></param>
        /// <returns></returns>
        public TeahingLoadTeacher GetTeahingLoadTeacher(int idTeacher, int idAcademicYear, string nameLoadChair)
        {
            using (var context = new DBTeachingLoadEntities())
            {
                var load = (from lt in context.TeahingLoadTeachers
                            .Include("LoadTeacher")
                            where (lt.IdTeacher == idTeacher) && (lt.IdAcademicYear == idAcademicYear) && (lt.NameLoadChair == nameLoadChair)
                            select lt).FirstOrDefault();
                if (load == null)
                {
                    try
                    {
                        load = new TeahingLoadTeacher();
                        load.IdTeacher = idTeacher;
                        load.IdAcademicYear = idAcademicYear;
                        load.NameLoadChair = nameLoadChair;
                        load.SumLoadTeacher = 0;
                        load.SumLoadSemestr1 = 0;
                        load.SumLoadSemestr2 = 0;
                        load.LoadTeacher = new System.Data.Objects.DataClasses.EntityCollection<LoadTeacher>();

                        context.TeahingLoadTeachers.AddObject(load);
                    }
                    catch (DataServiceRequestException ex)
                    {
                        throw new ApplicationException("Ошибка добавления данных" + ex.ToString());
                    }

                    context.SaveChanges();
                }
                    
                return load;
            }
        }

        /// <summary>
        /// Получение данных о нагрузке преподавателя по дисциплинам 
        /// </summary>
        /// <param name="IdTeachingLoadTeacher"></param>
        /// <returns></returns>
        public ObservableCollection<LoadTeacher> GetLoadTeacher(int IdTeachingLoadTeacher, ObservableCollection<LoadTeacher> teacherLoads)
        {
            using (var context = new DBTeachingLoadEntities())
            {
                var loadTeacher = (from lt in context.LoadTeacher
                                   where (lt.IdTeachingLoadTeacher == IdTeachingLoadTeacher)
                                   select lt);
                if (loadTeacher != null)
                {
                    foreach (LoadTeacher lt in loadTeacher)
                        teacherLoads.Add(lt);
                }

                return teacherLoads;
            }
        }

        /// <summary>
        /// Получение данных о дисциплине, распределенной преподавателю
        /// </summary>
        /// <param name="IdLoadTeacher"></param>
        /// <returns></returns>
        public LoadTeacher GetDisciplineTeacher(int IdLoadTeacher)
        {
            using (var context = new DBTeachingLoadEntities())
            {
                var loadTeacher = (from lt in context.LoadTeacher
                                   where (lt.Id == IdLoadTeacher)
                                   select lt).FirstOrDefault(); ;
                return loadTeacher;
            }
        }


        /// <summary>
        /// Определение существования дисциплины учебной нагрузки
        /// </summary>
        /// <param name="code"></param>
        /// <param name="discipline"></param>
        /// <param name="speciality"></param>
        /// <param name="nameGroup"></param>
        /// <param name="formEducation"></param>
        /// <param name="course"></param>
        /// <param name="semestr"></param>
        /// <returns></returns>
        public bool IsLoad(int idTeachingLoadTeacher, string code, string discipline, string speciality, string nameGroup, string formEducation, int? course, int? semestr)
        {
            bool isLoad = false;

            using (var context = new DBTeachingLoadEntities())
            {
                var load = (from lt in context.LoadTeacher
                            where (lt.IdTeachingLoadTeacher == idTeachingLoadTeacher) &&
                                  (lt.Code == code) &&
                                  (lt.Discipline == discipline) &&
                                  (lt.Speciality == speciality) &&
                                  (lt.NameGroup == nameGroup) &&
                                  (lt.FormEducation == formEducation) &&
                                  (lt.Course == course) &&
                                  (lt.Semester == semestr)
                            select lt).FirstOrDefault();

                if (load != null)
                    isLoad = true;
            }
            return isLoad;
        }

        /// <summary>
        /// Добовление дисциплины в нагрузку преподавателя
        /// </summary>
        /// <param name="loadTeacher"></param>
        public LoadTeacher AddItemLoadTeacher(LoadTeacher newLoadTeacher)
        {
            LoadTeacher load = new LoadTeacher();

            using (var context = new DBTeachingLoadEntities())
            {
                
                try
                {
                    load.IdTeachingLoadTeacher = newLoadTeacher.IdTeachingLoadTeacher;
                    load.NameGroup = newLoadTeacher.NameGroup;
                    load.Student = newLoadTeacher.Student;
                    load.Subgroup = newLoadTeacher.Subgroup;
                    load.ForeignStudent = newLoadTeacher.ForeignStudent;
                    load.Speciality = newLoadTeacher.Speciality;
                    load.Profile = newLoadTeacher.Profile;
                    load.Qualification = newLoadTeacher.Qualification;
                    load.Discipline = newLoadTeacher.Discipline;
                    load.FormEducation = newLoadTeacher.FormEducation;
                    load.ShortNameFaculty = newLoadTeacher.ShortNameFaculty;
                    load.Code = newLoadTeacher.Code;
                    load.SemesterYear = newLoadTeacher.SemesterYear;
                    load.Semester = newLoadTeacher.Semester;
                    load.Course = newLoadTeacher.Course;
                    load.Lecture = 0;
                    load.PracticalExercises = 0;
                    load.LaboratoryWork = 0;
                    load.Examination = 0;
                    load.SetOff = 0;
                    load.Consultation = 0;
                    load.CourseProject = 0;
                    load.CourseWorkt = 0;
                    load.Practical = 0;
                    load.GraduationDesign = 0;
                    load.ControlWork = 0;
                    load.Gac = 0;
                    load.Dot = 0;
                    load.Others = 0;
                    load.SumLoad = 0;
                    load.FlagChange = String.Empty;

                    context.LoadTeacher.AddObject(load);
                }
                catch (DataServiceRequestException ex)
                {
                    throw new ApplicationException("Ошибка добавления данных" + ex.ToString());
                }

                context.SaveChanges();
            }
            return load;
        }

        /// <summary>
        /// Получения данных о соответствии дисциплин нагрузки преподавателя и дисциплин кафедры
        /// для преподавателя
        /// </summary>
        /// <param name="idTeacher"></param>
        /// <returns></returns>
        public ObservableCollection<ChairTeacherLoad> GetChairTeacherLoad(int idTeacher, int status, string nameLoadChair)
        {
            ObservableCollection<ChairTeacherLoad> loads = new ObservableCollection<ChairTeacherLoad>();

            using (var context = new DBTeachingLoadEntities())
            {
                var QueryLoad = from load in context.ChairTeacherLoad
                                    where (load.IdTeacher == idTeacher) && (load.Status == status) && (load.NameLoadChair == nameLoadChair)
                                    select load;

                foreach (ChairTeacherLoad load in QueryLoad)
                    loads.Add(load);
            }
            return loads;
        }

        /// <summary>
        /// Получение данных о соответствии дисциплин нагрузки преподавателя и дисциплин кафедры
        /// для дисциплины
        /// </summary>
        /// <param name="idTeachingLoad"></param>
        /// <param name="nameLoadChair"></param>
        /// <returns></returns>
        public ObservableCollection<ChairTeacherLoad> GetChairTeacherLoadDiscipline(int idTeachingLoad, string nameLoadChair)
        {
            ObservableCollection<ChairTeacherLoad> loads = new ObservableCollection<ChairTeacherLoad>();

            using (var context = new DBTeachingLoadEntities())
            {
                var QueryLoad = from load in context.ChairTeacherLoad
                                where (load.IdTeachingLoad == idTeachingLoad) && (load.NameLoadChair == nameLoadChair)
                                select load;

                foreach (ChairTeacherLoad load in QueryLoad)
                    loads.Add(load);
            }
            return loads;
        }

        /// <summary>
        /// Добавление данных о соответствии дисциплин нагрузки преподавателя и дисциплин кафедры
        /// </summary>
        /// <param name="teach"></param>
        public void AddItemChairTeacherLoad(ChairTeacherLoad newChairTeacherLoad, string nameLoadChair)
        {
            using (var context = new DBTeachingLoadEntities())
            {
                ChairTeacherLoad load = new ChairTeacherLoad();
                try
                {
                    load.IdTeacher = newChairTeacherLoad.IdTeacher;
                    load.IdLoadTeacher = newChairTeacherLoad.IdLoadTeacher;
                    load.IdTeachingLoad = newChairTeacherLoad.IdTeachingLoad;
                    load.Status = newChairTeacherLoad.Status;
                    load.NameLoadChair = nameLoadChair;

                    context.ChairTeacherLoad.AddObject(load);
                }
                catch (DataServiceRequestException ex)
                {
                    throw new ApplicationException("Ошибка добавления данных" + ex.ToString());
                }

                context.SaveChanges();
            }
        }

        // <summary>
        /// Поиск данных соответствия дисциплин нагрузки преподавателя по дисциплине кафедры
        /// </summary>
        /// <param name="teach"></param>
        /// <returns></returns>
        public ChairTeacherLoad FindChairTeacherLoad(int idTeachingLoad, int status)
        {
            using (var context = new DBTeachingLoadEntities())
            {
                var chairTeacherLoad = (from load in context.ChairTeacherLoad
                                where (load.IdTeachingLoad == idTeachingLoad) && (load.Status == status)
                                select load).FirstOrDefault();

                return chairTeacherLoad;
            }
        }

        /// <summary>
        /// Получение данных соответствия дисциплин нагрузки преподавателей по дисциплине кафедры
        /// </summary>
        /// <param name="idTeachingLoad"></param>
        /// <returns></returns>
        public ObservableCollection<ChairTeacherLoad> GetChairTeacherLoad(int idTeachingLoad)
        {
            ObservableCollection<ChairTeacherLoad> loads = new ObservableCollection<ChairTeacherLoad>();

            using (var context = new DBTeachingLoadEntities())
            {
                var chairTeacherLoad = from load in context.ChairTeacherLoad
                                        where (load.IdTeachingLoad == idTeachingLoad) 
                                        select load;

                foreach (ChairTeacherLoad ctl in chairTeacherLoad)
                    loads.Add(ctl);

                return loads;
            }
        }

        /// <summary>
        /// Получение данных соответствия дисциплин нагрузки ассистетна по дисциплине кафедры
        /// </summary>
        /// <param name="idTeachingLoad"></param>
        /// <returns></returns>
        public ObservableCollection<ChairTeacherLoad> GetChairAssistentLoad(int idTeachingLoad, int status)
        {
            ObservableCollection<ChairTeacherLoad> loads = new ObservableCollection<ChairTeacherLoad>();

            using (var context = new DBTeachingLoadEntities())
            {
                var chairTeacherLoad = from load in context.ChairTeacherLoad
                                       where (load.IdTeachingLoad == idTeachingLoad) && (load.Status == status)
                                       select load;

                foreach (ChairTeacherLoad ctl in chairTeacherLoad)
                    loads.Add(ctl);

                return loads;
            }
        }

        /// <summary>
        /// Удаление данных соответствия дисциплин нагрузки преподавателя по дисциплине кафедры
        /// </summary>
        /// <param name="DelChairTeacherLoad"></param>
        public void RemoveItemChairTeacherLoad(ChairTeacherLoad delChairTeacherLoad)
        {
            using (var context = new DBTeachingLoadEntities())
            {
                try
                {
                    var deleteChairTeacherLoad = (from ctl in context.ChairTeacherLoad
                                                  where ctl.Id == delChairTeacherLoad.Id
                                       select ctl).FirstOrDefault();

                    //if (deleteChairTeacherLoad != null)
                    //{
                        context.ChairTeacherLoad.DeleteObject(deleteChairTeacherLoad);
                        context.SaveChanges();
                    //}
                }
                catch (DataServiceRequestException ex)
                {
                    throw new InvalidOperationException(string.Format(
                       "Запись ChairTeacherLoad с Id '{0}' не может быть удален.\n", delChairTeacherLoad.Id), ex);
                }
            }
        }

        /// <summary>
        /// Удаление дисциплины нагрузки преподавателя 
        /// </summary>
        /// <param name="delLoadTeacher"></param>
        public void RemoveItemLoadTeacher(LoadTeacher delDiscipline)
        {
            using (var context = new DBTeachingLoadEntities())
            {
                try
                {
                    var deleteLoadTeacher = (from disc in context.LoadTeacher
                                             where disc.Id == delDiscipline.Id
                                                  select disc).First();

                    context.LoadTeacher.DeleteObject(deleteLoadTeacher);
                    context.SaveChanges();
                }
                catch (OptimisticConcurrencyException ex)
                {
                    throw new InvalidOperationException(string.Format(
                       "Дисциплина преподавателя с Id '{0}' не может быть удален.\n", delDiscipline.Id), ex);
                }
            }
        }

        /// <summary>
        /// Редактирование дисциплины нагрузки преподавателя 
        /// </summary>
        /// <param name="editLoadTeacher"></param>
        public void EditItemLoadTeacher(LoadTeacher editLoadTeacher)
        {
            using (var context = new DBTeachingLoadEntities())
            {
                try
                {
                    var editLoad = (from load in context.LoadTeacher
                                    where load.Id == editLoadTeacher.Id
                                     select load).First();
                    context.LoadTeacher.ApplyCurrentValues(editLoadTeacher);

                    context.SaveChanges();
                }
                catch (OptimisticConcurrencyException ex)
                {
                    throw new InvalidOperationException(string.Format(
                       "Дисциплина преподавателя с Id '{0}' не может быть отредактирован.\n", editLoadTeacher.Id), ex);
                }
            }
        }

        /// <summary>
        /// Редактирование общих данных нагрузки преподавателя 
        /// </summary>
        /// <param name="editTeahingLoadTeacher"></param>
        public void EditItemTeahingLoadTeacher(TeahingLoadTeacher editTeahingLoadTeacher)
        {
            using (var context = new DBTeachingLoadEntities())
            {
                try
                {
                    var editLoad = (from load in context.TeahingLoadTeachers
                                    where load.Id == editTeahingLoadTeacher.Id
                                    select load).First();
                    context.TeahingLoadTeachers.ApplyCurrentValues(editTeahingLoadTeacher);

                    context.SaveChanges();
                }
                catch (OptimisticConcurrencyException ex)
                {
                    throw new InvalidOperationException(string.Format(
                       "Общие данные по нагрузке преподавателя с Id '{0}' не могут быть отредактирован.\n", editTeahingLoadTeacher.Id), ex);
                }
            }
        }

        /// <summary>
        /// Редактирование дисциплины нагрузки кафедры
        /// </summary>
        /// <param name="editTeachingLoad"></param>
        public void EditTeachingLoad(TeachingLoad editTeachingLoad)
        {
            using (var context = new DBTeachingLoadEntities())
            {
                try
                {
                    var editLoad = (from load in context.TeachingLoads
                                    where load.Id == editTeachingLoad.Id
                                    select load).First();
                    context.TeachingLoads.ApplyCurrentValues(editTeachingLoad);

                    context.SaveChanges();
                }
                catch (OptimisticConcurrencyException ex)
                {
                    throw new InvalidOperationException(string.Format(
                       "Дисциплина кафедры с Id '{0}' не может быть отредактирован.\n", editTeachingLoad.Id), ex);
                }
            }
        }

        /// <summary>
        /// Редактирование общих данных нагрузки кафедры 
        /// </summary>
        /// <param name="editTeachingLoadChair"></param>
        public void EditTeachingLoadChair(TeachingLoadChair editTeachingLoadChair)
        {
            using (var context = new DBTeachingLoadEntities())
            {
                try
                {
                    var editLoad = (from load in context.TeachingLoadChairs
                                    where load.Id == editTeachingLoadChair.Id
                                    select load).First();
                    context.TeachingLoadChairs.ApplyCurrentValues(editTeachingLoadChair);

                    context.SaveChanges();
                }
                catch (OptimisticConcurrencyException ex)
                {
                    throw new InvalidOperationException(string.Format(
                       "Общие данные по нагрузке кафедры с Id '{0}' не могут быть отредактирован.\n", editTeachingLoadChair.Id), ex);
                }
            }
        }

        /// <summary>
        /// Получение обобщенных данных о нагрузке преподавателей кафедры
        /// </summary>
        /// <param name="idChair"></param>
        /// <param name="idAcademicYear"></param>
        /// <returns></returns>
        public ObservableCollection<tlsp_getAllLoadTeacher_Result> GetAllLoadTeacher(int idChair, int? idAcademicYear)
        {
            ObservableCollection<tlsp_getAllLoadTeacher_Result> loadsAllTeacher = new ObservableCollection<tlsp_getAllLoadTeacher_Result>();
            using (var context = new DBTeachingLoadEntities())
            {
                var QueryLoadsTeacher = context.tlsp_getAllLoadTeacher(idAcademicYear, idChair);

                foreach (var disc in QueryLoadsTeacher)
                    loadsAllTeacher.Add(disc);
            }
            return loadsAllTeacher;
        }

    }
}
