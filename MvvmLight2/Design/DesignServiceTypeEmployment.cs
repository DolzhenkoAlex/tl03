using MvvmLight2.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvvmLight2.Design
{
    public class DesignServiceTypeEmployment
    {
        public ObservableCollection<DictTypeEmployment> TypeEmployments { get; private set; }

        public DesignServiceTypeEmployment()
        {
            TypeEmployments = new ObservableCollection<DictTypeEmployment>
           {
               new DictTypeEmployment
                        {
                            TypeOfEmployment = "основная",
                        },
                        
                        new DictTypeEmployment
                        {
                            TypeOfEmployment = "совместительство",
                        },
                        
                        new DictTypeEmployment
                        {
                            TypeOfEmployment = "почасовая оплата",
                        }
                    };
        }
    }
}
