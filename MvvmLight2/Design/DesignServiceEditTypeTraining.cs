using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvvmLight2.Model;

namespace MvvmLight2.Design
{
    class DesignServiceEditTypeTraining
    {
        public DictTypeTraining DictTypeTraining { get; private set; }

        public DesignServiceEditTypeTraining()
        {
            DictTypeTraining = new DictTypeTraining
            {
                TypeWork = "Проведение семинара",

            };
        }
    }
}
