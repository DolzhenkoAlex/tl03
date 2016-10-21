using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvvmLight2.Model;

namespace MvvmLight2.Design
{
    class DesignServiceEditTypeEmployment
    {
        public DictTypeEmployment DictTypeEmployment { get; private set; }

        public DesignServiceEditTypeEmployment()
        {
            DictTypeEmployment = new DictTypeEmployment
            {
                TypeOfEmployment = "почасовая оплата",

            };
        }
    }
}