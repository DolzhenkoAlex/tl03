using System;
using System.Collections.ObjectModel;
using System.Linq;
using MvvmLight2.Model;

namespace MvvmLight2.Helper
{
    /// <summary>
    /// Справочние форм обучения
    /// </summary>
    public class ListEducationForm : ObservableCollection<DictEducationForm>
    {
        public ListEducationForm()
        {
            using (var context = new DBTeachingLoadEntities())
            {
                var QueryEducationForm = from educ in context.DictEducationForms
                                         where educ.StatusDel == true
                                         select educ;
                foreach (DictEducationForm e in QueryEducationForm)
                    this.Add(e);
            }
        }

        public DictEducationForm FindEducationForm(
            ObservableCollection<DictEducationForm> listEducationForm,
            string educationForm,
            Func<DictEducationForm, string, bool> predicate)
        {
            DictEducationForm education = new DictEducationForm();
            foreach (var ed in listEducationForm)
            {
                if (predicate(ed, educationForm))
                {
                    education = ed;
                    break;
                }
            }
            return education;
        }
    }
}
