using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvvmLight2.Model;

namespace MvvmLight2.Design
{
    public class DesignServiceEditQualification
    {
        public DictQualification DictQualification { get; private set; }

        public DesignServiceEditQualification()
        {
            DictQualification = new DictQualification
            {
                NameQualification = "Бакалавр",

            };
        }
    }
}