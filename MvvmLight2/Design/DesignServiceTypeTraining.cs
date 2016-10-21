using MvvmLight2.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvvmLight2.Design
{
    public class DesignServiceTypeTraining
    {
        public ObservableCollection<DictTypeTraining> TypeTrainings { get; private set; }

        public DesignServiceTypeTraining()
        {
            TypeTrainings = new ObservableCollection<DictTypeTraining>
           {
               new DictTypeTraining
                        {
                            TypeWork = "Руководство, консультации, рецензирование и прием защиты курсовых проектов для российских обучающихся",
                        },
                        
                        new DictTypeTraining
                        {
                            TypeWork = "Проведение текущих консультаций по учебным дисциплинам - дневная форма",
                        },
                        
                        new DictTypeTraining
                        {
                            TypeWork = "Проверка и прием защиты контрольных работ, предусмотренных учебным планом",
                        }
                    };
        }
    }
}
