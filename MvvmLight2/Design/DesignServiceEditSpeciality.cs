using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvvmLight2.Model;

namespace MvvmLight2.Design
{
    public class DesignServiceEditSpeciality
    {
        public DictSpeciality DictSpeciality { get; private set; }

        public DesignServiceEditSpeciality()
        {
            DictSpeciality = new DictSpeciality
            {
                CodeSpeciality = "08010001",
                NameSpeciality = "Экономика",
                Profile = "Бухгалтерский учет, анализ и аудит"
            };
        }
    }
}