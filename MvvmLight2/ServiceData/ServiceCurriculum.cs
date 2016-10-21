using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.Services.Client;
using System.Linq;
using MvvmLight2.Model;


namespace MvvmLight2.ServiceData
{
    public class ServiceCurriculum: IServiceCurriculum
    {
        /// <summary>
        /// Загрузка данных по учебным планам
        /// </summary>
        /// <param name="callback"></param>
        public void GetDataCurriculum(Action<ObservableCollection<Curriculum>, Exception> callback, int idAcademicYear)
        {
            ObservableCollection<Curriculum> curriculums = new ObservableCollection<Curriculum>();
            using (var context = new DBTeachingLoadEntities())
            {
                try
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
                    foreach (Curriculum c in QueryCurriculum)
                        curriculums.Add(c);
                }
                catch (EntityException ex)
                {
                    //throw new ApplicationException("Ошибка загрузки данных" + ex.ToString());
                }
            }
            callback(curriculums, null);
        }

        /// <summary>
        /// Получение данных по учебным планам из базы данных
        /// </summary>
        /// <param name="curriculums"></param>
        /// <param name="idUniversity"></param>
        /// <returns></returns>
        public ObservableCollection<Curriculum> GetCurriculum(ObservableCollection<Curriculum> curriculums, int idAcademicYear)
        {
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
                foreach (Curriculum c in QueryCurriculum)
                    curriculums.Add(c);
            }

            return curriculums;
        }

        /// <summary>
        /// Загрузка данных по дисциплинам учебного плана
        /// </summary>
        /// <param name="standartTime"></param>
        /// <param name="IdCurriculum"></param>
        /// <returns></returns>
        public ObservableCollection<DisciplinePlan> GetDiscipline(ObservableCollection<DisciplinePlan> disciplines, int IdCurriculum)
        {
            disciplines.Clear();

            using (var context = new DBTeachingLoadEntities())
            {
                var QueryDiscipline = from disc in context.DisciplinePlans
                                        .Include("Chair")
                                      where disc.IdCurriculum == IdCurriculum
                                      orderby disc.CodePlan
                                      select disc;
                foreach (DisciplinePlan c in QueryDiscipline)
                    disciplines.Add(c);
            }

            return disciplines;
        }


        /// <summary>
        /// Формирование данных для отчета Закрепление дисциплин
        /// </summary>
        /// <param name="idAcademicYear"></param>
        /// <returns></returns>
        public ObservableCollection<tlsp_getAllDiscipline_Result> GetAllDisciplines( int idAcademicYear)
        {
            ObservableCollection<tlsp_getAllDiscipline_Result> disciplines = new ObservableCollection<tlsp_getAllDiscipline_Result>();
            using (var context = new DBTeachingLoadEntities())
            {
                var query = context.tlsp_getAllDiscipline(idAcademicYear);

                foreach (var disc in query)
                    disciplines.Add(disc);
            }
            return disciplines;
        }

        /// <summary>
        /// Добавление данных по новому учебному плану
        /// </summary>
        /// <param name="curriculum"></param>
        public void AddItemCurriculum(Curriculum newCurriculum)
        {
            using (var context = new DBTeachingLoadEntities())
            {
                try
                {
                    context.Curriculum.AddObject(newCurriculum);
                    context.SaveChanges();
                }
                catch (DataServiceRequestException ex)
                {
                    throw new ApplicationException("Ошибка добавления данных" + ex.ToString());
                }

                
            }
        }


        /// <summary>
        /// Редактирование данных по учебному плану
        /// </summary>
        /// <param name="editGroup"></param>
        public void EditItemCurriculum(Curriculum editCurriculum)
        {
            using (var context = new DBTeachingLoadEntities())
            {
                try
                {
                    var currriculumEdit = (from curr in context.Curriculum
                                           where curr.Id == editCurriculum.Id
                                     select curr).FirstOrDefault();
                    if (currriculumEdit != null)
                    {
                        context.Curriculum.ApplyCurrentValues(editCurriculum);
                        context.SaveChanges();
                    }
                }
                catch (OptimisticConcurrencyException ex)
                {
                    throw new InvalidOperationException(string.Format(
                       "Учебный план с Id '{0}' не может быть отредактирован.\n", editCurriculum.Id), ex);
                }
            }
        }

        // <summary>
        /// Удаление данных по учебному плану
        /// </summary>
        /// <param name="delCurriculum"></param>
        public void RemoveItemCurriculum(Curriculum delCurriculum)
        {
            using (var context = new DBTeachingLoadEntities())
            {
                ObservableCollection<DisciplinePlan> delDisciplines = new ObservableCollection<DisciplinePlan>();
                try
                {
                    var deleteCurriculum = (from curr in context.Curriculum
                                            where curr.Id == delCurriculum.Id
                                            select curr).FirstOrDefault();

                    if (deleteCurriculum != null)
                    {
                        context.Curriculum.DeleteObject(deleteCurriculum);
                        context.SaveChanges();
                    }
                }
                catch (OptimisticConcurrencyException ex)
                {
                    throw new InvalidOperationException(string.Format(
                       "Учебный план с Id '{0}' не может быть удален.\n", delCurriculum.Id), ex);
                }
            }
        }

        /// <summary>
        /// Добавление новых данных по дисциплине учебного плана
        /// </summary>
        /// <param name="newDiscipline"></param>
        public void AddItemDiscipline(DisciplinePlan newDiscipline)
        {
            using (var context = new DBTeachingLoadEntities())
            {
                try
                {
                    context.DisciplinePlans.AddObject(newDiscipline);
                    context.SaveChanges();
                }
                catch (DataServiceRequestException ex)
                {
                    throw new ApplicationException("Ошибка добавления данных" + ex.ToString());
                }
            }
        }

        /// <summary>
        /// Редактирования данных по дисциплине учебного плана
        /// </summary>
        /// <param name="editDiscipline"></param>
        public void EditItemDiscipline(DisciplinePlan editDiscipline)
        {
            using (var context = new DBTeachingLoadEntities())
            {
                try
                {
                    var discEdit = (from disc in context.DisciplinePlans
                                    where disc.Id == editDiscipline.Id
                                           select disc).FirstOrDefault();
                    if (discEdit != null)
                    {
                        context.DisciplinePlans.ApplyCurrentValues(editDiscipline);
                        context.SaveChanges();
                    }
                }
                catch (OptimisticConcurrencyException ex)
                {
                    throw new InvalidOperationException(string.Format(
                       "Дисцплина учебноно плана с Id '{0}' не может быть отредактирован.\n", editDiscipline.Id), ex);
                }
            }
        }

        /// <summary>
        /// Удаление данных по дисциплине учебного плана
        /// </summary>
        /// <param name="delDiscipline"></param>
        public void RemoveItemDiscipline(DisciplinePlan delDiscipline)
        {
            using (var context = new DBTeachingLoadEntities())
            {
                try
                {
                    var deleteDiscipline = (from disc in context.DisciplinePlans
                                            where disc.Id == delDiscipline.Id
                                            select disc).FirstOrDefault();

                    if (deleteDiscipline != null)
                    {
                        context.DisciplinePlans.DeleteObject(deleteDiscipline);
                        context.SaveChanges();
                    }
                }
                catch (UpdateException ex)
                {
                    throw new InvalidOperationException(string.Format(
                       "Дисциплина учебного плана с Id '{0}' не может быть удалена\n", delDiscipline.Id), ex);
                }
            }
        }

        /// <summary>
        /// Удаление данных по дисциплине учебного плана с возвратом успеха операции
        /// </summary>
        /// <param name="delDiscipline"></param>
        public bool RemoveItemDiscipline(DisciplinePlan delDiscipline, out string message)
        {
            message = string.Empty;
            bool result = false;

            using (var context = new DBTeachingLoadEntities())
            {

                try
                {
                    var deleteDiscipline = (from disc in context.DisciplinePlans
                                            where disc.Id == delDiscipline.Id
                                            select disc).FirstOrDefault();

                    if (deleteDiscipline != null)
                    {
                        context.DisciplinePlans.DeleteObject(deleteDiscipline);
                        context.SaveChanges();
                    }
                }
                catch (UpdateException ex)
                {
                    message = string.Format("Дисциплина учебного плана\n '{0}' \nне может быть удалена!\nИспользуется в сформированных дисциплинах кафедр", delDiscipline.Discipline);
                    result = true;
                }
                return result;
            }
        }


        /// <summary>
        /// Получение титульных данных учебных планов для отчета по трудоемкости
        /// </summary>
        /// <param name="idAcademicYear"></param>
        /// <returns></returns>
        public ObservableCollection<tlsp_getCurriculumForComplexReport_Result> GetCurriculumForComplexReport(
            ObservableCollection<tlsp_getCurriculumForComplexReport_Result> curriculumsForReport, int idAcademicYear)
        {
            curriculumsForReport.Clear();

            using (var context = new DBTeachingLoadEntities())
            {
                var query = context.tlsp_getCurriculumForComplexReport(idAcademicYear as int?);

                //int count = query.Count();

                //if (query.Count() > 0)
                    foreach (tlsp_getCurriculumForComplexReport_Result cur in query)
                        curriculumsForReport.Add(cur);
                
                
                //for(int i = 1; i < 6 ; i++)
                //{
                //    switch(i)
                //    {
                //        case 1:
                //            var query = context.tlsp_getCurriculumForComplexReport(idAcademicYear as int?, true, false, false, false, false, false);
                //            if (query.Count() > 0)
                //                foreach (var cur in query)
                //                    curriculumsForReport.Add(cur);
                //            break;

                //        case 2:
                //            query = context.tlsp_getCurriculumForComplexReport(idAcademicYear as int?,  false, true, false, false, false, false);
                //            if (query.Count() > 0)
                //                foreach (var cur in query)
                //                    curriculumsForReport.Add(cur);
                //            break;

                //        case 3:
                //            query = context.tlsp_getCurriculumForComplexReport(idAcademicYear as int?, false,  false, true, false, false, false);
                //            if (query.Count() > 0)
                //                foreach (var cur in query)
                //                    curriculumsForReport.Add(cur);
                //            break;

                //        case 4:
                //            query = context.tlsp_getCurriculumForComplexReport(idAcademicYear as int?, false,  false, false, true, false, false);
                //            if (query.Count() > 0)
                //                foreach (var cur in query)
                //                    curriculumsForReport.Add(cur);
                //            break;
                        
                //        case 5:
                //            query = context.tlsp_getCurriculumForComplexReport(idAcademicYear as int?, false,  false, false, false, true, false);
                //            if (query.Count() > 0)
                //                foreach (var cur in query)
                //                    curriculumsForReport.Add(cur);
                //            break;

                //        case 6:
                //            query = context.tlsp_getCurriculumForComplexReport(idAcademicYear as int?, false,  false, false, false, false, true);
                //            if (query.Count() > 0)
                //                foreach (var cur in query)
                //                    curriculumsForReport.Add(cur);
                //            break;
                    //}
                //}
            }
            return curriculumsForReport;
        }

        /// <summary>
        /// Получение количества студентов, обучающихся по каждому учебному плану
        /// </summary>
        /// <param name="curriculumWithStudent"></param>
        /// <param name="idAcademicYear"></param>
        /// <param name="course"></param>
        /// <returns></returns>
        public ObservableCollection<tlsp_getCurriculumWithStudent_Result> GetCurriculumWithStudent(
            ObservableCollection<tlsp_getCurriculumWithStudent_Result> curriculumWithStudent, int idAcademicYear)
        {
            curriculumWithStudent.Clear();

            using (var context = new DBTeachingLoadEntities())
            {
                var query = context.tlsp_getCurriculumWithStudent(idAcademicYear as int?);
                //if (query.Count() > 0)
                    foreach (var cur in query)
                        curriculumWithStudent.Add(cur);
            }

            return curriculumWithStudent;
        }
    }
}
