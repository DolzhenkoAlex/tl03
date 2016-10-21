using MvvmLight2.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvvmLight2.Design
{
    public class DesignServiceAcademicYear
    {
        public ObservableCollection<DictAcademicYear> AcademicYears { get; private set; }

        public DesignServiceAcademicYear()
        {
            AcademicYears = new ObservableCollection<DictAcademicYear>
           {
               new DictAcademicYear
                        {
                            Year = "2013-2014",
                        },
                        
                        new DictAcademicYear
                        {
                            Year = "2014-2015",
                        },
                        
                        new DictAcademicYear
                        {
                            Year = "не задано",
                        }
                    };
        }
    }
}
