using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.Services.Client;
using System.Linq;
using MvvmLight2.Model;

namespace MvvmLight2.ServiceData
{
    /// <summary>
    /// Сервис данных по нагрузке кафедры
    /// </summary>
    public class ServiceLoadChair:IServiceLoadChair
    {
        /// <summary>
        /// Загрузка данных по нагрузке кафедры
        /// </summary>
        /// <param name="callback"></param>
        /// <param name="IdChair"></param>
        /// <param name="IdAcademicYear"></param>
        public void GetDataLoadChairTeaching(Action<ObservableCollection<TeachingLoad>, Exception> callback, int idChair, int idAcademicYear)
        {
            ObservableCollection<TeachingLoad> loadChair = new ObservableCollection<TeachingLoad>();

            using (var context = new DBTeachingLoadEntities())
            {
                try
                {
                    var load = (from lc in context.TeachingLoadChairs
                                            .Include("TeachingLoads")
                                where (lc.IdChair == idChair) && (lc.IdAcademicYear == idAcademicYear)
                                select lc).FirstOrDefault();
                    if (load != null)
                        foreach (TeachingLoad tl in load.TeachingLoads)
                            loadChair.Add(tl);
                }
                catch (EntityException ex)
                {
                    //throw new ApplicationException("Ошибка загрузки данных" + ex.ToString());
                }
            }
            callback(loadChair, null);
        }


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

        public ObservableCollection<TeachingLoadChair> GetAllLoadChair(int idChair, int idAcademicYear)
        {
            ObservableCollection<TeachingLoadChair> allLoadChair = new ObservableCollection<TeachingLoadChair>();

            using (var context = new DBTeachingLoadEntities())
            {
                var load = (from lc in context.TeachingLoadChairs
                            where (lc.IdChair == idChair) && (lc.IdAcademicYear == idAcademicYear)
                            select lc);
                if (load != null)
                    foreach (TeachingLoadChair tl in load)
                        allLoadChair.Add(tl);

                return allLoadChair;
            }
        }

        /// <summary>
        /// Получение данных по нагрузке кафедры
        /// </summary>
        /// <param name="callback"></param>
        /// <param name="idChair"></param>
        /// <param name="idAcademicYear"></param>
        /// <returns></returns>
        public ObservableCollection<TeachingLoad> GetLoadChairTeaching(ObservableCollection<TeachingLoad> loadChair, 
            int idChair, int idAcademicYear, string nameLoad)
        {
            using (var context = new DBTeachingLoadEntities())
            {
                var load = (from lc in context.TeachingLoadChairs
                                        .Include("TeachingLoads")
                            where (lc.IdChair == idChair) && (lc.IdAcademicYear == idAcademicYear) && (lc.NameLoadChair == nameLoad)
                            select lc).FirstOrDefault();
                if (load != null)
                    foreach (TeachingLoad tl in load.TeachingLoads)
                        loadChair.Add(tl);
               
            }
            return loadChair;
        }

        public ObservableCollection<TeachingLoad> GetLoadChairTeaching(int idChair, int idAcademicYear, string nameLoad)
        {
            ObservableCollection<TeachingLoad> loadChair = new ObservableCollection<TeachingLoad>();
            using (var context = new DBTeachingLoadEntities())
            {
                var load = (from lc in context.TeachingLoadChairs
                                        .Include("TeachingLoads")
                            where (lc.IdChair == idChair) && (lc.IdAcademicYear == idAcademicYear) && (lc.NameLoadChair == nameLoad)
                            select lc).FirstOrDefault();
                if (load != null)
                    foreach (TeachingLoad tl in load.TeachingLoads)
                        loadChair.Add(tl);

            }
            return loadChair;
        }


        /// <summary>
        /// Получение  данных по расределенным дисциплинам кафедры кафедры
        /// </summary>
        /// <param name="loadChair"></param>
        /// <param name="idChair"></param>
        /// <param name="idAcademicYear"></param>
        /// <param name="nameTeacher"></param>
        /// <param name="isLoad"></param>
        /// <returns></returns>
        public ObservableCollection<TeachingLoad> GetDisciplinesChairIsLoad(ObservableCollection<TeachingLoad> loadChair, 
            int idChair, int idAcademicYear, string nameLoad, bool isLoad)
        {
            using (var context = new DBTeachingLoadEntities())
            {
                var load = (from lc in context.TeachingLoadChairs
                                        .Include("TeachingLoads")
                            where (lc.IdChair == idChair) && (lc.IdAcademicYear == idAcademicYear) && (lc.NameLoadChair == nameLoad)
                            select lc).FirstOrDefault();
                if (load != null)
                    foreach (TeachingLoad tl in load.TeachingLoads)
                        if(tl.IsLoad == isLoad)
                            loadChair.Add(tl);
            }
            return loadChair;
        }

        /// <summary>
        /// Получение обобщенных данных по нагрузке кафедры
        /// </summary>
        /// <param name="teach"></param>
        /// <param name="idChair"></param>
        /// <param name="idAcademicYear"></param>
        /// <returns></returns>
        public TeachingLoadChair GetLoadChair(TeachingLoadChair load, int idChair, int? idAcademicYear, string nameLoadChair)
        {
            using (var context = new DBTeachingLoadEntities())
            {
                var loadEdit = (from lc in context.TeachingLoadChairs
                            where (lc.IdChair == idChair) && 
                                  (lc.IdAcademicYear == idAcademicYear) &&
                                  (lc.NameLoadChair == nameLoadChair)
                            select lc).FirstOrDefault();

                if (loadEdit != null)
                {
                    load.Id = loadEdit.Id;
                    load.IdChair = loadEdit.IdChair;
                    load.IdAcademicYear = loadEdit.IdAcademicYear;
                    load.NameLoadChair = loadEdit.NameLoadChair;
                    load.SumLoadChair = loadEdit.SumLoadChair;
                    load.SumUnloadChair = loadEdit.SumUnloadChair;
                    load.SumLoadChairCommerce = loadEdit.SumLoadChairCommerce;
                    load.SumLoadSemestr1 = loadEdit.SumLoadSemestr1;
                    load.SumLoadSemestr2 = loadEdit.SumLoadSemestr2;
                    load.SumLoadCommerceSemestr1 = loadEdit.SumLoadCommerceSemestr1;
                    load.SumLoadCommerceSemestr2 = loadEdit.SumLoadCommerceSemestr2;
                    load.DataLoadChair = loadEdit.DataLoadChair;
                    load.Note = loadEdit.Note;
                    load.IndexStreamLecture = loadEdit.IndexStreamLecture;
                    load.IndexStreamLab = loadEdit.IndexStreamLab;
                    load.IndexStreamPract = loadEdit.IndexStreamPract;
                }
            }
            return load;
        }

        /// <summary>
        /// Загрузка дисциплин кафедры с данными по группам
        /// </summary>
        /// <param name="idChair"></param>
        /// <param name="idAcademicYear"></param>
        /// <returns></returns>
        public ObservableCollection<tlsp_setDisciplineChairWithGroup_Result> GetDisciplinesChair(int idChair, int? idAcademicYear)
        {
            ObservableCollection<tlsp_setDisciplineChairWithGroup_Result> disciplines = new ObservableCollection<tlsp_setDisciplineChairWithGroup_Result>();
            using (var context = new DBTeachingLoadEntities())
            {
                var QueryDisciplineChair = context.tlsp_setDisciplineChairWithGroup(idAcademicYear, idChair);

                foreach (tlsp_setDisciplineChairWithGroup_Result disc in QueryDisciplineChair)
                    disciplines.Add(disc);
            }
            return disciplines;
        }


        /// <summary>
        /// Загрузка норм времени
        /// </summary>
        /// <param name="IdAcademicYear"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public ObservableCollection<tlsp_getStandartTime_Result> GetStandartTime(int? idAcademicYear, bool status)
        {
            ObservableCollection<tlsp_getStandartTime_Result> standartTime = new ObservableCollection<tlsp_getStandartTime_Result>();
            using (var context = new DBTeachingLoadEntities())
            {
                var QueryDisciplineChair = context.tlsp_getStandartTime(idAcademicYear, status);

                foreach (tlsp_getStandartTime_Result standart in QueryDisciplineChair)
                    standartTime.Add(standart);
            }
            return standartTime;
        }

        /// <summary>
        /// Редактирования данных по дисциплине 
        /// </summary>
        /// <param name="editTeachingLoadChair"></param>
        public void EditItemTeachingLoadChair(TeachingLoad editTeachingLoadChair)
        {
            using (var context = new DBTeachingLoadEntities())
            {
                try
                {
                    var loadEdit = (from lc in context.TeachingLoads
                                    where lc.Id == editTeachingLoadChair.Id
                                    select lc).FirstOrDefault();
                    if (loadEdit != null)
                    {
                        context.TeachingLoads.ApplyCurrentValues(editTeachingLoadChair);
                        context.SaveChanges();
                    }
                }
                catch (OptimisticConcurrencyException ex)
                {
                    throw new InvalidOperationException(string.Format(
                       "Нагрузка с Id '{0}' не может быть отредактирован.\n", editTeachingLoadChair.Id), ex);
                }
            }
        }

        /// <summary>
        /// Редактирование общих данных по нагрузке кафедры
        /// </summary>
        /// <param name="editLoadChair"></param>
        public void EditItemLoadChair(TeachingLoadChair editLoadChair)
        {
            using (var context = new DBTeachingLoadEntities())
            {
                try
                {
                    var loadEdit = (from lc in context.TeachingLoadChairs
                                    where lc.Id == editLoadChair.Id
                                    select lc).FirstOrDefault();
                    if (loadEdit != null)
                    {
                        context.TeachingLoadChairs.ApplyCurrentValues(editLoadChair);
                        context.SaveChanges();
                    }
                }
                catch (OptimisticConcurrencyException ex)
                {
                    throw new InvalidOperationException(string.Format(
                       "Общая нагрузка кафедры с Id '{0}' не может быть отредактирован.\n", editLoadChair.Id), ex);
                }
            }
        }


        /// <summary>
        /// Добавление новой нагрузки по дисциплине в список нагрузки кафедры
        /// </summary>
        /// <param name="newLoadChair"></param>
        public void AddItemLoadChair(TeachingLoad newLoadChair)
        {
            using (var context = new DBTeachingLoadEntities())
            {
                try
                {
                    context.TeachingLoads.AddObject(newLoadChair);
                    context.SaveChanges();
                }
                catch (DataServiceRequestException ex)
                {
                    throw new ApplicationException("Ошибка добавления данных" + ex.ToString());
                }
            }
        }

        /// <summary>
        /// Добавление новой обобщенной нагрузки кафедры
        /// </summary>
        /// <param name="newLoad"></param>
        public void AddLoadChair(TeachingLoadChair newLoad)
        {
             using (var context = new DBTeachingLoadEntities())
            {
                try
                {
                    context.TeachingLoadChairs.AddObject(newLoad);
                    context.SaveChanges();
                }
                catch (DataServiceRequestException ex)
                {
                    throw new ApplicationException("Ошибка добавления данных" + ex.ToString());
                }
            }
        }

        /// <summary>
        /// Удалениу нагрузки по дисциплине в списке нагрузки кафедры
        /// </summary>
        /// <param name="delLoadChair"></param>
        public void RemoveItemLoadChair(TeachingLoad delLoadChair)
        {
            using (var context = new DBTeachingLoadEntities())
            {
                try
                {
                    var deleteLoad = (from load in context.TeachingLoads
                                      where load.Id == delLoadChair.Id
                                       select load).FirstOrDefault();

                    if (deleteLoad != null)
                    {
                        context.TeachingLoads.DeleteObject(deleteLoad);
                        context.SaveChanges();
                    }
                }
                catch (UpdateException ex)
                {
                    throw new InvalidOperationException(string.Format(
                       "Нагрузка  по дисциплине с Id '{0}' не может быть удалена\n", delLoadChair.Id), ex);
                }
            }
        }

        /// <summary>
        /// Удалениу нагрузки по дисциплине в списке нагрузки кафедры с возвратом успеха операции
        /// </summary>
        /// <param name="delLoadChair"></param>
        public bool RemoveItemLoadChair(TeachingLoad delLoadChair, out string messageError)
        {
            messageError = string.Empty;
            bool result = false;
            using (var context = new DBTeachingLoadEntities())
            {
                try
                {
                    var deleteLoad = (from load in context.TeachingLoads
                                      where load.Id == delLoadChair.Id
                                      select load).FirstOrDefault();

                    if (deleteLoad != null)
                    {
                        context.TeachingLoads.DeleteObject(deleteLoad);
                        context.SaveChanges();
                    }
                }
                catch (UpdateException ex)
                {
                    //throw new InvalidOperationException(string.Format(
                    //   "Нагрузка  по дисциплине с Id '{0}' не может быть удалена\n", delLoadChair.Id), ex);

                    messageError = string.Format("Дисциплина нагрузки\n '{0}' \nне может быть удалена!\nИспользуется в распределении дисциплин преподавателям кафедры", delLoadChair.Discipline);
                    result = true;
                }

                return result;
            }
        }

        /// <summary>
        /// Удаление нагрузки
        /// </summary>
        /// <param name="delLoad"></param>
        public void RemoveLoad(TeachingLoadChair delLoad)
        {
            using (var context = new DBTeachingLoadEntities())
            {
                try
                {
                    var deleteLoad = (from load in context.TeachingLoadChairs
                                      where load.Id == delLoad.Id
                                      select load).FirstOrDefault();

                    if (deleteLoad != null)
                    {
                        context.TeachingLoadChairs.DeleteObject(deleteLoad);
                        context.SaveChanges();
                    }
                }
                catch (OptimisticConcurrencyException ex)
                {
                    throw new InvalidOperationException(string.Format(
                       "Нагрузка с Id '{0}' не может быть удалена\n", delLoad.Id), ex);
                }
            }
        }

        /// <summary>
        /// Получение данных по нагрузке кафедры для очного отделения
        /// </summary>
        /// <param name="callback"></param>
        /// <param name="idChair"></param>
        /// <param name="idAcademicYear"></param>
        /// <returns></returns>
        public ObservableCollection<TeachingLoad> GetLoadChairTeachingFullTime(ObservableCollection<TeachingLoad> loadChair,
            int idChair, int idAcademicYear, string nameLoad)
        {
            using (var context = new DBTeachingLoadEntities())
            {
                var load = (from lc in context.TeachingLoadChairs
                                        .Include("TeachingLoads")
                            where (lc.IdChair == idChair) && (lc.IdAcademicYear == idAcademicYear) && (lc.NameLoadChair == nameLoad)
                            select lc).FirstOrDefault();
                if (load != null)
                    foreach (TeachingLoad tl in load.TeachingLoads)
                        if (tl.FormEducation == "очная")
                            loadChair.Add(tl);

            }
            return loadChair;
        }

        /// <summary>
        /// Получение данных по нагрузке кафедры для очной формы обучения
        /// </summary>
        /// <param name="idChair"></param>
        /// <param name="idAcademicYear"></param>
        /// <param name="nameLoad"></param>
        /// <returns></returns>
        public ObservableCollection<TeachingLoad> GetLoadChairTeachingFullTime(int idChair, int idAcademicYear, string nameLoad)
        {
            ObservableCollection<TeachingLoad> loadChair = new ObservableCollection<TeachingLoad>();

            using (var context = new DBTeachingLoadEntities())
            {
                var load = (from lc in context.TeachingLoadChairs
                                        .Include("TeachingLoads")
                            where (lc.IdChair == idChair) && (lc.IdAcademicYear == idAcademicYear) && (lc.NameLoadChair == nameLoad)
                            select lc).FirstOrDefault();
                if (load != null)
                    foreach (TeachingLoad tl in load.TeachingLoads)
                        if (tl.FormEducation == "очная")
                            loadChair.Add(tl);

            }
            return loadChair;
        }

        /// <summary>
        /// Получение данных по нагрузке кафедры для заочного отделения
        /// </summary>
        /// <param name="callback"></param>
        /// <param name="idChair"></param>
        /// <param name="idAcademicYear"></param>
        /// <returns></returns>
        public ObservableCollection<TeachingLoad> GetLoadChairTeachingPartTime(ObservableCollection<TeachingLoad> loadChair,
            int idChair, int idAcademicYear, string nameLoad)
        {
            using (var context = new DBTeachingLoadEntities())
            {
                var load = (from lc in context.TeachingLoadChairs
                                        .Include("TeachingLoads")
                            where (lc.IdChair == idChair) && (lc.IdAcademicYear == idAcademicYear) && (lc.NameLoadChair == nameLoad)
                            select lc).FirstOrDefault();
                if (load != null)
                    foreach (TeachingLoad tl in load.TeachingLoads)
                        if (tl.FormEducation != "очная")
                            loadChair.Add(tl);

            }
            return loadChair;
        }

        /// <summary>
        /// Получение данных по нагрузке кафедры для заочного отделения
        /// </summary>
        /// <param name="idChair"></param>
        /// <param name="idAcademicYear"></param>
        /// <param name="nameLoad"></param>
        /// <returns></returns>
        public ObservableCollection<TeachingLoad> GetLoadChairTeachingPartTime(int idChair, int idAcademicYear, string nameLoad)
        {
            ObservableCollection<TeachingLoad> loadChair = new ObservableCollection<TeachingLoad>();
            using (var context = new DBTeachingLoadEntities())
            {
                var load = (from lc in context.TeachingLoadChairs
                                        .Include("TeachingLoads")
                            where (lc.IdChair == idChair) && (lc.IdAcademicYear == idAcademicYear) && (lc.NameLoadChair == nameLoad)
                            select lc).FirstOrDefault();
                if (load != null)
                    foreach (TeachingLoad tl in load.TeachingLoads)
                        if (tl.FormEducation != "очная")
                            loadChair.Add(tl);

            }
            return loadChair;
        }
    }
}
