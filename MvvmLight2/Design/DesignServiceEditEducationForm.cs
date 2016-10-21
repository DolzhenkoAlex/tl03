using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvvmLight2.Model;

namespace MvvmLight2.Design
{
    public class DesignServiceEditEducationForm
    {
        public DictEducationForm DictEducationForm { get; private set; }

        public DesignServiceEditEducationForm()
        {
            DictEducationForm = new DictEducationForm
            {
                FormEducation = "заочная",

            };
        }
    }
}