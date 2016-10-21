using MvvmLight2.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvvmLight2.Design
{
    public class DesignServiceEducationForm
    {
        public ObservableCollection<DictEducationForm> EducationForms { get; private set; }

        public DesignServiceEducationForm()
        {
            EducationForms = new ObservableCollection<DictEducationForm>
           {
               new DictEducationForm
                        {
                            FormEducation = "очная",
                        },
                        
                        new DictEducationForm
                        {
                            FormEducation = "заочная",
                        },
                        
                        new DictEducationForm
                        {
                            FormEducation = "заочно-сокращенная",
                        }
                    };
        }
    }
}
