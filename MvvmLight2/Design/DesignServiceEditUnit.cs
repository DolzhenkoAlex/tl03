using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvvmLight2.Model;

namespace MvvmLight2.Design
{
    class DesignServiceEditUnit
    {
        public DictUnit DictUnit { get; private set; }

        public DesignServiceEditUnit()
        {
            DictUnit = new DictUnit
            {
                Unit = "академический час",

            };
        }
    }
}