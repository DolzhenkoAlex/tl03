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
    /// Сервис данных по учебным годам
    /// </summary>
    public class ServiceAcademicYear : IServiceAcademicYear
    {
        /// <summary>
        /// Загрузка данных по учебным годам из базы данных
        /// </summary>
        /// <param name="callback"></param>
        /// <param name="idFaculty"></param>
        public void GetDataAcademicYear(Action<ObservableCollection<DictAcademicYear>, Exception> callback)
        {
            ObservableCollection<DictAcademicYear> years = new ObservableCollection<DictAcademicYear>();

            using (var context = new DBTeachingLoadEntities())
            {
                try
                {
                    var QueryAcademicYear = from c in context.DictAcademicYears
                                            where c.StatusDel == true
                                            orderby c.Year
                                            select c;
                    foreach (DictAcademicYear c in QueryAcademicYear)
                        years.Add(c);
                }
                catch (EntityException ex)
                {
                    //throw new ApplicationException("Ошибка загрузки данных" + ex.ToString());
                }
            }
            callback(years, null);
        }

        /// <summary>
        /// Получение данных по учебным годам из базы данных
        /// </summary>
        /// <param name="teachers"></param>
        /// <param name="idFaculty"></param>
        /// <returns></returns>
        public ObservableCollection<DictAcademicYear> GetAcademicYear(ObservableCollection<DictAcademicYear> years)
        {
            using (var context = new DBTeachingLoadEntities())
            {
                var QueryAcademicYear = from c in context.DictAcademicYears
                                        where c.StatusDel == true
                                        orderby c.Year
                                        select c;

                foreach (DictAcademicYear c in QueryAcademicYear)
                    years.Add(c);
            }

            return years;
        }

        /// <summary>
        /// Добавление данных по новому учебному году
        /// </summary>
        /// <param name="newFaculty"></param>
        public void AddItemDataAcademicYear(DictAcademicYear newAcademicYear)
        {
            using (var context = new DBTeachingLoadEntities())
            {
                try
                {
                    context.DictAcademicYears.AddObject(newAcademicYear);
                    context.SaveChanges();
                }
                catch (DataServiceRequestException ex)
                {
                    throw new ApplicationException("Ошибка добавления данных" + ex.ToString());
                }
            }
        }

        /// <summary>
        /// Редактирование данных по учебному году
        /// </summary>
        /// <param name="delChair"></param>
        public void EditItemAcademicYear(DictAcademicYear editAcademicYear)
        {
            using (var context = new DBTeachingLoadEntities())
            {
                try
                {
                    var AcademicYearEdit = (from year in context.DictAcademicYears
                                            where year.Id == editAcademicYear.Id
                                            select year).FirstOrDefault();
                    if (AcademicYearEdit != null)
                    {
                        context.DictAcademicYears.ApplyCurrentValues(editAcademicYear);
                        context.SaveChanges();
                    }
                }
                catch (OptimisticConcurrencyException ex)
                {
                    throw new InvalidOperationException(string.Format(
                       "Учебный год с Id '{0}' не может быть отредактирован.\n", editAcademicYear.Id), ex);
                }
            }
        }

        /// <summary>
        /// Удаление данных по учебному году
        /// </summary>
        /// <param name="delChair"></param>
        public void RemoveItemAcademicYear(DictAcademicYear delAcademicYear)
        {
            using (var context = new DBTeachingLoadEntities())
            {
                try
                {
                    var deleteAcademicYear = (from year in context.DictAcademicYears
                                              where year.Id == delAcademicYear.Id
                                              select year).FirstOrDefault();

                    if (deleteAcademicYear != null)
                    {

                        deleteAcademicYear.StatusDel = false;
                        context.DictAcademicYears.ApplyCurrentValues(deleteAcademicYear);
                       // context.DictAcademicYears.DeleteObject(deleteAcademicYear);

                        context.SaveChanges();
                    }
                }
                catch (OptimisticConcurrencyException ex)
                {
                    throw new InvalidOperationException(string.Format(
                       "Учебный год с Id '{0}' не может быть удален.\n", delAcademicYear.Id), ex);
                }
            }
        }

        // <summary>
        /// Нахождение учебного года по коду
        /// </summary>
        /// <param name="codeChair"></param>
        /// <returns></returns>
        public DictAcademicYear GetItemAcademicYear(string acYear)
        {
            using (var context = new DBTeachingLoadEntities())
            {
                var year = (from ch in context.DictAcademicYears
                            where (ch.Year == acYear)
                            select ch).FirstOrDefault();
                return year;
            }
        }
    }
}

