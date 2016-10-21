using MvvmLight2.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvvmLight2.Design
{
    public class DesignServiceQualification
    {
        public ObservableCollection<DictQualification> Qualifications { get; private set; }

        public DesignServiceQualification()
        {
            Qualifications = new ObservableCollection<DictQualification>
           {
               new DictQualification
                        {
                            NameQualification = "Специалист",
                        },
                        
                        new DictQualification
                        {
                            NameQualification = "Магистр",
                        },
                        
                        new DictQualification
                        {
                            NameQualification = "Бакалавр",
                        }
                    };
        }
    }
}