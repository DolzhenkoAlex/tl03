using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvvmLight2.Model;

namespace MvvmLight2.Design
{
    public class DesignerEditStandartTime
    {
        public StandartTime StandartTime { get; private set; }

        public DesignerEditStandartTime()
        {
            StandartTime = new StandartTime()
            {
                TypeOfWork = "Проведение консонсульнаций перед экзаменом",
                NormTime = 2,
                Notes = "Для группы численностью более 15 студентов"
                
            };
        }
    }
}
