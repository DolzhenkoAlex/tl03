using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvvmLight2.Model;

namespace MvvmLight2.Design
{
    public class DesignServiceEditAcademicYear
    {
        public DictAcademicYear DictAcademicYear { get; private set; }

        public DesignServiceEditAcademicYear()
        {
            DictAcademicYear = new DictAcademicYear
            {
                Year = "2014-2015",

            };
        }
    }
}