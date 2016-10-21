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
    /// Сервис данных по формам обучения
    /// </summary>
    public class ServiceEducationForm : IServiceEducationForm
    {
        /// <summary>
        /// Загрузка данных по формам обучения из базы данных
        /// </summary>
        /// <param name="callback"></param>
        /// <param name="idFaculty"></param>
        public void GetDataEducationForm(Action<ObservableCollection<DictEducationForm>, Exception> callback)
        {
            ObservableCollection<DictEducationForm> educationform = new ObservableCollection<DictEducationForm>();

            using (var context = new DBTeachingLoadEntities())
            {
                try
                {
                    var QueryEducationForm = from c in context.DictEducationForms
                                             where c.StatusDel == true
                                             orderby c.FormEducation
                                             select c;
                    foreach (DictEducationForm c in QueryEducationForm)
                        educationform.Add(c);
                }
                catch (EntityException ex)
                {
                    //throw new ApplicationException("Ошибка загрузки данных" + ex.ToString());
                }
            }
            callback(educationform, null);
        }

        /// <summary>
        /// Получение данных по формам обучения из базы данных
        /// </summary>
        /// <param name="teachers"></param>
        /// <param name="idFaculty"></param>
        /// <returns></returns>
        public ObservableCollection<DictEducationForm> GetEducationForm(ObservableCollection<DictEducationForm> educationform)
        {
            using (var context = new DBTeachingLoadEntities())
            {
                var QueryEducationForm = from c in context.DictEducationForms
                                         where c.StatusDel == true
                                         orderby c.FormEducation
                                         select c;

                foreach (DictEducationForm c in QueryEducationForm)
                    educationform.Add(c);
            }

            return educationform;
        }

        /// <summary>
        /// Добавление данных по новой форме обучения
        /// </summary>
        /// <param name="newFaculty"></param>
        public void AddItemDataEducationForm(DictEducationForm newEducationForm)
        {
            using (var context = new DBTeachingLoadEntities())
            {
                try
                {
                    context.DictEducationForms.AddObject(newEducationForm);
                    context.SaveChanges();
                }
                catch (DataServiceRequestException ex)
                {
                    throw new ApplicationException("Ошибка добавления данных" + ex.ToString());
                }
            }
        }

        /// <summary>
        /// Редактирование данных по форме обучения
        /// </summary>
        /// <param name="delChair"></param>
        public void EditItemEducationForm(DictEducationForm editEducationForm)
        {
            using (var context = new DBTeachingLoadEntities())
            {
                try
                {
                    var EducationFormEdit = (from educationform in context.DictEducationForms
                                             where educationform.Id == editEducationForm.Id
                                             select educationform).FirstOrDefault();
                    if (EducationFormEdit != null)
                    {
                        context.DictEducationForms.ApplyCurrentValues(editEducationForm);
                        context.SaveChanges();
                    }
                }
                catch (OptimisticConcurrencyException ex)
                {
                    throw new InvalidOperationException(string.Format(
                       "Форма обучения с Id '{0}' не может быть отредактирована.\n", editEducationForm.Id), ex);
                }
            }
        }

        /// <summary>
        /// Удаление данных по форме обучения
        /// </summary>
        /// <param name="delChair"></param>
        public void RemoveItemEducationForm(DictEducationForm delEducationForm)
        {
            using (var context = new DBTeachingLoadEntities())
            {
                try
                {
                    var deleteEducationForm = (from educationform in context.DictEducationForms
                                               where educationform.Id == delEducationForm.Id
                                               select educationform).FirstOrDefault();

                    if (deleteEducationForm != null)
                    {

                        deleteEducationForm.StatusDel = false;
                        context.DictEducationForms.ApplyCurrentValues(deleteEducationForm);
                       // context.DictEducationForms.DeleteObject(deleteEducationForm);

                        context.SaveChanges();
                    }
                }
                catch (OptimisticConcurrencyException ex)
                {
                    throw new InvalidOperationException(string.Format(
                       "Форма обучения с Id '{0}' не может быть удалена.\n", delEducationForm.Id), ex);
                }
            }
        }

        // <summary>
        /// Нахождение формы обучения по коду
        /// </summary>
        /// <param name="codeChair"></param>
        /// <returns></returns>
        public DictEducationForm GetItemEducationForm(string edForm)
        {
            using (var context = new DBTeachingLoadEntities())
            {
                var educationform = (from ch in context.DictEducationForms
                                     where (ch.FormEducation == edForm)
                                     select ch).FirstOrDefault();
                return educationform;
            }
        }
    }
}
