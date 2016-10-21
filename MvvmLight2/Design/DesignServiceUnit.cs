using MvvmLight2.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvvmLight2.Design
{
    public class DesignServiceUnit
    {
        public ObservableCollection<DictUnit> Units { get; private set; }

        public DesignServiceUnit()
        {
            Units = new ObservableCollection<DictUnit>
           {
               new DictUnit
                        {
                            Unit = "академический час",
                        },
                        
                        new DictUnit
                        {
                            Unit = "член комисси на выпускника",
                        },
                        
                        new DictUnit
                        {
                            Unit = "контрольная работа",
                        }
                    };
        }
    }
}

