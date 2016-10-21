using System;
using System.Collections.ObjectModel;
using System.Linq;
using MvvmLight2.Model;
using System.Data.Services.Client;
using System.Data;

namespace MvvmLight2.ServiceData
{
    /// <summary>
    /// Сервис данных по дисциплинам кафедры из базы данных
    /// </summary>
    public class ServiceDisciplineChair: IServiceDisciplineChair
    {

        /// <summary>
        /// Загрузка данных по дисциплинам кафедры 
        /// для заданного учебного года
        /// с помощью хранимой процедуры tlsp_getDisciplineChair
        /// </summary>
        /// <param name="callback"></param>
        /// <param name="idChair"></param>
        /// <param name="idAcademicYear"></param>
        public void GetDataDiscipline(Action<ObservableCollection<tlsp_getDisciplineChair_Result>, Exception> callback, int idChair, int idAcademicYear)
        {
            ObservableCollection<tlsp_getDisciplineChair_Result> disciplins = new ObservableCollection<tlsp_getDisciplineChair_Result>();

            using (var context = new DBTeachingLoadEntities())
            {
                try
                {
                    var QueryDisciplineChair = context.tlsp_getDisciplineChair(idAcademicYear, idChair);

                    foreach (var disc in QueryDisciplineChair)
                        disciplins.Add(disc);
                }
                catch (EntityException ex)
                {
                    //throw new ApplicationException("Ошибка загрузки данных" + ex.ToString());
                }
            }
            callback(disciplins, null);
        }

        /// <summary>
        /// Загрузка данных по дисциплинам кафедры 
        /// для заданного учебного года для очного отделения
        /// с помощью хранимой процедуры tlsp_getDisciplineChairFullTime
        /// </summary>
        /// <param name="callback"></param>
        /// <param name="idChair"></param>
        /// <param name="idAcademicYear"></param>
        public void GetDataDisciplineFullTime(Action<ObservableCollection<tlsp_getDisciplineChairFullTime_Result>, Exception> callback, int idChair, int idAcademicYear)
        {
            ObservableCollection<tlsp_getDisciplineChairFullTime_Result> disciplins = new ObservableCollection<tlsp_getDisciplineChairFullTime_Result>();

            using (var context = new DBTeachingLoadEntities())
            {
                var QueryDisciplineChair = context.tlsp_getDisciplineChairFullTime(idAcademicYear, idChair);

                foreach (var disc in QueryDisciplineChair)
                    disciplins.Add(disc);
            }
            callback(disciplins, null);
        }

        /// <summary>
        /// Загрузка дисциплин кафедры 
        /// для заданного учебного года для очного отделения
        /// </summary>
        /// <param name="idChair"></param>
        /// <param name="idAcademicYear"></param>
        /// <returns></returns>
        public ObservableCollection<tlsp_getDisciplineChairFullTime_Result> GetDisciplineFullTime(int idChair, int idAcademicYear)
        {
            ObservableCollection<tlsp_getDisciplineChairFullTime_Result> disciplins = new ObservableCollection<tlsp_getDisciplineChairFullTime_Result>();

            using (var context = new DBTeachingLoadEntities())
            {
                var QueryDisciplineChair = context.tlsp_getDisciplineChairFullTime(idAcademicYear, idChair);

                foreach (var disc in QueryDisciplineChair)
                    disciplins.Add(disc);
            }
            return disciplins;
        }
        


        /// <summary>
        /// Загрузка данных по дисциплинам кафедры 
        /// для заданного учебного года для заочного отделения
        /// с помощью хранимой процедуры tlsp_getDisciplineChairPartTime
        /// </summary>
        /// <param name="callback"></param>
        /// <param name="idChair"></param>
        /// <param name="idAcademicYear"></param>
        public void GetDataDisciplinePartTime(Action<ObservableCollection<tlsp_getDisciplineChairPartTime_Result>, Exception> callback, int idChair, int idAcademicYear)
        {
            ObservableCollection<tlsp_getDisciplineChairPartTime_Result> disciplins = new ObservableCollection<tlsp_getDisciplineChairPartTime_Result>();

            using (var context = new DBTeachingLoadEntities())
            {
                var QueryDisciplineChair = context.tlsp_getDisciplineChairPartTime(idAcademicYear, idChair);

                foreach (var disc in QueryDisciplineChair)
                    disciplins.Add(disc);
            }
            callback(disciplins, null);
        }


        /// <summary>
        /// Загрузка дисциплин кафедры 
        /// для заданного учебного года для заочного отделения
        /// </summary>
        /// <param name="idChair"></param>
        /// <param name="idAcademicYear"></param>
        /// <returns></returns>
        public ObservableCollection<tlsp_getDisciplineChairPartTime_Result> GetDisciplinePartTime(int idChair, int idAcademicYear)
        {
            ObservableCollection<tlsp_getDisciplineChairPartTime_Result> disciplins = new ObservableCollection<tlsp_getDisciplineChairPartTime_Result>();

            using (var context = new DBTeachingLoadEntities())
            {
                var QueryDisciplineChair = context.tlsp_getDisciplineChairPartTime(idAcademicYear, idChair);

                foreach (var disc in QueryDisciplineChair)
                    disciplins.Add(disc);
            }
            return disciplins;
        }
        

        
        /// <summary>
        /// Формирование данных по дисциплинам кафедры 
        /// для заданного учебного года 
        /// с помощью хранимой процедуры tlsp_getDisciplineChair
        /// </summary>
        /// <param name="standartTime"></param>
        /// <param name="idChair"></param>
        /// <param name="idAcademicYear"></param>
        /// <returns></returns>
        public ObservableCollection<tlsp_getDisciplineChair_Result> GetDiscipline(ObservableCollection<tlsp_getDisciplineChair_Result> disciplines, int idChair, int idAcademicYear)
        {
            using (var context = new DBTeachingLoadEntities())
            {
                var QueryDisciplineChair = context.tlsp_getDisciplineChair(idAcademicYear, idChair);

                foreach (var disc in QueryDisciplineChair)
                    disciplines.Add(disc);
            }
            return disciplines;
        }


        public ObservableCollection<tlsp_getDisciplineChair_Result> GetDiscipline(int idChair, int idAcademicYear)
        {
            ObservableCollection<tlsp_getDisciplineChair_Result> disciplines = new ObservableCollection<tlsp_getDisciplineChair_Result>();

            using (var context = new DBTeachingLoadEntities())
            {
                var QueryDisciplineChair = context.tlsp_getDisciplineChair(idAcademicYear, idChair);

                foreach (var disc in QueryDisciplineChair)
                    disciplines.Add(disc);
            }
            return disciplines;
        }

        /// <summary>
        /// Формирование данных по дисциплинам кафедры для очного отделения
        /// для заданного учебного года 
        /// с помощью хранимой процедуры tlsp_getDisciplineChairFullTime
        /// </summary>
        /// <param name="standartTime"></param>
        /// <param name="idChair"></param>
        /// <param name="idAcademicYear"></param>
        /// <returns></returns>
        public ObservableCollection<tlsp_getDisciplineChairFullTime_Result> GetDisciplineFullTime(ObservableCollection<tlsp_getDisciplineChairFullTime_Result> disciplines, int idChair, int idAcademicYear)
        {
            using (var context = new DBTeachingLoadEntities())
            {
                var QueryDisciplineChair = context.tlsp_getDisciplineChairFullTime(idAcademicYear, idChair);

                foreach (var disc in QueryDisciplineChair)
                    disciplines.Add(disc);
            }
            return disciplines;
        }

        /// <summary>
        /// Формирование данных по дисциплинам кафедры для заочного отделения
        /// для заданного учебного года 
        /// с помощью хранимой процедуры tlsp_getDisciplineChairPartTime
        /// </summary>
        /// <param name="standartTime"></param>
        /// <param name="idChair"></param>
        /// <param name="idAcademicYear"></param>
        /// <returns></returns>
        public ObservableCollection<tlsp_getDisciplineChairPartTime_Result> GetDisciplinePartTime(ObservableCollection<tlsp_getDisciplineChairPartTime_Result> disciplines, int idChair, int idAcademicYear)
        {
            using (var context = new DBTeachingLoadEntities())
            {
                var QueryDisciplineChair = context.tlsp_getDisciplineChairPartTime(idAcademicYear, idChair);

                foreach (var disc in QueryDisciplineChair)
                    disciplines.Add(disc);
            }
            return disciplines;
        }

        
        /// <summary>
        /// Загрузка учебных планов
        /// </summary>
        /// <param name="idAcademicYear"></param>
        /// <returns></returns>
        public ObservableCollection<Curriculum> GetCurriculum(int idAcademicYear)
        {
            ObservableCollection<Curriculum> curriculs = new ObservableCollection<Curriculum>();

            using (var context = new DBTeachingLoadEntities())
            {
                var QueryCurriculum = from curr in context.Curriculum
                                      .Include("DictAcademicYear")
                                        .Include("DictQualification")
                                        .Include("DictSpeciality")
                                        .Include("DictEducationForm")
                                        .Include("Chair")
                                           where curr.IdAcademicYear == idAcademicYear
                                           orderby curr.DictQualification.NameQualification, curr.DictEducationForm.FormEducation descending, curr.Name
                                           select curr;

                foreach (Curriculum curr in QueryCurriculum)
                    curriculs.Add(curr);
                return curriculs;
            }
        }

        /// <summary>
        /// Формирование дисциплин кафедры
        /// для заданного учебного года
        /// по учебным планам университета
        /// с помощью хранимой процедуры tlsp_setDisciplineChair
        /// </summary>
        /// <param name="standartTime"></param>
        /// <param name="idChair"></param>
        /// <param name="idAcademicYear"></param>
        /// <returns></returns>
        public ObservableCollection<DisciplineChair> SetDiscipline(ObservableCollection<DisciplineChair> disciplines, int idChair, int idAcademicYear)
        {
            using (var context = new DBTeachingLoadEntities())
            {
                var QueryDisciplineIdChairPlan = context.tlsp_setDisciplineChair(idAcademicYear, idChair);
               

                foreach (int idDisc in QueryDisciplineIdChairPlan)
                {
                    disciplines.Add(new DisciplineChair
                    {
                        IdChair = idChair,
                        IdAcademicYear = idAcademicYear,
                        IdDisciplinePlan = idDisc
                    });
                }
            }
            return disciplines;
        }

        /// <summary>
        /// Формирование дисциплин кафедры
        /// для заданного учебного года
        /// для заданного учебного плана университета
        /// с помощью хранимой процедуры tlsp_setDisciplinesOfCurriculum
        /// </summary>
        /// <param name="standartTime"></param>
        /// <param name="idChair"></param>
        /// <param name="idAcademicYear"></param>
        /// <param name="idCurrrculum"></param>
        /// <returns></returns>
        public ObservableCollection<DisciplineChair> SetDisciplineOfCurriculum(ObservableCollection<DisciplineChair> disciplines, int idChair, int idAcademicYear, int idCurriculum)
        {
            using (var context = new DBTeachingLoadEntities())
            {
                var QueryDisciplineIdChairPlan = context.tlsp_setDisciplinesOfCurriculum(idAcademicYear, idChair, idCurriculum);

                foreach (int idDisc in QueryDisciplineIdChairPlan)
                {
                    disciplines.Add(new DisciplineChair
                    {
                        IdChair = idChair,
                        IdAcademicYear = idAcademicYear,
                        IdDisciplinePlan = idDisc
                    });
                }
            }
            return disciplines;
        }

        /// <summary>
        /// Добавление дисциплины в список дисциплин кафедры
        /// </summary>
        /// <param name="newDiscipline"></param>
        public void AddItemDataDiscipline(DisciplineChair newDiscipline)
        {
            using (var context = new DBTeachingLoadEntities())
            {
                DisciplineChair discipline = new DisciplineChair();

                // В случае, если дисциплина имеется с списке дисциплин кафедры, то она удаляется 
                // и добавляется аналогичная дисциплина из текущего учебного плана
                try
                {
                    var deleteDiscipline = (from disc in context.DisciplineChairs
                                            where disc.IdDisciplinePlan == newDiscipline.IdDisciplinePlan
                                            select disc).FirstOrDefault();
                   
                    if (deleteDiscipline != null)
                        {
                            context.DisciplineChairs.DeleteObject(deleteDiscipline);
                            context.SaveChanges();
                        }
                }
                catch (OptimisticConcurrencyException ex)
                {
                    throw new InvalidOperationException(string.Format(
                       "Дисциплина с IdDisciplinePlan '{0}' не может быть удален.\n", newDiscipline.IdDisciplinePlan), ex);
                }

                // Добавление дисциплины из учебного плана в список дисциплин кафедры
                try
                {
                    discipline.IdAcademicYear = newDiscipline.IdAcademicYear;
                    discipline.IdChair = newDiscipline.IdChair;
                    discipline.IdDisciplinePlan = newDiscipline.IdDisciplinePlan;

                    context.DisciplineChairs.AddObject(discipline);
                    context.SaveChanges();
                }
                catch (DataServiceRequestException ex)
                {
                    throw new ApplicationException("Ошибка добавления данных" + ex.ToString());
                }

                
            }
        }

        /// <summary>
        /// Удаление дисциплины из списка дисциплин кафедры
        /// </summary>
        /// <param name="delDisciplineID"></param>
        public void RemoveItemDiscipline(int delDisciplineID)
        {
            using (var context = new DBTeachingLoadEntities())
            {
                try
                {
                    var deleteDiscipline = (from disc in context.DisciplineChairs
                                            where disc.Id == delDisciplineID
                                       select disc).FirstOrDefault();

                    if (deleteDiscipline != null)
                    {
                        context.DisciplineChairs.DeleteObject(deleteDiscipline);
                        context.SaveChanges();
                    }
                }
                catch (OptimisticConcurrencyException ex)
                {
                    throw new InvalidOperationException(string.Format(
                       "Дисциплина с Id '{0}' не может быть удален.\n", delDisciplineID), ex);
                }
            }
        }

        public bool RemoveItemDiscipline(tlsp_getDisciplineChair_Result delDiscipline, out string message)
        {
            message = String.Empty;
            bool result = false;

            using (var context = new DBTeachingLoadEntities())
            {
                try
                {
                    var deleteDiscipline = (from disc in context.DisciplineChairs
                                            where disc.Id == delDiscipline.Id
                                            select disc).FirstOrDefault();

                    if (deleteDiscipline != null)
                    {
                        context.DisciplineChairs.DeleteObject(deleteDiscipline);
                        context.SaveChanges();
                    }
                }
                catch (UpdateException ex)
                {
                    message = string.Format("Дисциплина  '{0}'\n не может быть удалена\n", delDiscipline) + ex.ToString();
                    result = true;
                }
            }

            return result;
        }

        /// <summary>
        /// Удаление всех дисциплины кафедры
        /// </summary>
        /// <param name="delDisciplineID"></param>
        public void RemoveALLDiscipline(int idAcademicYear, int idChair)
        {
            using (var context = new DBTeachingLoadEntities())
            {
                try
                {
                    var deleteDiscipline = from disc in context.DisciplineChairs
                                            where (disc.IdAcademicYear == idAcademicYear) && (disc.IdChair == idChair)
                                            select disc;

                    if (deleteDiscipline != null)
                    {
                        foreach (var disc in deleteDiscipline)
                            context.DisciplineChairs.DeleteObject(disc);

                        context.SaveChanges();
                    }
                }
                catch (OptimisticConcurrencyException ex)
                {
                    throw new InvalidOperationException(string.Format(
                       "Удаление не выполнено полностью"), ex);
                }
            }
        }

        public bool RemoveALLDiscipline(int idAcademicYear, int idChair, out string message)
        {
            message = String.Empty;
            bool result = false;

            using (var context = new DBTeachingLoadEntities())
            {
                try
                {
                    var deleteDiscipline = from disc in context.DisciplineChairs
                                           where (disc.IdAcademicYear == idAcademicYear) && (disc.IdChair == idChair)
                                           select disc;

                    if (deleteDiscipline != null)
                    {
                        foreach (var disc in deleteDiscipline)
                            context.DisciplineChairs.DeleteObject(disc);

                        context.SaveChanges();
                    }
                }
                catch (UpdateException ex)
                {
                    message = string.Format("Удаление не выполнено полностью") + ex.ToString();
                    result = true;
                }
            }

            return result;
        }
    }
}
