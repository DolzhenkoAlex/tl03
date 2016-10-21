using MvvmLight2.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvvmLight2.Design
{
    public class DesignServiceSpeciality
    {
        //public ObservableCollection<DictSpeciality> Specialitys { get; private set; }
        public ObservableCollection<DictSpeciality> SpecialityList { get; private set; }

        public DesignServiceSpeciality()
        {
            SpecialityList= new ObservableCollection<DictSpeciality>
           {
               new DictSpeciality
                        {
                            CodeSpeciality = "080200",
                            NameSpeciality = "Менеджмент",
                            Profile = "Производственный менеджмент",
                        },
                        
                        new DictSpeciality
                        {
                            CodeSpeciality = "08010001",
                            NameSpeciality = "Экономика",
                            Profile = "Бухгалтерский учет, анализ и аудит"
                        },
                        
                        new DictSpeciality
                        {
                            CodeSpeciality = "230400",
                            NameSpeciality = "Информационные системы и технологии",
                            Profile = "",
                        }
                    };
        }
    }
}
